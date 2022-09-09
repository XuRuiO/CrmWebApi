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
using CRM.Core.Attributes;
using CRM.Model.ViewPageModels;
using CRM.Core.Helpers;

namespace CRM.Service.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        private IUserRepository userRepository;

        public UserService(IUserRepository usersRepository)
        {
            this.baseDal = usersRepository;
            this.userRepository = usersRepository;
        }

        public async Task<(bool result, string message, UserInfoView userInfoView)> Login(string userName, string password)
        {
            //根据用户名，密码获取用户信息
            var userData = await baseDal.QueryFirstAsync(x => x.UserName == userName && x.Password == password);
            if (userData == null)
            {
                return (false, "用户名或密码不正确", null);
            }

            var userInfoView = new UserInfoView
            {
                Id = userData.Id.ObjToInt(),
                RoleName = "管理员"
            };

            return (true, "登陆成功", userInfoView);
        }

        public async Task<(bool result, string message)> AddUsersAsync(UserAddRequest addRequest)
        {
            var usersModel = new User()
            {
                Name = addRequest.Name
            };

            var result = await baseDal.AddAsync(usersModel, SqlSugarEnums.SqlSugarAddReturnAction.IdentityIntoEntity);

            return result ? (true, "新增成功！") : (false, "新增失败！");
        }

        public async Task<(bool result, string message)> AddListUsersAsync(List<UserAddRequest> addRequests)
        {
            var usersModelList = new List<User>();

            foreach (var item in addRequests)
            {
                var usersModel = new User()
                {
                    Name = item.Name
                };

                usersModelList.Add(usersModel);
            }

            var result = await baseDal.AddListAsync(usersModelList, SqlSugarEnums.SqlSugarAddReturnAction.IdentityIntoEntity);

            return result ? (true, "新增成功！") : (false, "新增失败！");
        }

        [MemoryCache]
        public async Task<BasePageModel<User>> GetListPage(string name)
        {
            var pageInfo = new SqlSugarPageInfo() { PageIndex = 1, PageSize = 5 };

            var result = await baseDal.QueryConditionPageAsync(null, pageInfo);

            return new BasePageModel<User>
            {
                Models = result,
                Total = pageInfo.TotalCount
            };
        }

        public async Task<List<dynamic>> GetUserRoleModelsAsync()
        {
            return await baseDal.QueryMuchAnonymityAsync<User, Role, dynamic>
                (
                    (t1, t2) => new JoinQueryInfos(JoinType.Inner, t1.RoleId.Contains(t2.Id.ToString())),
                    (t1, t2) => new { t1.Id, RoleName = t2.Name },
                    null,
                    new List<OrderByClause>() {
                        new OrderByClause(){ Sort="t2.Name",Order=SqlSugarEnums.OrderSequence.Asc}
                    }
                );
        }
    }
}
