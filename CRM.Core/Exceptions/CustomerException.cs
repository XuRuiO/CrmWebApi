using System;
using System.Collections.Generic;
using System.Text;
using CRM.Core.Models;

namespace CRM.Core.Exceptions
{
    /// <summary>
    /// 2019.08.22      Rui     自定义异常返回处理
    /// </summary>
    public class CustomerException : Exception
    {
        public ApiResponseStatusCode apiResponseStatusCode;

        public CustomerException(string errorMsg, ApiResponseStatusCode apiResponseStatusCode)
            : base(errorMsg)
        {
            this.apiResponseStatusCode = apiResponseStatusCode;
        }
    }
}
