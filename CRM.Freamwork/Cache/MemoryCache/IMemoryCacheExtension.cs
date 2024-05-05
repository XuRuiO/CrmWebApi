using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Freamwork.Cache.MemoryCache
{
    /// <summary>
    /// MemoryCache（内存缓存）的接口定义
    /// 方法定义的比较少，需要的话，可以在加
    /// </summary>
    public interface IMemoryCacheExtension
    {
        /// <summary>
        /// 根据指定Key，设置内存缓存中的数据
        /// </summary>
        /// <param name="cacheKey">缓存键</param>
        /// <param name="cacheValue">缓存值</param>
        /// <param name="absoluteExpirationRelativeToNow">相对当前时间的绝对过期时间（使用TimeSpan），为null条件无效</param>
        void Set(string cacheKey, object cacheValue, TimeSpan absoluteExpirationRelativeToNow);

        /// <summary>
        /// 根据指定Key，获取内存缓存中的数据
        /// </summary>
        /// <param name="cacheKey">缓存键</param>
        /// <returns></returns>
        object Get(string cacheKey);
    }
}
