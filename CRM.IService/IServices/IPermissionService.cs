using CRM.IService.IBase;
using CRM.Model.Models;
using CRM.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRM.IService.IServices
{
    public interface IPermissionService : IBaseService<Permission>
    {
        /// <summary>
        /// 获取菜单导航数据，生成前端路由
        /// </summary>
        /// <returns></returns>
        Task<List<MenuNavigationBarTreeView>> GetMenuNavigationBarTree();
    }
}

