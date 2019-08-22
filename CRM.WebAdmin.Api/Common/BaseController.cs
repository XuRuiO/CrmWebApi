using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Core.Models;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;

namespace CRM.WebAdmin.Api.Common
{
    /// <summary>
    /// 2019.08.21      Rui     WebApi控制器基类
    /// </summary>
    public class BaseController : ControllerBase
    {
        #region 2019.08.21      Rui     请求成功或失败，返回数据格式

        /// <summary>
        /// 请求成功，无返回值
        /// </summary>
        /// <param name="Message">返回信息</param>
        /// <returns></returns>
        protected VoidResult Success(string Message)
        {
            var response = new VoidResult
            {
                StatusCode = (int)ApiResponseStatusCode.Success,
                Message = Message
            };
            return response;
        }

        /// <summary>
        /// 请求成功，有返回值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Message">返回信息</param>
        /// <param name="Data">返回数据</param>
        /// <returns></returns>
        protected Result<T> Success<T>(string Message, T Data)
        {
            var response = new Result<T>
            {
                StatusCode = (int)ApiResponseStatusCode.Success,
                Message = Message,
                Data = Data
            };
            return response;
        }

        /// <summary>
        /// 请求失败，无返回值
        /// </summary>
        /// <param name="StatusCode">错误状态码</param>
        /// <param name="Message">返回信息</param>
        /// <returns></returns>
        protected VoidResult Error(int StatusCode, string Message)
        {
            var response = new VoidResult
            {
                StatusCode = StatusCode,
                Message = Message
            };
            return response;
        }

        /// <summary>
        /// 请求失败，无返回值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="StatusCode">错误状态码</param>
        /// <param name="Message">返回信息</param>
        /// <returns></returns>
        protected Result<T> Error<T>(int StatusCode, string Message)
        {
            var response = new Result<T>
            {
                StatusCode = StatusCode,
                Message = Message,
                Data = default
            };
            return response;
        }

        #endregion
    }
}
