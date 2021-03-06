﻿using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using CRM.Core.Helpers;
using CRM.Freamwork.Aop;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace CRM.Freamwork.Autofac
{
    /// <summary>
    /// 2019.05.27      Rui     使用Autofac接管.Net Core中内置的Ioc（Ioc依赖注入，控制反转）。
    /// </summary>
    public static class AutofacExtension
    {
        /// <summary>
        /// 添加Autofac服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceProvider AddAutofacCRM(this IServiceCollection services)
        {
            ////第一步：构造一个AutoFac的builder容器。
            var builder = new ContainerBuilder();

            #region 注册要通过反射创建的组件，注册拦截器Aop（AOP是切面，是面向的service层的所有方法）

            //内存缓存拦截器
            builder.RegisterType<MemoryCacheAop>();

            #endregion

            //第二步：告诉AutoFac容器，创建项目中的指定类的对象实例，以接口的形式存储（其实就是创建数据仓储层与业务逻辑层这两个程序集中所有类的对象实例，然后以其接口的形式保存到AutoFac容器内存中，当然如果有需要也可以创建其他程序集的所有类的对象实例，这个只需要我们指定就可以了）
            //获取项目路径
            var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;

            //定义注入的dll程序集名称集合
            var assembliesList = new List<Assembly>();

            //循环获取需要注入的dll
            foreach (var assemblysName in ConfigsHelper.GetAutoFacAssemblyName().Split(','))
            {
                //获取注入项目绝对路径
                var repositorysAndServicesDllFile = Path.Combine(basePath, assemblysName);
                //直接采用加载文件的方法
                /*
                  Assembly.LoadFile：只载入相应的dll文件，比如Assembly.LoadFile("a.dll")，则载入a.dll，假如a.dll中引用了b.dll的话，b.dll并不会被载入。
                  Assembly.LoadFrom：则不一样，它会载入dll文件及其引用的其他dll，比如上面的例子，b.dll也会被载入。
                 */
                var assemblysRepositorysAndServices = Assembly.LoadFrom(repositorysAndServicesDllFile);
                //将获取到的dll添加到list中
                assembliesList.Add(assemblysRepositorysAndServices);
            }

            //第三步：AsImplementedInterfaces:指明创建的stypes这个集合中所有类的对象实例，以其接口的形式保存。
            builder.RegisterAssemblyTypes(assembliesList.ToArray()).AsImplementedInterfaces()
                    .InstancePerLifetimeScope()
                    .EnableInterfaceInterceptors()
                    .InterceptedBy(typeof(MemoryCacheAop));     //允许将拦截器服务的列表分配给注册

            //第四步：将 services 填充 Autofac 容器生成器
            builder.Populate(services);

            //第五步：使用已进行的组件登记创建新容器
            var applicationContainer = builder.Build();

            //第六步：第三方IOC接管 core内置DI容器
            return new AutofacServiceProvider(applicationContainer);
        }
    }
}
