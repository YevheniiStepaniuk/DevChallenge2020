using System.Threading.Tasks;
using UniqueArticles.Domain.Models;

namespace UniqueArticles.Domain.Contracts
{
    public interface IArticleService
    {
        Task<ArticlesResponse> GetAll();
        Task<ArticleResponse> GetById(int articleId);
        Task<ArticleResponse> Create(CreateArticleRequest request);
        Task<ArticleGroupsResponse> GetDuplicateGroups();
    }
}