using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UniqueArticles.Application.Services;
using UniqueArticles.Domain.Configuration;
using UniqueArticles.Domain.Contracts;
using UniqueArticles.Domain.Contracts.Repositories;
using UniqueArticles.Infrastructure;

namespace UniqueArticles
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(c =>
                c.UseSqlServer(Configuration["Database:ConnectionString"]));

            services.Configure<UniqueArticlesOptions>(Configuration.GetSection("UniqueArticles"));

            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<IArticlesRepository, ArticlesRepository>();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Unique Articles API",
                    Version = "v1",
                });
            });

            services.AddControllers().AddNewtonsoftJson(c =>
            {
                c.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                c.SerializerSettings.Converters.Add(new StringEnumConverter());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.MigrateDatabaseToLatest();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Unique Articles API");
            });
        }
    }
}
