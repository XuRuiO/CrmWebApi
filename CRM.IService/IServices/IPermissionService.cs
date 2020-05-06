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
        /// ��ȡ�˵��������ݣ�����ǰ��·��
        /// </summary>
        /// <returns></returns>
        Task<List<MenuNavigationBarTreeView>> GetMenuNavigationBarTree();
    }
}

