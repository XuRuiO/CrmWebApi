using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace CRM.Model.Models
{
    ///<summary>
    ///用户角色信息表
    ///</summary>
    [SugarTable("T_UserRole")]
    public partial class UserRoleModel
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public Guid Id {get;set;}

        /// <summary>
        /// 用户主键Id
        /// </summary>
        public Guid? UserId {get;set;}

        /// <summary>
        /// 角色主键Id
        /// </summary>
        public Guid? RoleId {get;set;}
    }
}
