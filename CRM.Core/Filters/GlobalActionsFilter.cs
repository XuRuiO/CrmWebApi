using CRM.Core.Helpers;
using CRM.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Filters
{
    /// <summary>
    /// 2020.03.12      Rui      全局方法过滤器
    /// </summary>
    public class GlobalActionsFilter: ActionFilterAttribute
    {
        //Action之前执行
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                //定义无返回值的错误信息描述
                var response = new VoidResult { StatusCode = (int)ApiResponseStatusCode.ParameterError };

                foreach (var item in context.ModelState.Values)
                {
                    foreach (var error in item.Errors)
                    {
                        if (!response.Message.IsNullOrEmpty())
                        {
                            response.Message += "|";
                        }
                        response.Message += error.ErrorMessage;
                    }
                }

                //Json序列化配置，取消默认驼峰
                var serializerSettings = new JsonSerializerSettings();
                serializerSettings.ContractResolver = new DefaultContractResolver();

                context.Result = new JsonResult(response, serializerSettings);
            }
        }

        //Action之后执行
        public override void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
