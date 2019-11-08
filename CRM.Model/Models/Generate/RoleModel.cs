using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace CRM.Model.Models
{
    ///<summary>
    ///角色信息表
    ///</summary>
    [SugarTable("T_Role")]
    public partial class RoleModel
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public Guid Id {get;set;}

        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name {get;set;}
    }
}
