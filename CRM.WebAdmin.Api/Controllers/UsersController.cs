﻿using System;
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
    public class UsersController : ControllerBase
    {
        private IUsersService usersService;

        //构造函数注入
        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

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

        /// <summary>
        /// 获取名称
        /// </summary>
        /// <param name="aaa"></param>
        /// <returns></returns>
        [HttpGet("GetName")]
        public async Task<string> GetName(string aaa)
        {
            return await Task.Run(() => aaa);
        }
    }
}