using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Helpers
{
    /// <summary>
    /// 2020.03.02      Rui     常量帮助类，存放系统中所有的常量参数配置（经常会变动的配置文件，建议存放在appsettings.json中）
    /// </summary>
    public struct ConstHelper
    {
        #region 微信小程序-基本常量配置

        /// <summary>
        /// 小程序appId
        /// </summary>
        public const string weChatAppId = "wx2bb5db26548bb62d";

        /// <summary>
        /// 小程序appSecret
        /// </summary>
        public const string weChatSecret = "b835f1fb4627b458111efb37452385f0";

        #endregion

        #region 系统内部加密/解密使用的密钥

        /// <summary>
        /// （微信模块相关的密钥）AES加密/解密密钥，8位字节以上，8的倍数
        /// </summary>
        public const string weChatAesEncryptDecryptKey = "nhV$OdZCobPT1J2W";

        #endregion
    }
}
