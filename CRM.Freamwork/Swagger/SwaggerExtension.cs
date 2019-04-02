using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CRM.Freamwork.Swagger
{
    /// <summary>
    /// 2018.10.22  Rui
    /// Swagger 自定义配置
    /// </summary>
    public static class SwaggerExtension
    {
        /// <summary>
        /// 添加Swagger服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwaggerGenCRM(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("V1", new Info
                {
                    Version = "V1.1.0",
                    Title = "CRM.WebAdmin.Api",
                    Description = "CRM.WebAdmin.Api 框架",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "XuRuiO", Email = "xhxy@live.cn", Url = "https://github.com/XuRuiO" }
                });

                //添加生成Swagger文档注释功能
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
        public static void AddDocumentAnnotation(this SwaggerGenOptions options)
        {
            var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;

            //获取CRM.WebAdmin.Api XML文档注释
            var xmlApiPath = Path.Combine(basePath, "CRM.WebAdmin.Api.xml");
            //默认的第二个参数是false，这个是controller的注释，记得修改
            options.IncludeXmlComments(xmlApiPath, true);

            //获取CRM.Model.xml XML文档注释
            //var xmlModelPath = Path.Combine(basePath, "CRM.Model.xml");
            ////默认的第二个参数是false，这个是controller的注释，记得修改
            //options.IncludeXmlComments(xmlModelPath);
        }

        /// <summary>
        /// 添加header验证信息
        /// </summary>
        /// <param name="options"></param>
        public static void AddHeaderValidate(this SwaggerGenOptions options)
        {
            var security = new Dictionary<string, IEnumerable<string>> { { "CRM.Core", new string[] { } } };
            options.AddSecurityRequirement(security);
            //方案名称“CRM.Core”可自定义，上下一致即可
            //注意：自定义的认证中间件，我们的Token可以不用带 Bearer 特定字符串（即不需要此格式：Bearer {token} ）;
            //      如果使用官方认证中间件，Token传递的时候，也必须带上"Bearer " 这样的特定字符串
            options.AddSecurityDefinition("CRM.Core", new ApiKeyScheme
            {
                Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入 {token} ",
                Name = "Authorization",     //jwt默认的参数名称
                In = "header",        //jwt默认存放Authorization信息的位置（请求头中）
                Type = "apiKey"
            });
        }
    }
}
