using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace CRM.Model.Models
{
    ///<summary>
    ///路由菜单信息表
    ///</summary>
    [SugarTable("T_Permission")]
    public partial class Permission
    {
        public Permission()
        {
            this.ParentId = Convert.ToInt32("0");
            this.IsHidden = false;
            this.IsAffix = false;
            this.IsUseLayout = true;
            this.TerminalType = Convert.ToInt32("4");
            this.Enabled = Convert.ToInt32("1");
            this.Deleted = Convert.ToInt32("0");
            this.Created = DateTime.Now;
            this.Modified = DateTime.Now;
        }
        /// <summary>
        /// 主键Id
        /// </summary>        
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        /// <summary>
        /// 父Id
        /// </summary>        
        public int? ParentId { get; set; }

        /// <summary>
        /// 路由地址
        /// </summary>        
        public string Path { get; set; }

        /// <summary>
        /// 重定向地址（输入父路径，跳转到指定的子路劲）
        /// </summary>        
        public string Redirect { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>        
        public string Title { get; set; }

        /// <summary>
        /// 菜单图标
        /// </summary>        
        public string Icon { get; set; }

        /// <summary>
        /// 是否隐藏菜单在侧边导航栏出现
        /// </summary>        
        public bool? IsHidden { get; set; }

        /// <summary>
        /// 是否固钉在tagsView中，不可被删除
        /// </summary>        
        public bool? IsAffix { get; set; }

        /// <summary>
        /// 是否使用Layout布局，基本上父导航为True
        /// </summary>        
        public bool? IsUseLayout { get; set; }

        /// <summary>
        /// 排序序号
        /// </summary>        
        public int? OrderSort { get; set; }

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
