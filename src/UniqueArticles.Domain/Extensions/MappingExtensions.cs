using System.Linq;
using UniqueArticles.Domain.Entities;
using UniqueArticles.Domain.Models;

namespace UniqueArticles.Domain.Extensions
{
    public static class MappingExtensions
    {
        public static ArticleResponse ToResponse(this Article article, int similarityPercent)
        {
            var duplicateArticleIds = article.ArticlesRelationFromThis
                .EmptyIfNull()
                .Where(ra => ra.SimilarityPercent >= similarityPercent)
                .Select(ra => ra.ArticleId)
                .Union(article.ArticlesRelationToThis
                    .EmptyIfNull()
                    .Where(ra => ra.SimilarityPercent >= similarityPercent)
                    .Select(ra => ra.RelatedArticleId))
                .ToArray();

            var articleResponse = new ArticleResponse
            {
                Content = article.Content,
                Id = article.Id,
                DuplicateArticleIds = duplicateArticleIds
            };

            return articleResponse;
        }
    }
}