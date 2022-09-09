using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using CRM.Core.Helpers;
using Autofac;
using Module = Autofac.Module;
using Autofac.Extras.DynamicProxy;
using CRM.Freamwork.Aop;

namespace CRM.Freamwork.Autofac
{
    /// <summary>
    /// 使用Autofac接管.Net Core中内置的Ioc（Ioc依赖注入，控制反转）。
    /// </summary>
    public class AutofacModuleRegister : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //内存缓存拦截器
            builder.RegisterType<MemoryCacheAop>();

            //获取项目路径
            var basePath = AppContext.BaseDirectory;

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

            //AsImplementedInterfaces:指明创建的types这个集合中所有类的对象实例，以其接口的形式保存。
            builder.RegisterAssemblyTypes(assembliesList.ToArray())
                   .AsImplementedInterfaces()
                   .InstancePerDependency()
                   .EnableInterfaceInterceptors()
                   .InterceptedBy(typeof(MemoryCacheAop));     //允许将拦截器服务的列表分配给注册
        }
    }
}
