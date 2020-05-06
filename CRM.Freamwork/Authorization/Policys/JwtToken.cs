using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CRM.Freamwork.Authorization.Policys
{
    /// <summary>
    /// 2020.03.21      Rui      JwtToken生成类
    /// </summary>
    public class JwtToken
    {
        /// <summary>
        /// 获取基于JWT的Token
        /// </summary>
        /// <param name="claims">需要在登陆的时候配置</param>
        /// <param name="permissionRequirement">在Startup中定义的参数</param>
        /// <returns></returns>
        public static string BuildJwtToken(Claim[] claims, PermissionRequirement permissionRequirement)
        {
            var now = DateTime.Now;

            //实例化JwtSecurityToken
            var jwt = new JwtSecurityToken(
                issuer: permissionRequirement.Issuer,       //jwt的签发者
                audience: permissionRequirement.Audience,       //接收jwt的一方
                claims: claims,
                notBefore: now,     //生效时间，定义在什么时间之前，该jwt都是不可用的
                expires: now.Add(permissionRequirement.Expiration),     //jwt的过期时间，这个过期时间必须要大于签发时间（注意JWT有自己的缓冲过期时间）
                signingCredentials: permissionRequirement.SigningCredentials        //jwt加密的密钥
            );

            //生成JwtToken
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }
    }
}
