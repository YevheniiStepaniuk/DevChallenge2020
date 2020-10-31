using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UniqueArticles.Infrastructure;

namespace UniqueArticles
{
    public static class ApplicationBuilderExtensions
    {
        public static void MigrateDatabaseToLatest(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                context.Database.EnsureCreated();
                context.Database.Migrate();
            }
        }
    }
}