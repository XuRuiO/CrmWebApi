using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Freamwork.GlobalRouting
{
    /// <summary>
    /// 2018.12.02  Rui
    /// 定义个类RouteConvention，实现 IApplicationModelConvention 接口，添加全局路由统一前缀。
    /// 参考链接：https://www.cnblogs.com/savorboard/p/dontnet-IApplicationModelConvention.html
    /// </summary>
    public class RouteConvention : IApplicationModelConvention
    {
        private readonly AttributeRouteModel attributeRouteModel;

        public RouteConvention(IRouteTemplateProvider routeTemplateProvider)
        {
            this.attributeRouteModel = new AttributeRouteModel(routeTemplateProvider);
        }

        //实现接口中的Apply方法
        public void Apply(ApplicationModel applicationModel)
        {
            //遍历所有的 Controller
            foreach (var controller in applicationModel.Controllers)
            {
                //1、已经标记了 RouteAttribute 的 Controller
                //这一块需要注意，如果在控制器中已经标注有路由了，则会在路由的前面再添加指定的路由内容。
                var matchedSelectors = controller.Selectors.Where(x => x.AttributeRouteModel != null).ToList();
                if (matchedSelectors.Any())
                {
                    foreach (var selectorModel in matchedSelectors)
                    {
                        //在当前路由上再添加一个路由前缀
                        selectorModel.AttributeRouteModel = AttributeRouteModel.CombineAttributeRouteModel(attributeRouteModel, selectorModel.AttributeRouteModel);
                    }
                }

                //2、没有标记RouteAttribute的Controller
                var unmatchedSelectors = controller.Selectors.Where(x => x.AttributeRouteModel == null).ToList();
                if (unmatchedSelectors.Any())
                {
                    foreach (var selectorModel in unmatchedSelectors)
                    {
                        //添加一个路由前缀
                        selectorModel.AttributeRouteModel = attributeRouteModel;
                    }
                }
            }
        }
    }
}
