using Newtonsoft.Json;

namespace UniqueArticles.Domain.Models
{
    public class ArticleResponse
    {
        public int Id { get; set; }
        public string Content { get; set; }

        [JsonProperty("duplicate_article_ids")]
        public int[] DuplicateArticleIds { get; set; }
    }
}