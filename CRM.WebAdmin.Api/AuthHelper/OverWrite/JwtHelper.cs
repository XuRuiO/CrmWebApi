using CRM.Core.Helpers;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Linq;

namespace CRM.WebAdmin.Api.AuthHelper.OverWrite
{
    /// <summary>
    /// 2018.12.20      Rui     生成Token，和Token反序列成model
    /// </summary>
    public class JwtHelper
    {
        //定义一个私钥
        public static string secreKey { get; set; } = "sdfsdfsrty45634kkhllghtdgdfss345t678fs";

        /// <summary>
        /// 颁发JWT字符串
        /// </summary>
        /// <param name="tokenModelJWT">令牌类</param>
        /// <returns></returns>
        public static string IssueJWT(TokenModelJWT tokenModelJWT)
        {
            //签发人
            string iss = ConfigsHelper.GetJwtAudienceIssuer();
            //接收jwt的一方
            string aud = ConfigsHelper.GetJwtAudienceAud();
            //密钥
            string secret = ConfigsHelper.GetJwtAudienceSecret();

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti,tokenModelJWT.Uid.ToString()),        //jti:为JWT提供了唯一的标识符
                new Claim(JwtRegisteredClaimNames.Iat,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),     //jwt的签发时间
                new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),     //生效时间，定义在什么时间之前，该jwt都是不可用的
                new Claim(JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.AddSeconds(1000)).ToUnixTimeSeconds()}"),      //jwt的过期时间，这个过期时间必须要大于签发时间
                new Claim(JwtRegisteredClaimNames.Iss,iss),
                new Claim(JwtRegisteredClaimNames.Aud,aud)
            };

            claims.Any();

            //可以将一个用户的多个角色全部赋予
            claims.AddRange(tokenModelJWT.Role.Split(',').Select(x => new Claim(ClaimTypes.Role, x)));

            //秘钥 (SymmetricSecurityKey 对安全性的要求，密钥的长度太短会报出异常)


            //var dateTime = DateTime.UtcNow;
            //var claims = new Claim[]
            //{
            //    new Claim(JwtRegisteredClaimNames.Jti,tokenModelJWT.Uid.ToString()),
            //    new Claim("Role",tokenModelJWT.Role),
            //    new Claim(JwtRegisteredClaimNames.Iat,dateTime.ToString(),ClaimValueTypes.Integer64)
            //};
            ////密钥
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secreKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                issuer: "CRM.Core",
                claims: claims,     //声明集合
                expires: dateTime.AddHours(2),
                signingCredentials: creds
                );

            var jwtHandler = new JwtSecurityTokenHandler();
            var encodedJwt = jwtHandler.WriteToken(jwt);

            return encodedJwt;
        }

        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="jwtStr"></param>
        /// <returns></returns>
        public static TokenModelJWT SerializeJWT(string jwtStr)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtSecurityToken = jwtHandler.ReadJwtToken(jwtStr);
            object role = new object();
            try
            {
                jwtSecurityToken.Payload.TryGetValue("Role", out role);
            }
            catch (Exception)
            {
                throw;
            }
            var modelJWT = new TokenModelJWT
            {
                Uid = (jwtSecurityToken.Id).ObjToInt(),
                Role = role.ObjToString()
            };

            return modelJWT;
        }
    }

    /// <summary>
    /// 令牌类，主要存储个人角色信息，自己简单写一个，也可以是你登陆的时候的用户信息，或者其他
    /// </summary>
    public class TokenModelJWT
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Uid { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// 职能
        /// </summary>
        public string Work { get; set; }
    }
}
