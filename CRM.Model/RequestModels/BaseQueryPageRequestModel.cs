using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Model.RequestModels
{
    /// <summary>
    /// 基类查询分页模型（请求）,需要分页查询的请继承该类
    /// </summary>
    public class BaseQueryPageRequestModel : BaseRequestModel
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 页数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalCount { get; set; }
    }
}
