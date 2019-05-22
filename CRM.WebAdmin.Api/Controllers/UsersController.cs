using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.IService.IServices;
using CRM.Model.InputModels;
using CRM.Model.Models;
using CRM.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebAdmin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        //采用autoioc注入时，将控制器引用的依赖项 CRM.Service 去掉
        public IUsersService usersService = new UsersService();

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="addInput"></param>
        /// <returns></returns>
        [HttpPost("AddUsers")]
        public async Task<(bool result, string message)> AddUsers([FromBody]UsersAddInput addInput)
        {
            var result = await usersService.AddUsersAsync(addInput);

            return result;
        }

        /// <summary>
        /// 批量新增用户
        /// </summary>
        /// <param name="addInputs"></param>
        /// <returns></returns>
        [HttpPost("AddListUsers")]
        public async Task<(bool result, string message)> AddListUsers([FromBody]List<UsersAddInput> addInputs)
        {
            var result = await usersService.AddListUsersAsync(addInputs);

            return result;
        }
    }
}