using CRM.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Repository.SqlSugarOrm
{
    /// <summary>
    /// 2019.02.23      Rui     获取数据库连接字符串
    /// </summary>
    public class DbConfig
    {
        /// <summary>
        /// SqlServer 数据库连接字符串
        /// </summary>
        public static string ConnectionString = ConfigsHelper.GetSqlServerConnection();
    }
}
