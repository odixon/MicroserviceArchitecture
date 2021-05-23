using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using OnlineCourse.Catalog.Context;
using OnlineCourse.Catalog.Repositories;
using OnlineCourse.Catalog.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCourse.Catalog
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



            services.AddControllers(options =>
            {
                /*
                 * Tüm controllerlar için Authorize attribute'ü merkezi bir  noktadan ekledik.
                 */
                options.Filters.Add(new AuthorizeFilter());

            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OnlineCourse.Catalog", Version = "v1" });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.Authority = Configuration["IdentityServer"]; // token kim tarafından dağıtılacak onu belirliyoruz
                options.Audience = "resource_catalog"; // audience bilgisini veriyoruz
                options.RequireHttpsMetadata = false; // Https durumunu false ediyoruz
            });
            /*
             * appsettings dosyasından mongodb connection bilgilerini okuyup bu bilgileri  DI ile  kullanabilmek için 
             * startup içerisinde Singleton olarak ayağa kaldırdık.  
             */
            #region ICatalogDatabaseSettings ve CatalogDatabaseSettings  
            services.Configure<CatalogDatabaseSettings>(Configuration.GetSection(nameof(CatalogDatabaseSettings)));
            services.AddSingleton<ICatalogDatabaseSettings>(cds => cds.GetRequiredService<IOptions<CatalogDatabaseSettings>>().Value);
            #endregion

            services.AddSingleton<ICatalogContext, CatalogContext>();
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<ICourseService, CourseManager>();
            #region Project Dependencies
            #endregion
            #region AutoMapper
            services.AddAutoMapper(typeof(Startup));
            #endregion
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OnlineCourse.Catalog v1"));
            }
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
