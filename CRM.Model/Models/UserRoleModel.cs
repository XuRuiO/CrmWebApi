using System;
using System.Collections.Generic;
using System.Text;
using SqlSugar;

namespace CRM.Model.Models
{
    [SugarTable("T_UserRole")]
    public class UserRoleModel
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 用户主键Id
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 角色主键Id
        /// </summary>
        public Guid RoleId { get; set; }
    }
}
