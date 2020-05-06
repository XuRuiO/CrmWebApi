using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace CRM.Model.Models
{
    ///<summary>
    ///客户信息表
    ///</summary>
    [SugarTable("T_Customer")]
    public partial class Customer
    {
        public Customer()
        {
            this.TerminalType = Convert.ToInt32("4");
            this.Enabled = Convert.ToInt32("1");
            this.Deleted = Convert.ToInt32("0");
            this.Created = DateTime.Now;
            this.Modified = DateTime.Now;
        }
        /// <summary>
        /// 
        /// </summary>        
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>        
        public string Name { get; set; }

        /// <summary>
        /// 请求接口的终端类型：1（Ios），2（Android），3（WeChat）,4（PC）
        /// </summary>        
        public int? TerminalType { get; set; }

        /// <summary>
        /// 启用状态： 0禁用 ，1启用（默认启用）
        /// </summary>        
        public int? Enabled { get; set; }

        /// <summary>
        /// 删除状态： 0未删除（默认未删除），1已删除
        /// </summary>        
        public int? Deleted { get; set; }

        /// <summary>
        /// 系统创建时间
        /// </summary>        
        public DateTime? Created { get; set; }

        /// <summary>
        /// 系统创建人
        /// </summary>        
        public int? Creator { get; set; }

        /// <summary>
        /// 系统修改时间
        /// </summary>        
        public DateTime? Modified { get; set; }

        /// <summary>
        /// 系统修改人
        /// </summary>        
        public int? Modifier { get; set; }
    }
}
