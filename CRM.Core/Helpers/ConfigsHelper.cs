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
        /// 获取是否启用SqlSugar打印sql日志功能
        /// </summary>
        /// <returns></returns>
        public static bool GetIsEnableSqlSugarLog()
        {
            return TypeConversionHelper.StringToBool(AppSettingsHelper.AppSetting("IsEnableSqlSugarLog"));
        }

        /// <summary>
        /// 获取AutoFac注入的应用程序集名称
        /// </summary>
        /// <returns></returns>
        public static string GetAutoFacAssemblyName()
        {
            return AppSettingsHelper.AppSetting("AutoFacAssemblyName");
        }

        /// <summary>
        /// 获取是否启用RedisCache功能
        /// </summary>
        /// <returns></returns>
        public static bool GetRedisCacheEnabled()
        {
            return TypeConversionHelper.StringToBool(AppSettingsHelper.AppSetting("Cache:RedisCache:Enabled"));
        }

        /// <summary>
        /// 获取RedisCache连接字符串
        /// </summary>
        /// <returns></returns>
        public static string GetRedisCacheConnectionString()
        {
            return AppSettingsHelper.AppSetting("Cache:RedisCache:ConnectionString");
        }

        /// <summary>
        /// 获取JWT(playload配置),接收jwt的一方
        /// </summary>
        /// <returns></returns>
        public static string GetJwtAudienceAud()
        {
            return AppSettingsHelper.AppSetting("Audience:Audience");
        }

        /// <summary>
        /// 获取JWT(playload配置),jwt的签发者
        /// </summary>
        /// <returns></returns>
        public static string GetJwtAudienceIssuer()
        {
            return AppSettingsHelper.AppSetting("Audience:Issuer");
        }

        /// <summary>
        /// 获取jwt加密的密钥
        /// </summary>
        /// <returns></returns>
        public static string GetJwtAudienceSecret()
        {
            return AppSettingsHelper.AppSetting("Audience:Secret");
        }
    }
}
