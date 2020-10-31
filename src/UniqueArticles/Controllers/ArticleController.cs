using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UniqueArticles.Domain.Contracts;
using UniqueArticles.Domain.Models;

namespace UniqueArticles.Controllers
{
    [ApiController]
    [Route("/articles")]
    public class ArticleController
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet]
        public async Task<ActionResult<ArticlesResponse>> GetAll()
        {
            var articles = await _articleService.GetAll();

            return articles;
        }

        [HttpGet("{articleId:int}")]
        public async Task<ActionResult<ArticleResponse>> GetId(int articleId)
        {
            var article = await _articleService.GetById(articleId);

            if (article == null)
            {
                return new NotFoundResult();
            }

            return article;
        }

        [HttpPost]
        public async Task<ActionResult<ArticleResponse>> Create(CreateArticleRequest request)
        {
            var article = await _articleService.Create(request);

            return article;
        }

        [HttpGet("duplicate_groups")]
        public async Task<ActionResult<ArticleGroupsResponse>> GetDuplicateGroups()
        {
            var groups = await _articleService.GetDuplicateGroups();

            return groups;
        }

    }
}