using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Core.Filters;
using CRM.Core.Helpers;
using CRM.Freamwork.Autofac;
using CRM.Freamwork.Cache.MemoryCache;
using CRM.Freamwork.Cache.RedisCache;
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
        //构造函数 Startup  Core的核心是依赖注入  所以要有构造函数进行注入 
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            #region 2019.04.10      Rui     获取appsettings.json中自定义节点AppSettings的配置文件

            AppSettingsHelper.SetSection(Configuration.GetSection("AppSettings"));

            #endregion

            #region 2019.06.11      Rui     初始化CsRedis Sdk服务

            if (ConfigsHelper.GetRedisCacheEnabled())
            {
                RedisCacheExtension.Initialization();
            }

            #endregion
        }

        //承载注入实现的对象 IConfiguration
        public IConfiguration Configuration { get; }

        //添加服务的方法 ConfigureServices，主要实现了依赖注入(DI)的配置
        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //注册Mvc到Container
            services.AddMvc(options =>
            {
                //路由参数在此处仍然是有效的，比如添加一个版本号，不需要可以注释
                //options.UseCentralRoutePrefix(new RouteAttribute("v1"));

                //注入全局异常捕获
                options.Filters.Add(typeof(GlobalExceptionsFilter));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            #region 2019.08.11      Rui     部分服务注入，netcore自带方法

            //内存缓存注入
            services.AddScoped<IMemoryCacheExtension, MemoryCacheExtension>();
            services.AddSingleton<IMemoryCache>(factory =>
            {
                var cache = new MemoryCache(new MemoryCacheOptions());
                return cache;
            });

            #endregion

            #region 2019.06.14      Rui     CORS跨域配置，声明策略

            //声明策略，记得下边app中配置
            services.AddCors(x =>
            {
                //方法一
                //↓↓↓↓↓↓↓注意正式环境不要使用这种全开放的处理↓↓↓↓↓↓↓↓↓↓
                //x.AddPolicy("AllRequests", policy =>
                //{
                //    policy
                //    .AllowAnyOrigin()//允许任何源
                //    .AllowAnyMethod()//允许任何方式
                //    .AllowAnyHeader()//允许任何头
                //    .AllowCredentials();//允许cookie
                //});
                //↑↑↑↑↑↑↑注意正式环境不要使用这种全开放的处理↑↑↑↑↑↑↑↑↑↑

                //方法二，一般采用这种方法
                x.AddPolicy("LimitRequests", policy =>
                {
                    policy
                    .WithOrigins("http://127.0.0.1:1002", "http://localhost:1002")   //支持多个域名端口，注意端口号后不要带/斜杆：比如localhost:8000/，是错的
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            #endregion

            #region 2018.11.11      Rui     添加Swagger自定义配置

            services.AddSwaggerGenCRM();

            #endregion

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

            #endregion

            #region 2019.05.27      Rui     依赖注入Autofac

            //让Autofac接管Starup中的ConfigureServices方法，记得修改返回类型IServiceProvider
            return services.AddAutofacCRM();

            #endregion
        }

        //主要是http处理管道配置和一些系统配置
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
                //生产环境，发生错误跳转错误页面
                app.UseExceptionHandler("/Error");

                //生产环境中，使用HTTPS严格安全传输(or HSTS) 对于保护web安全是非常重要的。强制实施 HTTPS 在 ASP.NET Core，配合 app.UseHttpsRedirection。
                //app.UseHsts();
            }

            #region 2019.06.14      Rui     使用CORS中间件

            //跨域第一种方法
            //app.UseCors(options => options.WithOrigins("http://localhost:8021").AllowAnyHeader().AllowAnyMethod());

            //跨域第二种方法，将 CORS 中间件添加到 web 应用程序管线中, 以允许跨域请求。
            app.UseCors("LimitRequests");

            #endregion

            #region 2018.11.11      Rui     使用Swagger中间件

            app.UseSwagger();
            app.UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint("/swagger/V1/swagger.json", "CRM API V1");
                //如果想直接在域名的根目录直接加载 swagger 比如访问：localhost:8001 就能访问，可以这样设置：
                option.RoutePrefix = "";     //路径配置，设置为空，表示直接访问该文件
                //路径配置，设置为空，表示直接在根域名（localhost:8001）访问该文件,注意localhost:8001/swagger是访问不到的，
                //这个时候去launchSettings.json中把"launchUrl": "swagger/index.html"去掉， 然后直接访问localhost:8001/index.html即可
            });

            #endregion

            #region 2018.12.20      Rui     使用JwtTokenAuth中间件

            //使用自定义的认证中间件
            app.UseMiddleware<JwtTokenAuth>();

            #endregion

            //使用状态错误码中间件，把错误码返回前台，比如是404
            app.UseStatusCodePages();

            //使用Mvc中间件
            app.UseMvc();
        }
    }
}
