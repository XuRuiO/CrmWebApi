using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Model.ViewModels
{
    public class UserRoleView
    {
        /// <summary>
        /// 用户主键Id
        /// </summary>
        public Guid UserModelId { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserModelName { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleModelName { get; set; }
    }
}
