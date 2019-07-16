using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Model.ViewPageModels
{
    /// <summary>
    /// 基类查询分页模型（请求）
    /// </summary>
    public class BaseQueryPageModel
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
