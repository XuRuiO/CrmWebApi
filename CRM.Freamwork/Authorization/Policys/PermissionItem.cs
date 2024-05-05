using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Freamwork.Authorization.Policys
{
    /// <summary>
    /// 权限许可项实体
    /// </summary>
    public class PermissionItem
    {
        /// <summary>
        /// 用户名称
        /// </summary>
        public virtual string UserName { get; set; }

        /// <summary>
        /// 用户角色
        /// </summary>
        public virtual string Role { get; set; }

        /// <summary>
        /// 请求Url
        /// </summary>
        public virtual string Url { get; set; }
    }
}
