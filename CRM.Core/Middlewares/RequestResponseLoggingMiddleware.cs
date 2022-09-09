using CRM.Core.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Core.Middlewares
{
    /// <summary>
    /// 2020.03.13      请求响应日志记录中间件，api中产生的请求和响应数据记录日志
    /// </summary>
    public class RequestResponseLoggingMiddleware
    {
        //保存下一个中间件
        private readonly RequestDelegate next;
        private readonly ILogger<RequestResponseLoggingMiddleware> logger;
        private Stopwatch stopwatch;
        private SortedDictionary<string, object> data;

        //构造函数注入
        public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
        {
            //在构造函数中传入下一个中间件
            this.next = next;
            this.logger = logger;
            this.stopwatch = new Stopwatch();
        }

        public async Task Invoke(HttpContext context)
        {
            stopwatch.Restart();
            data = new SortedDictionary<string, object>();

            HttpRequest request = context.Request;
            data.Add("request.url", request.Path.ToString());
            data.Add("request.headers", request.Headers.ToDictionary(x => x.Key, v => string.Join(";", v.Value.ToList())));
            data.Add("request.method", request.Method);
            data.Add("request.executeStartTime", DateTimeOffset.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));

            // 获取请求body内容
            if (request.Method.ToLower().EqualsByOIC("post"))
            {
                // 启用倒带功能，就可以让 Request.Body 可以再次读取
                request.EnableBuffering();

                // 升级3.0以上，不允许同步操作，必须异步
                using var reader = new StreamReader(request.Body, Encoding.UTF8);
                var body = await reader.ReadToEndAsync();
                data.Add("request.body", body);
                request.Body.Position = 0;
            }
            else if (request.Method.ToLower().EqualsByOIC("get"))
            {
                data.Add("request.body", request.QueryString.Value);
            }

            // 获取Response.Body内容
            var originalBodyStream = context.Response.Body;

            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;

                await next(context);

                data.Add("response.body", await GetResponse(context.Response));
                data.Add("response.executeEndTime", DateTimeOffset.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));

                await responseBody.CopyToAsync(originalBodyStream);
            }

            // 响应完成记录时间和存入日志
            context.Response.OnCompleted(() =>
            {
                stopwatch.Stop();
                data.Add("elaspedTime", stopwatch.ElapsedMilliseconds + "ms");
                var json = JsonConvert.SerializeObject(data);
                logger.LogInformation(json);
                return Task.CompletedTask;
            });
        }

        /// <summary>
        /// 获取响应内容
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public async Task<string> GetResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);
            return text;
        }
    }
}
