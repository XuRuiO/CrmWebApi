﻿using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Model.Models
{
    /// <summary>
    /// 用户信息表
    /// </summary>
    [SugarTable("T_User")]
    public class UserModel
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string Name { get; set; }
    }
}
