using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Logging;
using CRM.Core.Models;
using CRM.IService.IServices;
using CRM.Model.Models;
using CRM.Model.RequestModels;
using CRM.Model.ViewPageModels;
using CRM.WebAdmin.Api.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CRM.WebAdmin.Api.Controllers
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private IUserService userService;

        //构造函数注入
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="addInput"></param>
        /// <returns></returns>
        [HttpPost("AddUsers")]
        public async Task<(bool result, string message)> AddUsers([FromBody]UserAddRequest addRequest)
        {
            var result = await userService.AddUsersAsync(addRequest);

            return result;
        }

        /// <summary>
        /// 批量新增用户
        /// </summary>
        /// <param name="addInputs"></param>
        /// <returns></returns>
        [HttpPost("AddListUsers")]
        public async Task<(bool result, string message)> AddListUsers([FromBody]List<UserAddRequest> addRequests)
        {
            var result = await userService.AddListUsersAsync(addRequests);

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetName")]
        public async Task<List<dynamic>> GetName()
        {
            var result = await userService.GetUserRoleModelsAsync();

            return result;
        }

        [HttpPost]
        public async Task<Result<BasePageModel<UserModel>>> GetListPage(string name = "徐")
        {
            var result = await userService.GetListPage(name);

            return Success("请求成功！", result);
        }
    }
}