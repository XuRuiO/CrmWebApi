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
        Task<(bool result, string message)> AddUsersAsync(UsersAddInput addInput);

        Task<(bool result, string message)> AddListUsersAsync(List<UsersAddInput> addInputs);
    }
}
