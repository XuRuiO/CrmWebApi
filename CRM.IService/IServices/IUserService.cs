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
    public interface IUserService : IBaseService<UserModel>
    {
        Task<(bool result, string message)> AddUsersAsync(UserAddRequest addRequest);

        Task<(bool result, string message)> AddListUsersAsync(List<UserAddRequest> addRequests);

        Task<List<dynamic>> GetUserRoleModelsAsync();

        Task<BasePageModel<UserModel>> GetListPage(string name);
    }
}
