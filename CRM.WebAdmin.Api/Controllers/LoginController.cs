using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Core.CustomExtensions;
using CRM.Core.Helpers;
using CRM.Core.Models;
using CRM.Core.ThirdPartyHelper;
using CRM.Freamwork;
using CRM.IService.IServices;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebAdmin.Api.Controllers
{
    /// <summary>
    /// 登陆管理
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : BaseController
    {
        private IUserService _userService;

        //构造函数注入
        public LoginController(IUserService userService)
        {
            this._userService = userService;
        }

        /// <summary>
        /// 用户登陆，登陆成功返回Token给用户；登陆失败，返回错误信息
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task<Result<string>> Login(string userName, string password)
        {
            #region 数据验证
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new CustomerException("请输入用户名", ApiResponseStatusCode.ParameterError);
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new CustomerException("请输入密码", ApiResponseStatusCode.ParameterError);
            }
            #endregion

            var result = await _userService.Login(userName, password);

            if (!result.result)
            {
                return Error<string>((int)ApiResponseStatusCode.ParameterError, result.message);
            }
            else
            {
                return Success<string>(result.message, "");
            }
        }
    }
}