using CRM.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Repository.SqlSugarOrm
{
    public class DbConfig
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public static string ConnectionString = ConfigsHelper.GetSqlServerConnection();
    }
}
