using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Freamwork.GlobalRouting
{
    public static class MvcOptionsExtensions
    {
        public static void UseCentralRoutePrefix(this MvcOptions options, IRouteTemplateProvider routeTemplate)
        {
            //添加我们自定义 实现IApplicationModelConvention的RouteConvention
            options.Conventions.Insert(0, new RouteConvention(routeTemplate));
        }
    }
}
