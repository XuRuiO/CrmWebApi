using System;
using System.Collections.Generic;
using System.Text;
using CRM.Core.Helpers;
using CSRedis;

namespace CRM.Freamwork.Cache.RedisCache
{
    /// <summary>
    /// 2019.06.11      Rui     RedisCache管理，使用了CsRedis Sdk，初始化Redis链接
    /// </summary>
    public class RedisCacheExtension
    {
        /// <summary>
        /// 初始化Redis服务
        /// </summary>
        public static void Initialization()
        {
            var csredis = new CSRedis.CSRedisClient(ConfigsHelper.GetRedisCacheConnectionString());
            RedisHelper.Initialization(csredis);
        }
    }
}
