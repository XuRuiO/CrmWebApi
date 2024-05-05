using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Freamwork.Authorization.Policys
{
    /// <summary>
    /// Jwt权限必要参数类
    /// 继承 IAuthorizationRequirement，用于设计自定义权限处理器PermissionHandler
    /// 因为AuthorizationHandler 中的泛型参数 TRequirement 必须继承 IAuthorizationRequirement
    /// </summary>
    public class PermissionRequirement : IAuthorizationRequirement
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
        /// 权限许可项集合
        /// </summary>
        public List<PermissionItem> PermissionItems { get; set; }

        /// <summary>
        /// 拒绝访问Action
        /// </summary>
        public string DeniedAction { get; set; }

        /// <summary>
        /// 认证授权类型
        /// </summary>
        public string ClaimType { get; set; }

        /// <summary>
        /// 请求路劲
        /// </summary>
        public string LoginPath { get; set; } = "/Api/Login";

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="issuer">Jwt的签发者</param>
        /// <param name="audience">Jwt接收的一方</param>
        /// <param name="signingCredentials">Jwt签名验证</param>
        /// <param name="expiration">Jwt过期时间，时间戳</param>
        /// <param name="permissionItems">权限许可项集合</param>
        /// <param name="deniedAction">拒绝访问Action</param>
        /// <param name="claimType">认证授权类型</param>
        public PermissionRequirement(string issuer, string audience, SigningCredentials signingCredentials, TimeSpan expiration,
            List<PermissionItem> permissionItems, string deniedAction, string claimType)
        {
            Issuer = issuer;
            Audience = audience;
            SigningCredentials = signingCredentials;
            Expiration = expiration;
            PermissionItems = permissionItems;
            DeniedAction = deniedAction;
            ClaimType = claimType;
        }
    }
}
