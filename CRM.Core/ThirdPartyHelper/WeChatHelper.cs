using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CRM.Core.Helpers;

namespace CRM.Core.ThirdPartyHelper
{
    /// <summary>
    /// 2020.03.02      Rui     微信帮助类，涉及到接口获取，签命验证，解密
    /// </summary>
    public class WeChatHelper
    {
        #region 微信接口请求

        /// <summary>
        /// auth.code2Session，根据小程序code，获取数据
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<string> GetOpenIdAndSessionKeyAsync(string code)
        {
            //微信接口地址
            string url = "https://api.weixin.qq.com/sns/jscode2session";
            //授权类型，此处只需填写 authorization_code
            var grantType = "authorization_code";
            //创建请求参数
            var paramsDic = new Dictionary<string, string>();
            paramsDic.Add("appid", ConstHelper.weChatAppId);
            paramsDic.Add("secret", ConstHelper.weChatSecret);
            paramsDic.Add("js_code", code);
            paramsDic.Add("grant_type", grantType);

            return await HttpHelper.GetAsync(url, paramsDic);
        }

        #endregion

        #region 解密算法

        /// <summary>
        /// 微信Aes解密算法（微信算法均调用改方法），与SecurityHelper中的Aes不同
        /// </summary>
        /// <param name="decryptString">解密字符串</param>
        /// <param name="key">SessionKey</param>
        /// <param name="ivString">Iv向量</param>
        /// <returns></returns>
        public static string WeChatAesDecrypt(string decryptString, string key, string ivString)
        {
            try
            {
                //创建解密器生成工具实例
                AesCryptoServiceProvider aes = new AesCryptoServiceProvider();

                //设置解密器参数
                aes.Mode = CipherMode.CBC;
                aes.BlockSize = 128;
                aes.Padding = PaddingMode.PKCS7;

                //格式化待处理字符串 base64处理
                byte[] byte_encryptedData = Convert.FromBase64String(decryptString);
                byte[] byte_sessionKey = Convert.FromBase64String(key);
                byte[] byte_iv = Convert.FromBase64String(ivString);

                aes.IV = byte_iv;
                aes.Key = byte_sessionKey;
                //根据设置好的数据生成解密器实例
                ICryptoTransform transform = aes.CreateDecryptor();

                //解密
                byte[] final = transform.TransformFinalBlock(byte_encryptedData, 0, byte_encryptedData.Length);

                //生成结果
                return Encoding.UTF8.GetString(final);
            }
            catch
            {
                return string.Empty;
            }
        }

        #endregion
    }
}
