using System;
using System.Collections.Generic;
using System.Text;
using CRM.Core.Models;

namespace CRM.Core.CustomExtensions
{
    /// <summary>
    /// 2019.08.22      Rui     自定义异常返回处理
    /// </summary>
    public class CustomerException : Exception
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public ApiResponseStatusCode apiResponseStatusCode;

        /// <summary>
        /// 是否记录日志
        /// </summary>
        public bool IsWriteLog { get; set; }

        public CustomerException(string errorMsg, ApiResponseStatusCode apiResponseStatusCode, bool isWriteLog = false)
            : base(errorMsg)
        {
            this.apiResponseStatusCode = apiResponseStatusCode;
            this.IsWriteLog = isWriteLog;
        }
    }
}
