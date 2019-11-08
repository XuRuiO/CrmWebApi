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
    public partial class UserModel
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public Guid Id {get;set;}

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name {get;set;}
    }
}
