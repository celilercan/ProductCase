using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProductCase.Data;
using ProductCase.Service.ProductServices;
using ProductCase.Service.ProductCategoryServices;
using ProductCase.Caching;

namespace ProductCase.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddEntityFrameworkSqlServer();

            services.AddDbContext<DatabaseContext>(item => item.UseSqlServer(Configuration.GetConnectionString("sqlserver")));

            services.AddDistributedRedisCache(opt =>
            {
                opt.Configuration = Configuration.GetConnectionString("redis");
                opt.InstanceName = "master";
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("CoreSwagger", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Swagger on ASP.NET Core",
                    Version = "1.0.0",
                    Description = "Try Swagger",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "Celil Ercan",
                        Url = new Uri("http://celilercan.com"),
                        Email = "celilercan91@gmail.com"
                    },
                    TermsOfService = new Uri("http://swagger.io/terms/")
                });
            });

            #region Dependencies
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddTransient<ICacheManager, CacheManager>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IProductCategoryService, ProductCategoryService>();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/CoreSwagger/swagger.json", "ProductCase API V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
