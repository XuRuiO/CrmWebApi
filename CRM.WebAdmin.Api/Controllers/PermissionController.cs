using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Core.Models;
using CRM.Freamwork;
using CRM.IService.IServices;
using CRM.Model.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebAdmin.Api.Controllers
{
    /// <summary>
    /// 路由菜单管理
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : BaseController
    {
        private IPermissionService permissionService;

        public PermissionController(IPermissionService permissionService)
        {
            this.permissionService = permissionService;
        }

        /// <summary>
        /// 获取菜单导航数据，生成前端路由
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetMenuNavigationBarTree")]
        public async Task<ResultBasic<List<MenuNavigationBarTreeView>>> GetMenuNavigationBarTree()
        {
            var result = await permissionService.GetMenuNavigationBarTree();

            return ResultBasic.WithSuccess(result, "请求成功");
        }
    }
}