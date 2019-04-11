using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Helpers
{
    /// <summary>
    /// 2019.04.10      Rui     appsettings.json操作类
    /// </summary>
    public class AppSettingsHelper
    {
        private static IConfigurationSection _section = null;

        /// <summary>
        /// 根据指定Key返回配置文件
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string AppSetting(string key)
        {
            string str = "";
            if (_section.GetSection(key) != null)
            {
                str = _section.GetSection(key).Value;
            }
            return str;
        }

        /// <summary>
        /// 设置部分节点配置文件
        /// </summary>
        /// <param name="section"></param>
        public static void SetSection(IConfigurationSection section)
        {
            _section = section;
        }
    }
}
