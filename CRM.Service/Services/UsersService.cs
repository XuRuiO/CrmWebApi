using CRM.Core.Models;
using CRM.IRepository.IRepositorys;
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
        private IUsersRepository usersRepository;

        public UsersService(IUsersRepository usersRepository)
        {
            this.baseDal = usersRepository;
            this.usersRepository = usersRepository;
        }

        public async Task<(bool result, string message)> AddUsersAsync(UsersAddInput addInput)
        {
            var usersModel = new UsersModel()
            {
                Id = Guid.NewGuid(),
                Name = addInput.Name
            };

            var result = await baseDal.AddAsync(usersModel, SqlSugarEnums.SqlSugarAddReturnAction.IdentityIntoEntity);

            return result ? (true, "新增成功！") : (false, "新增失败！");
        }

        public async Task<(bool result, string message)> AddListUsersAsync(List<UsersAddInput> addInputs)
        {
            var usersModelList = new List<UsersModel>();

            foreach (var item in addInputs)
            {
                var usersModel = new UsersModel()
                {
                    Id = Guid.NewGuid(),
                    Name = item.Name
                };

                usersModelList.Add(usersModel);
            }

            var result = await baseDal.AddListAsync(usersModelList, SqlSugarEnums.SqlSugarAddReturnAction.IdentityIntoEntity);

            return result ? (true, "新增成功！") : (false, "新增失败！");
        }
    }
}
