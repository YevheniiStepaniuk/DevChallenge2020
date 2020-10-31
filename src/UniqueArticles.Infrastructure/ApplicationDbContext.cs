using Microsoft.EntityFrameworkCore;
using UniqueArticles.Domain.Entities;

namespace UniqueArticles.Infrastructure
{
    public class ApplicationDbContext: DbContext
    {
        public DbSet<Article> Articles  { get; set; }
        public DbSet<ArticleRelation> ArticleRelations  { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        /// <inheritdoc />
        protected ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ArticleRelation>().HasKey(a => new {DuplicateId = a.RelatedArticleId, a.ArticleId});

            modelBuilder.Entity<ArticleRelation>().HasOne(a => a.Article)
                .WithMany(a => a.ArticlesRelationToThis)
                .HasForeignKey(a => a.ArticleId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ArticleRelation>()
                .HasOne(a => a.RelatedArticle)
                .WithMany(a => a.ArticlesRelationFromThis)
                .HasForeignKey(a => a.RelatedArticleId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}