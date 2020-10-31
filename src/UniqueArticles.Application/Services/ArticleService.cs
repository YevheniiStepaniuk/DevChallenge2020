using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using UniqueArticles.Domain;
using UniqueArticles.Domain.Configuration;
using UniqueArticles.Domain.Contracts;
using UniqueArticles.Domain.Contracts.Repositories;
using UniqueArticles.Domain.Entities;
using UniqueArticles.Domain.Extensions;
using UniqueArticles.Domain.Models;

namespace UniqueArticles.Application.Services
{
    public class ArticleService : IArticleService
    {
        private readonly UniqueArticlesOptions _articlesOptions;
        private readonly IArticlesRepository _articlesRepository;

        public ArticleService(IOptions<UniqueArticlesOptions> articlesOptions, IArticlesRepository articlesRepository)
        {
            _articlesOptions = articlesOptions.Value;
            _articlesRepository = articlesRepository;
        }

        public async Task<ArticlesResponse> GetAll()
        {
            var articles = await _articlesRepository.GetAll();

            return new ArticlesResponse
            {
                Articles = articles.Select(a => a.ToResponse(_articlesOptions.SimilarityPercent)).ToArray()
            };
        }

        public async Task<ArticleResponse> GetById(int articleId)
        {
            var article = await _articlesRepository.GetById(articleId);

            return article?.ToResponse(_articlesOptions.SimilarityPercent);
        }

        public async Task<ArticleResponse> Create(CreateArticleRequest request)
        {
            var articleDuplicates = new List<ArticleRelation>();
            var calculator = new ArticlesSimilarityCalculator();

            foreach (var article in await _articlesRepository.GetAll())
            {
                var percent = calculator.CalculateSimilarityPercent(request.Content, article.Content);

                articleDuplicates.Add(new ArticleRelation
                {
                    RelatedArticleId = article.Id,
                    SimilarityPercent = percent
                });
            }

            var createdArticle = await _articlesRepository.Create(new Article { Content = request.Content, ArticlesRelationToThis = articleDuplicates });
            

            return createdArticle.ToResponse(_articlesOptions.SimilarityPercent);
        }

        public async Task<ArticleGroupsResponse> GetDuplicateGroups()
        {
            var relations = await _articlesRepository.GetAllRelations();

           var groups =  relations
               .Where(a => a.SimilarityPercent > _articlesOptions.SimilarityPercent)
                .GroupBy(a => a.ArticleId)
               .Select(a => a.Select(ag => ag.RelatedArticleId).Append(a.Key));

           return new ArticleGroupsResponse {DuplicateGroups = groups};
        }
    }
}