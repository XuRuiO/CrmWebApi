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
    public partial class Role
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public Guid Id {get;set;}

        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name {get;set;}

        /// <summary>
        /// 启用状态： 0禁用 ，1启用（默认启用）
        /// </summary>
        public bool Enabled {get;set;}

        /// <summary>
        /// 删除状态： 0未删除（默认未删除），1已删除
        /// </summary>
        public bool Deleted {get;set;}

        /// <summary>
        /// 系统创建时间
        /// </summary>
        public DateTime Created {get;set;}

        /// <summary>
        /// 系统创建人
        /// </summary>
        public Guid Creator {get;set;}

        /// <summary>
        /// 系统修改时间
        /// </summary>
        public DateTime Modified {get;set;}

        /// <summary>
        /// 系统修改人
        /// </summary>
        public Guid Modifier {get;set;}
    }
}
