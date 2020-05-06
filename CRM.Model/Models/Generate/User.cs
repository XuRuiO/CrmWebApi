using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace CRM.Model.Models
{
    ///<summary>
    ///用户信息表
    ///</summary>
    [SugarTable("T_User")]
    public partial class User
    {
        public User()
        {
            this.LastLoginDate = DateTime.Now;
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
        /// 用户名
        /// </summary>        
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>        
        public string Password { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>        
        public string Nick { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>        
        public string Name { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>        
        public int? Age { get; set; }

        /// <summary>
        /// 性别: Man（男），Women（女）
        /// </summary>        
        public string Sex { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>        
        public string Mobile { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>        
        public string IdentityCard { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>        
        public string Email { get; set; }

        /// <summary>
        /// 省份Id
        /// </summary>        
        public int? ProvinceId { get; set; }

        /// <summary>
        /// 市Id
        /// </summary>        
        public int? CityId { get; set; }

        /// <summary>
        /// 地区Id
        /// </summary>        
        public int? RegionId { get; set; }

        /// <summary>
        /// 街道地址
        /// </summary>        
        public string Address { get; set; }

        /// <summary>
        /// 头像
        /// </summary>        
        public string HeadSculpture { get; set; }

        /// <summary>
        /// 最后登录日期
        /// </summary>        
        public DateTime? LastLoginDate { get; set; }

        /// <summary>
        /// 角色Id，多个角色用 , 分开
        /// </summary>        
        public string RoleId { get; set; }

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
