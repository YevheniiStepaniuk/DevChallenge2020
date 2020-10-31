using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace UniqueArticles.Infrastructure
{
    public class DesignTimeDbContextFactory: IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            Console.WriteLine("Applying migrations");
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddEnvironmentVariables()
                .AddJsonFile("appsettings.json", true, reloadOnChange: false)
                .Build();

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var connectionString = configuration["Database:ConnectionString"];
            builder.UseSqlServer(connectionString);

            Console.WriteLine((string)"Connection string used: {0}", (object)connectionString);

            return new ApplicationDbContext(builder.Options);
        }
    }
}