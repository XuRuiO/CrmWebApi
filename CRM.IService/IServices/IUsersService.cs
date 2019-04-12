using CRM.IService.IBase;
using CRM.Model.InputModels;
using CRM.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRM.IService.IServices
{
    public interface IUsersService : IBaseService<UsersModel>
    {
        Task<(bool result, string message)> AddUsers(UsersAddInput addInput);

        Task<(bool result, string message)> AddListUsers(List<UsersAddInput> addInputs);
    }
}
