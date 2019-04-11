using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Model.Models
{
    /// <summary>
    /// 用户表
    /// </summary>
    [SugarTable("T_Users")]
    public class UsersModel
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public System.Guid Id { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
    }
}
