using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Helpers
{
    /// <summary>
    /// 2019.04.11      Rui     类型转换帮助类
    /// </summary>
    public class TypeConversionHelper
    {
        /// <summary>
        /// 将指定字符串转换为bool类型（true或false）
        /// </summary>
        /// <param name="str">目标字符串</param>
        /// <param name="defaultValue">如果都不符合，默认返回false</param>
        /// <returns></returns>
        public static bool StringToBool(string str, bool defaultValue = false)
        {
            if (str.ToLower().Equals("true", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            if (str.ToLower().Equals("false", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
            return defaultValue;
        }
    }
}
