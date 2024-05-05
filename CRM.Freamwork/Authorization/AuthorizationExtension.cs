using CRM.Core.Helpers;
using CRM.Freamwork.Authorization.Policys;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace CRM.Freamwork.Authorization
{
    /// <summary>
    /// 授权配置扩展，需要在Startup服务启动
    /// </summary>
    public static class AuthorizationExtension
    {
        public static void AddAuthorizationSetup(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            //jwt签发人
            var issuer = ConfigsHelper.GetJwtAudienceIssuer();
            //jwt接收方
            var audience = ConfigsHelper.GetJwtAudienceAud();
            //jwt密钥
            var secret = ConfigsHelper.GetJwtAudienceSecret();
            var secretByteArray = Encoding.ASCII.GetBytes(secret);
            var signingKey = new SymmetricSecurityKey(secretByteArray);
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            //jwt过期时间
            var accessTokenExpiration = ConfigsHelper.GetJwtAudienceAccessTokenExpiration();
            var expiration = TimeSpan.FromSeconds(accessTokenExpiration);

            //如果要数据库动态绑定，这里先留个空，后边处理器里动态赋值
            var permission = new List<PermissionItem>();

            //角色与接口的权限要求参数
            var permissionRequirement = new PermissionRequirement(
                issuer,
                audience,
                signingCredentials,
                expiration,
                permission,
                "",
                ClaimTypes.Role);

            //自定义复杂的策略授权
            services.AddAuthorization(options =>
            {
                options.AddPolicy(ConstHelper.PermissionName, policy => policy.Requirements.Add(permissionRequirement));
            });


            /* Jwt 权限 */
            //令牌验证参数
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = issuer,//发行人
                ValidateAudience = true,
                ValidAudience = audience,//订阅人
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromSeconds(30),
                RequireExpirationTime = true
            };

            // 开启Bearer认证
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            });
        }
    }
}
