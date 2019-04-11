using CRM.Core.Models;
using CRM.IService.IServices;
using CRM.Model.InputModels;
using CRM.Model.Models;
using CRM.Service.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Service.Services
{
    public class UsersService : BaseService<UsersModel>, IUsersService
    {
        public async Task<(bool result, string message)> AddUsers(UsersAddInput addInput)
        {
            var usersModel = new UsersModel()
            {
                Id = Guid.NewGuid(),
                Name = addInput.Name
            };

            var result = await baseDal.Add(usersModel, SqlSugarEnums.SqlSugarAddReturnAction.IdentityIntoEntity);

            return result ? (true, "新增成功！") : (false, "新增失败！");
        }
    }
}
