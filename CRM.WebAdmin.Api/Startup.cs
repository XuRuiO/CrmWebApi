using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Freamwork.GlobalRouting;
using CRM.Freamwork.Swagger;
using CRM.WebAdmin.Api.AuthHelper.OverWrite;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CRM.WebAdmin.Api
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
            //注册Mvc到Container
            services.AddMvc(options =>
            {
                //路由参数在此处仍然是有效的，比如添加一个版本号
                options.UseCentralRoutePrefix(new RouteAttribute("v1"));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            #region 2018.11.11      Rui     添加Swagger自定义配置

            services.AddSwaggerGenCRM();

            #endregion 2018.11.11      Rui     添加Swagger自定义配置

            #region 2018.12.11      Rui     Token服务注册

            services.AddSingleton<IMemoryCache>(factory =>
            {
                var cache = new MemoryCache(new MemoryCacheOptions());
                return cache;
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Client", policy => policy.RequireRole("Client").Build());
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin").Build());
                options.AddPolicy("AdminOrClient", policy => policy.RequireRole("Admin,Client").Build());
            });

            #endregion 2018.12.11      Rui     Token服务注册
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //在开发环境中，使用异常页面，这样可以暴漏错误堆栈信息，所以不要放在生产环境
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            #region 2018.11.11      Rui     使用Swagger

            app.UseSwagger();
            app.UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint("/swagger/V1/swagger.json", "CRM API V1");
                //如果想直接在域名的根目录直接加载 swagger 比如访问：localhost:8001 就能访问，可以这样设置：
                /*option.RoutePrefix = "";*/     //路径配置，设置为空，表示直接访问该文件
                //路径配置，设置为空，表示直接在根域名（localhost:8001）访问该文件,注意localhost:8001/swagger是访问不到的，
                //这个时候去launchSettings.json中把"launchUrl": "swagger/index.html"去掉， 然后直接访问localhost:8001/index.html即可
            });

            #endregion 2018.11.11      Rui     使用Swagger

            #region 2018.12.20      Rui     使用JwtTokenAuth中间件

            //使用自定义的认证中间件
            app.UseMiddleware<JwtTokenAuth>();

            #endregion

            //返回错误码
            app.UseStatusCodePages();

            //使用Mvc中间件
            app.UseMvc();
        }
    }
}
