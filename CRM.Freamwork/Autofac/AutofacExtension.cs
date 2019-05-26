using Autofac;
using Autofac.Extensions.DependencyInjection;
using CRM.IService.IServices;
using CRM.Service.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Freamwork.Autofac
{
    /// <summary>
    /// 2019.05.27
    /// </summary>
    public static class AutofacExtension
    {
        public static IServiceProvider AddAutofacCRM(this IServiceCollection services)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<UsersService>().As<IUsersService>();

            builder.Populate(services);

            var ApplicationContainer = builder.Build();

            return new AutofacServiceProvider(ApplicationContainer);//第三方IOC接管 core内置DI容器
        }
    }
}
