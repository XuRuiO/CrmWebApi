using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace CRM.Core.Helpers
{
    /// <summary>
    /// 2018.12.20      Rui     扩展方法帮助类
    /// </summary>
    public static class ExtensionMethodsHelper
    {
        #region 类型转换的扩展方法

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

        /// <summary>
        /// 将指定字符串转换为bool类型（true或false）
        /// </summary>
        /// <param name="str">目标字符串</param>
        /// <param name="defaultValue">如果都不符合，默认返回false</param>
        /// <returns></returns>
        public static bool StringToBool(this string thisValue, bool defaultValue = false)
        {
            if (thisValue.ToLower().Equals("true", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            if (thisValue.ToLower().Equals("false", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
            return defaultValue;
        }

        /// <summary>
        /// 对象转json的扩展方法
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(this object thisValue)
        {
            //转换System.DateTime的日期格式到 ISO 8601日期格式
            //ISO 8601 (如2008-04-12T12:53Z)
            IsoDateTimeConverter isoDateTimeConverter = new IsoDateTimeConverter();
            //设置日期格式
            isoDateTimeConverter.DateTimeFormat = "yyyy-MM-dd hh:mm:ss";
            //序列化
            return JsonConvert.SerializeObject(thisValue, isoDateTimeConverter);
        }

        /// <summary>
        /// 解析JSON字符串，生成对象实体
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json字符串(eg.{"ID":"112","Name":"石子儿"})</param>
        /// <returns>对象实体</returns>
        public static T DeserializeJsonToObject<T>(this string thisValue) where T : class
        {
            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(thisValue);
            object o = serializer.Deserialize(new JsonTextReader(sr), typeof(T));
            T t = o as T;
            return t;
        }

        /// <summary>
        /// 解析JSON数组对象，生成对象实体集合
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json数组字符串(eg.[{"ID":"1","Name":"lttr"}])</param>
        /// <returns>对象实体集合</returns>
        public static List<T> DeserializeJsonToList<T>(this string thisValue) where T : class
        {
            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(thisValue);
            object o = serializer.Deserialize(new JsonTextReader(sr), typeof(List<T>));
            List<T> list = o as List<T>;
            return list;
        }

        #endregion

        #region 枚举获取的扩展方法

        /// <summary>
        /// 获取枚举项描述信息，Description
        /// </summary>
        /// <param name="value">枚举项，如EnumSex.Man</param>
        /// <returns></returns>
        public static string GetEnumDescription(this Enum value)
        {
            return value.GetType()
                .GetMember(value.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<DescriptionAttribute>()?
                .Description;
        }

        /// <summary>
        /// 获得枚举字段的名称。
        /// </summary>
        /// <returns></returns>
        public static string GetEnumName(this Enum thisValue)
        {
            return Enum.GetName(thisValue.GetType(), thisValue);
        }

        /// <summary>
        /// 获得枚举字段的值。
        /// </summary>
        /// <returns></returns>
        public static T GetEnumValue<T>(this Enum thisValue)
        {
            return (T)Enum.Parse(thisValue.GetType(), thisValue.ToString());
        }

        #endregion

        #region 逻辑判段的扩展方法

        /// <summary>
        /// 忽略大小写
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="inValue"></param>
        /// <returns></returns>
        public static bool EqualsByOIC(this string thisValue, string inValue)
        {
            return thisValue.Equals(inValue, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 值在的范围？
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="begin">大于等于begin</param>
        /// <param name="end">小于等于end</param>
        /// <returns></returns>
        public static bool IsInRange(this int thisValue, int begin, int end)
        {
            return thisValue >= begin && thisValue <= end;
        }
        /// <summary>
        /// 值在的范围？
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="begin">大于等于begin</param>
        /// <param name="end">小于等于end</param>
        /// <returns></returns>
        public static bool IsInRange(this DateTime thisValue, DateTime begin, DateTime end)
        {
            return thisValue >= begin && thisValue <= end;
        }

        /// <summary>
        /// 在里面吗?
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="thisValue"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool IsIn<T>(this T thisValue, params T[] values)
        {
            return values.Contains(thisValue);
        }

        /// <summary>
        /// 在里面吗?
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="thisValue"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool IsContainsIn(this string thisValue, params string[] inValues)
        {
            return inValues.Any(it => thisValue.Contains(it));
        }

        /// <summary>
        /// 是null或""?
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="ingoreWhiteSpace">是否忽略空白字符，默认true</param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string thisValue, bool ingoreWhiteSpace = true)
        {
            if (ingoreWhiteSpace) return string.IsNullOrWhiteSpace(thisValue);
            if (thisValue == null) return thisValue.Length == 0;
            return thisValue.ToString() == "";
        }
        /// <summary>
        /// 是null或""?
        /// </summary>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this Guid? thisValue)
        {
            if (thisValue == null) return true;
            return thisValue == Guid.Empty;
        }
        /// <summary>
        /// 是null或""?
        /// </summary>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this Guid thisValue)
        {
            if (thisValue == null) return true;
            return thisValue == Guid.Empty;
        }

        /// <summary>
        /// 有值?(与IsNullOrEmpty相反)
        /// </summary>
        /// <returns></returns>
        public static bool IsValuable(this object thisValue)
        {
            if (thisValue == null) return false;
            return thisValue.ToString() != "";
        }
        /// <summary>
        /// 有值?(与IsNullOrEmpty相反)
        /// </summary>
        /// <returns></returns>
        public static bool IsValuable(this IEnumerable<object> thisValue)
        {
            if (thisValue == null || thisValue.Count() == 0) return false;
            return true;
        }

        /// <summary>
        /// 是零?
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static bool IsZero(this object thisValue)
        {
            return (thisValue == null || thisValue.ToString() == "0");
        }

        /// <summary>
        /// 是INT?
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static bool IsInt(this object thisValue)
        {
            if (thisValue == null) return false;
            return Regex.IsMatch(thisValue.ToString(), @"^\d+$");
        }
        /// <summary>
        /// 不是INT?
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static bool IsNoInt(this object thisValue)
        {
            if (thisValue == null) return true;
            return !Regex.IsMatch(thisValue.ToString(), @"^\d+$");
        }

        /// <summary>
        /// 是金钱?
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static bool IsMoney(this object thisValue)
        {
            if (thisValue == null) return false;
            double outValue = 0;
            return double.TryParse(thisValue.ToString(), out outValue);
        }

        /// <summary>
        /// 是时间?
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static bool IsDate(this object thisValue)
        {
            if (thisValue == null) return false;
            DateTime outValue = DateTime.MinValue;
            return DateTime.TryParse(thisValue.ToString(), out outValue);
        }

        /// <summary>
        /// 是邮箱?
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static bool IsEamil(this object thisValue)
        {
            if (thisValue == null) return false;
            return Regex.IsMatch(thisValue.ToString(), @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$");
        }

        /// <summary>
        /// 是手机?
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static bool IsMobile(this object thisValue)
        {
            if (thisValue == null) return false;
            return Regex.IsMatch(thisValue.ToString(), @"^\d{11}$");
        }

        /// <summary>
        /// 是座机?
        /// </summary>
        public static bool IsTelephone(this object thisValue)
        {
            if (thisValue == null) return false;
            return Regex.IsMatch(thisValue.ToString(), @"^(\(\d{3,4}\)|\d{3,4}-|\s)?\d{8}$");

        }

        /// <summary>
        /// 是身份证?
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static bool IsIDcard(this object thisValue)
        {
            if (thisValue == null) return false;
            return Regex.IsMatch(thisValue.ToString(), @"^(\d{15}$|^\d{18}$|^\d{17}(\d|X|x))$");
        }

        /// <summary>
        /// 是传真?
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static bool IsFax(this object thisValue)
        {
            if (thisValue == null) return false;
            return Regex.IsMatch(thisValue.ToString(), @"^[+]{0,1}(\d){1,3}[ ]?([-]?((\d)|[ ]){1,12})+$");
        }

        /// <summary>
        ///是适合正则匹配?
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="begin">大于等于begin</param>
        /// <param name="end">小于等于end</param>
        /// <returns></returns>
        public static bool IsMatch(this object thisValue, string pattern)
        {
            if (thisValue == null) return false;
            Regex reg = new Regex(pattern);
            return reg.IsMatch(thisValue.ToString());
        }

        #endregion
    }
}
