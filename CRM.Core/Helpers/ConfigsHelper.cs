using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Helpers
{
    /// <summary>
    /// 2019.04.10      Rui     配置文件帮助类
    /// 说明：存放appsetting.json中AppSettings中的配置文件
    /// </summary>
    public class ConfigsHelper
    {
        /// <summary>
        /// 获取SqlServer数据库连接字符串
        /// </summary>
        /// <returns></returns>
        public static string GetSqlServerConnection()
        {
            return AppSettingsHelper.AppSetting("SqlServerConnection");
        }

        /// <summary>
        /// 获取是否启用SqlSugar 打印sql日志功能
        /// </summary>
        /// <returns></returns>
        public static bool GetIsEnableSqlSugarLog()
        {
            return TypeConversionHelper.StringToBool(AppSettingsHelper.AppSetting("IsEnableSqlSugarLog"));
        }
    }
}
