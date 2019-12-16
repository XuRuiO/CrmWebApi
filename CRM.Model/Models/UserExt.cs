using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CRM.Model.Models
{
    /// <summary>
    /// 用户信息表
    /// </summary>
    public partial class User
    {
        /// <summary>
        /// 性别
        /// </summary>
        public enum EnumSex
        {
            [Description("男")]
            Man,
            [Description("女")]
            Women
        }
    }
}
