using CRM.IRepository.IRepositorys;
using CRM.IService.IServices;
using CRM.Model.Models;
using CRM.Model.ViewModels;
using CRM.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Service.Services
{
    public class PermissionService : BaseService<Permission>, IPermissionService
    {
        private IPermissionRepository permissionRepository;

        public PermissionService(IPermissionRepository permissionRepository)
        {
            this.baseDal = permissionRepository;
            this.permissionRepository = permissionRepository;
        }

        /// <summary>
        /// ��ȡ�˵��������ݣ�����ǰ��·��
        /// </summary>
        /// <returns></returns>
        public async Task<List<MenuNavigationBarTreeView>> GetMenuNavigationBarTree()
        {
            //��ȡ�˵�����
            var permissionList = await baseDal.QueryAllAsync();

            return RecursiveMenuNaviBarTree(permissionList);
        }

        #region �˵��ݹ����

        private List<MenuNavigationBarTreeView> RecursiveMenuNaviBarTree(List<Permission> permissions, int parentId = 0)
        {
            var barTreeViews = new List<MenuNavigationBarTreeView>();
            //����ParentId��ȡ����
            var menuNavData = permissions.Where(x => x.ParentId == parentId).OrderBy(x => x.OrderSort);

            if (menuNavData.Any())
            {
                foreach (var item in menuNavData)
                {
                    var parentMenuNavigation = new MenuNavigationBarTreeView();
                    parentMenuNavigation.Id = item.Id;
                    parentMenuNavigation.Path = item.Path;
                    if (!string.IsNullOrWhiteSpace(item.Redirect))
                    {
                        parentMenuNavigation.Redirect = item.Redirect;
                    }
                    parentMenuNavigation.Meta = new MenuNavigationBarMeta
                    {
                        Title = item.Title,
                        Icon = item.Icon,
                        IsHidden = item.IsHidden,
                        IsAffix = item.IsAffix,
                        IsUseLayout = item.IsUseLayout,
                        OrderSort = item.OrderSort
                    };
                    parentMenuNavigation.Children = RecursiveMenuNaviBarTree(permissions, item.Id);

                    barTreeViews.Add(parentMenuNavigation);
                }
            }

            return barTreeViews;
        }

        #endregion
    }
}

