using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Helpers
{
    /// <summary>
    /// 2018.12.20      Rui     扩展方法帮助类
    /// </summary>
    public static class ExtensionMethodsHelper
    {
        /// <summary>
        /// object 转换成 Int
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static int ObjToInt(this object thisValue)
        {
            int reval = 0;
            if (thisValue == null)
            {
                return 0;
            }
            if (thisValue != null && thisValue != DBNull.Value && int.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return reval;
        }

        /// <summary>
        /// object 转换成 String
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static string ObjToString(this object thisValue)
        {
            if (thisValue != null)
            {
                return thisValue.ToString().Trim();
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
