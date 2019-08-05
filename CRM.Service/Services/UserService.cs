using CRM.Core.Models;
using CRM.IRepository.IRepositorys;
using CRM.IService.IServices;
using CRM.Model.RequestModels;
using CRM.Model.Models;
using CRM.Model.ViewModels;
using CRM.Service.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;
using System.Linq.Expressions;

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

        public async Task<(bool result, string message)> AddUsersAsync(UserAddRequest addRequest)
        {
            var usersModel = new UserModel()
            {
                Id = Guid.NewGuid(),
                Name = addRequest.Name
            };

            var result = await baseDal.AddAsync(usersModel, SqlSugarEnums.SqlSugarAddReturnAction.IdentityIntoEntity);

            return result ? (true, "新增成功！") : (false, "新增失败！");
        }

        public async Task<(bool result, string message)> AddListUsersAsync(List<UserAddRequest> addRequests)
        {
            var usersModelList = new List<UserModel>();

            foreach (var item in addRequests)
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

        public async Task<List<dynamic>> GetUserRoleModelsAsync()
        {
            return await baseDal.QueryMuchAnonymityAsync<UserRoleModel, RoleModel, dynamic>
                (
                    (t1, t2) => new JoinQueryInfos(JoinType.Inner, t1.RoleId == t2.Id),
                    (t1, t2) => new { Id = t1.Id, RoleName = t2.Name },
                    null,
                    new List<OrderByClause>() {
                        new OrderByClause(){ Sort="t2.Name",Order=SqlSugarEnums.OrderSequence.Asc}
                    }
                );
        }

        public async Task<List<UserModel>> GetListPage()
        {
            var pageInfo = new SqlSugarPageInfo() { PageIndex = 1, PageSize = 5 };

            return await baseDal.QueryConditionPageAsync(x => x.Name.Contains("徐"), pageInfo);
        }
    }
}
