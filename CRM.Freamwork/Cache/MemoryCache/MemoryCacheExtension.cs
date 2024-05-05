using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Caching.Memory;

namespace CRM.Freamwork.Cache.MemoryCache
{
    /// <summary>
    /// MemoryCache（内存缓存）的接口实现
    /// 方法定义的比较少，需要的话，可以在加
    /// </summary>
    public class MemoryCacheExtension : IMemoryCacheExtension
    {
        //引用Microsoft.Extensions.Caching.Memory;这个和.net 还是不一样，没有了Httpruntime了
        private readonly IMemoryCache cache;

        public MemoryCacheExtension(IMemoryCache cache)
        {
            this.cache = cache;
        }

        /// <summary>
        /// 根据指定Key，设置内存缓存中的数据
        /// </summary>
        /// <param name="cacheKey">缓存键</param>
        /// <param name="cacheValue">缓存值</param>
        /// <param name="absoluteExpirationRelativeToNow">相对当前时间的绝对过期时间（使用TimeSpan）</param>
        public void Set(string cacheKey, object cacheValue, TimeSpan absoluteExpirationRelativeToNow)
        {
            cache.Set(cacheKey, cacheValue, absoluteExpirationRelativeToNow);
        }

        /// <summary>
        /// 根据指定Key，获取内存缓存中的数据
        /// </summary>
        /// <param name="cacheKey">缓存键</param>
        /// <returns></returns>
        public object Get(string cacheKey)
        {
            return cache.Get(cacheKey);
        }
    }
}
