using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Freamwork.GlobalRouting
{
    /// <summary>
    /// 2018.12.02  Rui     自定义全局路由统一前缀
    /// </summary>
    public static class MvcOptionsExtension
    {
        public static void UseCentralRoutePrefix(this MvcOptions options, IRouteTemplateProvider routeTemplate)
        {
            //添加我们自定义 实现IApplicationModelConvention的RouteConvention
            options.Conventions.Insert(0, new RouteConvention(routeTemplate));
        }
    }
}
