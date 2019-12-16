using CRM.IService.IBase;
using CRM.Model.RequestModels;
using CRM.Model.Models;
using CRM.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CRM.Model.ViewPageModels;

namespace CRM.IService.IServices
{
    public interface IUserService : IBaseService<User>
    {
        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        Task<(bool result, string message, UserInfoView userInfoView)> Login(string userName, string password);

        Task<(bool result, string message)> AddUsersAsync(UserAddRequest addRequest);

        Task<(bool result, string message)> AddListUsersAsync(List<UserAddRequest> addRequests);

        Task<List<dynamic>> GetUserRoleModelsAsync();

        Task<BasePageModel<User>> GetListPage(string name);
    }
}
