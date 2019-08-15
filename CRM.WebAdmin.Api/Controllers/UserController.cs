using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Logging;
using CRM.IService.IServices;
using CRM.Model.RequestModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace CRM.WebAdmin.Api.Controllers
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService userService;
        private ILogger<UserController> logger;

        //构造函数注入
        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            this.userService = userService;
            this.logger = logger;
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
        public async Task<List<Model.Models.UserModel>> GetListPage(string name = "徐")
        {
            logger.LogInformation("徐瑞你好！");

            var result = await userService.GetListPage(name);

            return result;
        }
    }
}