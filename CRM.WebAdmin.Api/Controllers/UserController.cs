using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.IService.IServices;
using CRM.Model.InputModels;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebAdmin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
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
        public async Task<(bool result, string message)> AddUsers([FromBody]UserAddInput addInput)
        {
            var result = await userService.AddUsersAsync(addInput);

            return result;
        }

        /// <summary>
        /// 批量新增用户
        /// </summary>
        /// <param name="addInputs"></param>
        /// <returns></returns>
        [HttpPost("AddListUsers")]
        public async Task<(bool result, string message)> AddListUsers([FromBody]List<UserAddInput> addInputs)
        {
            var result = await userService.AddListUsersAsync(addInputs);

            return result;
        }

        /// <summary>
        /// 获取名称
        /// </summary>
        /// <param name="aaa"></param>
        /// <returns></returns>
        [HttpGet("GetName")]
        public async Task<string> GetName(string aaa)
        {
            return await Task.Run(() => RedisHelper.Get("User:67e5f521-7c0a-480f-9eb2-aa26d6632439"));
        }
    }
}