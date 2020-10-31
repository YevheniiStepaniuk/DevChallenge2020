using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UniqueArticles.Domain.Contracts.Repositories;
using UniqueArticles.Domain.Entities;

namespace UniqueArticles.Infrastructure
{
    public class ArticlesRepository: IArticlesRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ArticlesRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<List<Article>> GetAll()
        {
            var  articles = await _applicationDbContext
                .Articles
                .Include(a => a.ArticlesRelationFromThis)
                .Include(a => a.ArticlesRelationToThis)
                .ToListAsync();

            return articles;
        }

        public async Task<List<ArticleRelation>> GetAllRelations()
        {
            var relations = await _applicationDbContext.ArticleRelations.ToListAsync();

            return relations;
        }

        public async Task<Article> GetById(int articleId)
        {
            var article = await _applicationDbContext.Articles
                .Include(a => a.ArticlesRelationFromThis)
                .Include(a => a.ArticlesRelationToThis)
                .SingleOrDefaultAsync(a => a.Id == articleId);

            return article;
        }

        public async Task<Article> Create(Article article)
        {
            await _applicationDbContext.Articles.AddAsync(article);
            await _applicationDbContext.SaveChangesAsync();

            return article;
        }
    }
}