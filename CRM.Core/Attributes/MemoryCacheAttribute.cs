using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Attributes
{
    /// <summary>
    /// 2019.08.11      Rui     这个Attribute就是使用时候的验证，把它添加到要缓存数据的方法中，即可完成缓存的操作。
    /// 特性类的定位参数和命名参数的类型仅限于特性参数类型，这些包括：bool, byte, char, double, float, int, long, short, string, System.Type, object, enum
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class MemoryCacheAttribute : Attribute
    {
        /// <summary>
        /// 缓存绝对过期时间（秒数）
        /// </summary>
        public int AbsoluteExpiration { get; set; } = 30 * 60;
    }
}
