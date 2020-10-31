using System.Collections.Generic;
using System.Threading.Tasks;
using UniqueArticles.Domain.Entities;

namespace UniqueArticles.Domain.Contracts.Repositories
{
    public interface IArticlesRepository
    {
        Task<List<Article>> GetAll();
        Task<Article> GetById(int articleId);
        Task<Article> Create(Article article);
        Task<List<ArticleRelation>> GetAllRelations();
    }
}