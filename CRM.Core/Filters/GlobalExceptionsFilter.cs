using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CRM.Core.Filters
{
    /// <summary>
    /// 2019.08.13      Rui     全局异常错误日志记录
    /// </summary>
    public class GlobalExceptionsFilter : ExceptionFilterAttribute
    {
        //private ILogger<GlobalExceptionsFilter> logger;

        ////构造函数注入
        //public GlobalExceptionsFilter(ILogger<GlobalExceptionsFilter> logger)
        //{
        //    this.logger = logger;
        //}

        /// <summary>
        /// 重写发生异常时，执行得方法
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(ExceptionContext context)
        {
            var logger = context.HttpContext.RequestServices.GetService<ILogger<GlobalExceptionsFilter>>();

            string str = string.Format("【自定义错误】：{0} \r\n【异常类型】：{1} \r\n【异常信息】：{2} \r\n【堆栈调用】：{3}",
                new object[] { context.Exception.Message, context.Exception.GetType().Name, context.Exception.Message, context.Exception.StackTrace });

            logger.LogError(str);
        }
    }
}
