using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Freamwork.Authorization.Policys
{
    /// <summary>
    /// 2020.03.21      Rui      Jwt权限必要参数类
    /// 继承 IAuthorizationRequirement，用于设计自定义权限处理器PermissionHandler
    /// 因为AuthorizationHandler 中的泛型参数 TRequirement 必须继承 IAuthorizationRequirement
    /// </summary>
    public class PermissionRequirement
    {
        /// <summary>
        /// Jwt的签发者
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Jwt接收的一方
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// Jwt签名验证
        /// </summary>
        public SigningCredentials SigningCredentials { get; set; }

        /// <summary>
        /// Jwt过期时间，时间戳
        /// </summary>
        public TimeSpan Expiration { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="issuer">Jwt的签发者</param>
        /// <param name="audience">Jwt接收的一方</param>
        /// <param name="signingCredentials">Jwt签名验证</param>
        /// <param name="expiration">Jwt过期时间，时间戳</param>
        public PermissionRequirement(string issuer, string audience, SigningCredentials signingCredentials, TimeSpan expiration)
        {
            Issuer = issuer;
            Audience = audience;
            SigningCredentials = signingCredentials;
            Expiration = expiration;
        }
    }
}
