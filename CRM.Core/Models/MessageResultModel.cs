using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CRM.Core.Models
{
    /// <summary>
    /// 有返回值
    /// </summary>
    /// <typeparam name="T">需要返回的泛型数据</typeparam>
    public struct Result<T>
    {
        /// <summary>
        /// 返回状态码
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public T Data { get; set; }
    }

    /// <summary>
    /// 无返回值
    /// </summary>
    public struct VoidResult
    {
        /// <summary>
        /// 返回状态码
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        public string Message { get; set; }
    }

    /// <summary>
    /// 返回状态码
    /// </summary>
    public enum ApiResponseStatusCode
    {
        /// <summary>
        /// 服务器已成功处理了请求
        /// </summary>
        [Description("服务器已成功处理了请求")]
        Success = 200,

        /// <summary>
        /// 当前版本有更新
        /// </summary>
        [Description("当前版本有更新")]
        Upgrade = 300,

        /// <summary>
        /// 请求参数不完整或不正确
        /// </summary>
        [Description("请求参数不完整或不正确")]
        ParameterError = 400,

        /// <summary>
        /// 未授权标识，要求身份验证
        /// </summary>
        [Description("未授权标识，要求身份验证")]
        Unauthorized = 401,

        /// <summary>
        /// 请求TOKEN失效
        /// </summary>
        [Description("请求TOKEN失效")]
        TokenInvalid = 403,

        /// <summary>
        /// HTTP请求类型不合法
        /// </summary>
        [Description("HTTP请求类型不合法")]
        HttpMehtodError = 405,

        /// <summary>
        /// HTTP请求不合法,请求参数可能被篡改
        /// </summary>
        [Description("HTTP请求不合法,请求参数可能被篡改")]
        HttpRequestError = 406,

        /// <summary>
        /// 该URL已经失效
        /// </summary>
        [Description("该URL已经失效")]
        URLExpireError = 407,

        /// <summary>
        /// 内部业务请求出错
        /// </summary>
        [Description("内部业务请求出错")]
        Error = 500,

        /// <summary>
        /// 系统预期外错误
        /// </summary>
        [Description("系统预期外错误")]
        UnExpectError = 999
    }
}
