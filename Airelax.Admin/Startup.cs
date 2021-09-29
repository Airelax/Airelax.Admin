using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Airelax.EntityFramework;
using Airelax.EntityFramework.DbContexts;
using Lazcat.Infrastructure.ExceptionHandlers;
using Lazcat.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Define = Airelax.Admin.Defines.Define;

namespace Airelax.Admin
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostEnvironment hostEnvironment)
        {
            Configuration = configuration;
            HostEnvironment = hostEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IHostEnvironment HostEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connectString;
            
            if (HostEnvironment.IsDevelopment())
            {
                connectString = Define.Database.LOCAL_CONNECT_STRING;
                connectString = Define.Database.DB_CONNECT_STRING;
                services.AddCors(opt => { opt.AddPolicy("dev", builder => builder.WithOrigins("http://localhost:8080").AllowCredentials().AllowAnyHeader()); });
            }
            else
            {
                connectString = Define.Database.DB_CONNECT_STRING;
                //todo remove product
                //connectString = Define.Database.LOCAL_CONNECT_STRING;
            }
            // dotnet ef --startup-project Airelax migrations add $description -p Airelax.EntityFramework
            // 更新資料庫 dotnet ef --startup-project Airelax database update -p Airelax.EntityFramework

            //if use local DB
            services.AddDbContext<AirelaxContext>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString(connectString),
                    x =>
                    {
                        x.MigrationsAssembly(Define.Database.ENTITY_FRAMEWORK);
                        x.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    })
            );

            services.AddByDependencyInjectionAttribute();
            services.AddControllersWithViews();
            services.AddSwaggerDocument();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseExceptionHandler(builder => builder.Run(async context => await ExceptionHandler.HandleError(context)));
            app.UseStaticFiles();

            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}