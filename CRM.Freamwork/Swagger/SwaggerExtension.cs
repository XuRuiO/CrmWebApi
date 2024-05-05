using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;

namespace CRM.Freamwork.Swagger
{
    /// <summary>
    /// Swagger 自定义配置
    /// </summary>
    public static class SwaggerExtension
    {
        /// <summary>
        /// 添加Swagger服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwaggerSetup(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("V1", new OpenApiInfo
                {
                    Version = "V1.1.0",
                    Title = "CRM.WebAdmin.Api",
                    Description = "CRM.WebAdmin.Api 框架",
                    Contact = new OpenApiContact
                    {
                        Name = "XuRuiO",
                        Email = "xhxy@live.cn",
                        Url = new Uri("https://github.com/XuRuiO")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "XuRuiO",
                        Url = new Uri("https://github.com/XuRuiO")
                    }
                });

                //添加生成Swagger的文档注释功能
                options.AddDocumentAnnotation();

                //添加header验证信息
                options.AddHeaderValidate();
            });

            return services;
        }

        /// <summary>
        /// 添加生成Swagger文档注释功能
        /// </summary>
        /// <param name="options"></param>
        private static void AddDocumentAnnotation(this SwaggerGenOptions options)
        {
            var basePath = AppContext.BaseDirectory;
            //var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;

            //获取CRM.WebAdmin.Api XML文档注释
            var xmlApiPath = Path.Combine(basePath, "CRM.WebAdmin.Api.xml");
            //默认的第二个参数是false，是否开启controller的注释
            options.IncludeXmlComments(xmlApiPath, true);

            //获取CRM.Model XML文档注释
            var xmlModelPath = Path.Combine(basePath, "CRM.Model.xml");
            options.IncludeXmlComments(xmlModelPath);
        }

        /// <summary>
        /// 添加header验证信息
        /// </summary>
        /// <param name="options"></param>
        private static void AddHeaderValidate(this SwaggerGenOptions options)
        {
            //方案名称“CRM.Core”可自定义，上下一致即可
            //注意：自定义的认证中间件，我们的Token可以不用带 Bearer 特定字符串（即不需要此格式：Bearer {token} ）;
            //      如果使用官方认证中间件，Token传递的时候，也必须带上"Bearer " 这样的特定字符串
            options.AddSecurityDefinition("CRM.Core", new OpenApiSecurityScheme
            {
                Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）",
                Name = "Authorization",     //jwt默认的参数名称
                In = ParameterLocation.Header,        //jwt默认存放Authorization信息的位置（请求头中）
                Type = SecuritySchemeType.ApiKey
            });
        }
    }
}
