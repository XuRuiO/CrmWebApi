using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using CRM.Core.Models;
using CRM.IService.IServices;
using CRM.WebAdmin.Api.AuthHelper.OverWrite;
using CRM.WebAdmin.Api.Common;
using Microsoft.AspNetCore.Http;
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
        [HttpGet("Login")]
        public async Task<Result<string>> Login(string userName, string password)
        {
            #region 数据验证
            if (string.IsNullOrWhiteSpace(userName))
            {
                return Error<string>((int)ApiResponseStatusCode.Error, "请输入用户名！");
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                return Error<string>((int)ApiResponseStatusCode.Error, "请输入密码！");
            }
            #endregion

            var result = await _userService.Login(userName, password);

            if (!result.result)
            {
                return Error<string>((int)ApiResponseStatusCode.Error, result.message);
            }
            else
            {
                //生成JWT令牌
                TokenModelJWT tokenModelJWT = new TokenModelJWT
                {
                    Id = result.userInfoView.Id.ToString(),
                    Role = result.userInfoView.RoleName
                };

                string jwtStr = JwtHelper.IssueJWT(tokenModelJWT);

                return Success<string>(result.message, jwtStr);
            }
        }
    }
}