using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Model.ViewModels
{
    /// <summary>
    /// 用户信息视图，返回给前台页面展示
    /// </summary>
    public class UserInfoView
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 角色名称（多个角色的话，拼接起来）
        /// </summary>
        public string RoleName { get; set; }
    }
}
