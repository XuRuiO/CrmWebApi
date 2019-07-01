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
    public class UserService : BaseService<UserModel>, IUserService
    {
        private IUserRepository usersRepository;

        public UserService(IUserRepository usersRepository)
        {
            this.baseDal = usersRepository;
            this.usersRepository = usersRepository;
        }

        public async Task<(bool result, string message)> AddUsersAsync(UserAddInput addInput)
        {
            var usersModel = new UserModel()
            {
                Id = Guid.NewGuid(),
                Name = addInput.Name
            };

            var result = await baseDal.AddAsync(usersModel, SqlSugarEnums.SqlSugarAddReturnAction.IdentityIntoEntity);

            return result ? (true, "新增成功！") : (false, "新增失败！");
        }

        public async Task<(bool result, string message)> AddListUsersAsync(List<UserAddInput> addInputs)
        {
            var usersModelList = new List<UserModel>();

            foreach (var item in addInputs)
            {
                var usersModel = new UserModel()
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
