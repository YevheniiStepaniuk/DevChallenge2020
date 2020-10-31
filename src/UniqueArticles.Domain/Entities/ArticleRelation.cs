using System.ComponentModel.DataAnnotations.Schema;

namespace UniqueArticles.Domain.Entities
{
    [Table("ArticleRelation")]
    public class ArticleRelation
    {
        public int ArticleId { get; set; }
        public int RelatedArticleId { get; set; }
        public double SimilarityPercent { get; set; }

        public Article Article { get; set; }
        public Article RelatedArticle { get; set; }
    }
}