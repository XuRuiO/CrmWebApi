using CRM.IService.IBase;
using CRM.Model.InputModels;
using CRM.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRM.IService.IServices
{
    public interface IUserService : IBaseService<UserModel>
    {
        Task<(bool result, string message)> AddUsersAsync(UserAddInput addInput);

        Task<(bool result, string message)> AddListUsersAsync(List<UserAddInput> addInputs);
    }
}
