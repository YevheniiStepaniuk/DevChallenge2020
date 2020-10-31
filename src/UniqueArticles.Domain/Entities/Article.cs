using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using UniqueArticles.Domain.Extensions;

namespace UniqueArticles.Domain.Entities
{
    [Table("Article")]
    public class Article
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public List<ArticleRelation> ArticlesRelationToThis { get; set; }
        public List<ArticleRelation> ArticlesRelationFromThis { get; set; }

        [NotMapped]
        public List<ArticleRelation> ArticleRelations =>
            ArticlesRelationToThis.EmptyIfNull().Concat(ArticlesRelationFromThis.EmptyIfNull()).ToList();
    }
}
