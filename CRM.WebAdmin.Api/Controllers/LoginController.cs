using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        [HttpGet("GetJWTStr")]
        public ActionResult GetJWTStr(long id = 1, string sub = "Admin")
        {
            //这里就是用户登陆以后，通过数据库去调取数据，分配权限的操作
            TokenModelJWT tokenModelJWT = new TokenModelJWT();
            tokenModelJWT.Uid = id;
            tokenModelJWT.Role = sub;

            string jwtstr = JwtHelper.IssueJWT(tokenModelJWT);

            return Ok(jwtstr);
        }
    }
}