using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.DynamicProxy;
using CRM.Core.Attributes;
using CRM.Core.Helpers;
using CRM.Freamwork.Cache.MemoryCache;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace CRM.Freamwork.Aop
{
    /// <summary>
    /// 2019.08.10      Rui     内存缓存Aop拦截器，继承接口IInterceptor
    /// </summary>
    public class MemoryCacheAop : IInterceptor
    {
        //通过注入的方式，把缓存操作接口通过构造函数注入
        private readonly IMemoryCacheExtension memoryCacheExtension;
        public MemoryCacheAop(IMemoryCacheExtension memoryCacheExtension)
        {
            this.memoryCacheExtension = memoryCacheExtension;
        }

        /// <summary>
        /// 实例化接口IINterceptor的唯一方法Intercept，Intercept方法是拦截的关键所在，也是IInterceptor接口中的唯一定义
        /// </summary>
        /// <param name="invocation"></param>
        public void Intercept(IInvocation invocation)
        {
            var method = invocation.MethodInvocationTarget ?? invocation.Method;
            //对当前方法的特性验证
            var memoryCacheAttribute = method.GetCustomAttributes(true).FirstOrDefault(x => x.GetType() == typeof(MemoryCacheAttribute)) as MemoryCacheAttribute;
            if (memoryCacheAttribute != null)
            {
                //获取自定义缓存键
                var cacheKey = MemoryCacheKey(invocation);
                //根据Key获取相应的缓存值
                var cacheValue = memoryCacheExtension.Get(cacheKey);
                if (cacheValue != null)
                {
                    //将当前获取到的缓存值，赋值给当前执行方法
                    invocation.ReturnValue = cacheValue;
                    return;
                }
                //去执行当前的方法
                invocation.Proceed();
                //存入缓存
                if (!string.IsNullOrWhiteSpace(cacheKey))
                {
                    memoryCacheExtension.Set(cacheKey, invocation.ReturnValue, TimeSpan.FromSeconds(memoryCacheAttribute.AbsoluteExpiration));
                }
            }
            else
            {
                //直接执行被拦截方法
                invocation.Proceed();
            }
        }

        /// <summary>
        /// 根据拦截的Server类，方法名 生成内存缓存key
        /// </summary>
        /// <param name="invocation"></param>
        /// <returns></returns>
        private string MemoryCacheKey(IInvocation invocation)
        {
            var typeName = invocation.TargetType.Name;
            var methodName = invocation.Method.Name;
            string key = $"{typeName}:{methodName}";

            return key;
        }
    }
}
