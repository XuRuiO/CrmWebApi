using System;
using System.Collections.Generic;
using System.Text;
using CRM.Core.CustomExtensions;
using CRM.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CRM.Core.Filters
{
    /// <summary>
    /// 2019.08.13      Rui     全局异常错误日志记录
    /// </summary>
    public class GlobalExceptionsFilter : ExceptionFilterAttribute
    {
        private ILogger<GlobalExceptionsFilter> logger;

        //构造函数注入
        public GlobalExceptionsFilter(ILogger<GlobalExceptionsFilter> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// 重写发生异常时，执行得方法
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(ExceptionContext context)
        {
            //定义无返回值的错误信息描述
            var response = new VoidResult();

            if (context.Exception is CustomerException exception)
            {
                response.StatusCode = (int)exception.apiResponseStatusCode;
                response.Message = exception.Message;

                //是否记录日志
                if (exception.IsWriteLog)
                {
                    //记录异常日志格式
                    string exceptionLog = string.Format(" \r\n【异常类型】：{0} \r\n【异常信息】：{1} \r\n【堆栈调用】：{2}",
                        new object[] { context.Exception.GetType().Name, context.Exception.Message, context.Exception.StackTrace });

                    logger.LogError($"WebApi 发生异常 message：{exceptionLog}");
                }
            }
            else
            {
                response.StatusCode = (int)ApiResponseStatusCode.UnExpectError;
                response.Message = "系统繁忙，稍后重试！";

                //记录异常日志格式
                string exceptionLog = string.Format(" \r\n【异常类型】：{0} \r\n【异常信息】：{1} \r\n【堆栈调用】：{2}",
                    new object[] { context.Exception.GetType().Name, context.Exception.Message, context.Exception.StackTrace });

                logger.LogError($"WebApi 发生异常 message：{exceptionLog}");
            }

            //Json序列化配置，取消默认驼峰
            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new DefaultContractResolver();

            //将异常信息返回
            context.Result = new JsonResult(response, serializerSettings);
        }
    }
}
