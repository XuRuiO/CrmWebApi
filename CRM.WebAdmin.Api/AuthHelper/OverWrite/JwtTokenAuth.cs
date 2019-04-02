using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CRM.WebAdmin.Api.AuthHelper.OverWrite
{
    /// <summary>
    /// 2018.12.20      Rui     一个中间件，用来过滤每一个http请求，就是每当一个用户发送请求的时候，都先走这一步，然后再去访问http请求的接口
    /// </summary>
    public class JwtTokenAuth
    {
        //保存下一个中间件
        private readonly RequestDelegate _next;

        //在构造函数中传入下一个中间件
        public JwtTokenAuth(RequestDelegate next)
        {
            _next = next;
        }

        //中间件方法，接收HttpContext参数返回Task
        public Task Invoke(HttpContext httpContext)
        {
            //检测是否包含'Authorization' 请求头
            if (!httpContext.Request.Headers.ContainsKey("Authorization"))
            {
                return _next(httpContext);
            }
            var tokenHeader = httpContext.Request.Headers["Authorization"].ToString();
            //序列化token，获取授权
            TokenModelJWT modelJWT = JwtHelper.SerializeJWT(tokenHeader);

            //授权 注意这个可以添加多个角色声明，请注意这是一个 list
            var claimList = new List<Claim>();
            var claim = new Claim(ClaimTypes.Role, modelJWT.Role);
            claimList.Add(claim);
            var identity = new ClaimsIdentity(claimList);
            var principal = new ClaimsPrincipal(identity);
            httpContext.User = principal;

            //调用下一个中间件
            return _next(httpContext);
        }
    }
}
