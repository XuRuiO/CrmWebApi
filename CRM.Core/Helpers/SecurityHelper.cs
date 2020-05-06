using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CRM.Core.Helpers
{
    /// <summary>
    /// 2020.03.01      Rui      安全帮助类，涉及到所有（非第三方接口签名验证和解密）需要加密、解密得方法
    /// </summary>
    public class SecurityHelper
    {
        #region MD5加密方法，字符串均为大写

        /// <summary>
        /// 16位MD5加密
        /// </summary>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public static string Md5Encrypt16(string password)
        {
            try
            {
                //实例化一个md5对像
                using (var md5 = MD5.Create())
                {
                    string resultStr = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(password)), 4, 8);
                    //BitConverter转换出来的字符串会在每个字符中间产生一个分隔符，需要去除掉
                    resultStr = resultStr.Replace("-", string.Empty);
                    return resultStr.ToUpper();
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 32位MD5加密
        /// </summary>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public static string MD5Encrypt32(string password)
        {
            try
            {
                //实例化一个md5对像
                using (var md5 = MD5.Create())
                {
                    string resultStr = string.Empty;
                    //加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
                    byte[] charArray = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
                    //通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
                    foreach (var item in charArray)
                    {
                        //将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                        resultStr = string.Concat(resultStr, item.ToString("X"));
                    }

                    return resultStr.ToUpper();
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 64位MD5加密
        /// </summary>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public static string MD5Encrypt64(string password)
        {
            try
            {
                //实例化一个md5对像
                using (var md5 = MD5.Create())
                {
                    //加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
                    byte[] charArray = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
                    return Convert.ToBase64String(charArray).ToUpper();
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        #endregion

        #region DES加密/解密，密钥长度为16位

        /*
         * Des说明：
         * 1、加密的密钥必须是16位，因为是通过AES处理的Create，AES内置的位数为16位。
         * 2、加密结果返回Base64字符格式
         */

        //默认密钥向量 
        private static byte[] Keys = { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };

        /// <summary> 
        /// DES加密
        /// </summary> 
        /// <param name="encryptString">待加密的字符串</param> 
        /// <param name="encryptKey">加密密钥,要求为16位</param> 
        /// <returns>加密成功返回加密后的字符串，失败返回空</returns> 
        public static string DESEncrypt(string encryptString, string encryptKey)
        {
            try
            {
                if (encryptKey.Length != 16)
                {
                    return string.Empty;
                }
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 16));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                var DCSP = Aes.Create();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary> 
        /// DES解密
        /// </summary> 
        /// <param name="decryptString">待解密的字符串</param> 
        /// <param name="decryptKey">解密密钥,要求为16位,和加密密钥相同</param> 
        /// <returns>解密成功返回解密后的字符串，失败返回空</returns> 
        public static string DESDecrypt(string decryptString, string decryptKey)
        {
            try
            {
                if (decryptKey.Length != 16)
                {
                    return string.Empty;
                }
                byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey.Substring(0, 16));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                var DCSP = Aes.Create();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                Byte[] inputByteArrays = new byte[inputByteArray.Length];
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return string.Empty;
            }

        }

        #endregion

        #region AES加密/解密，密钥长度为16位char字符（加密算法比DES更好，推荐使用）

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="encryptString">待加密字符串</param>
        /// <param name="key">密钥（需要16位的char类型）</param>
        /// <returns></returns>
        public static string AESEncrypt(string encryptString, string key)
        {
            try
            {
                //utf-8编码字节数组
                var encryptKey = Encoding.UTF8.GetBytes(key);
                //创建aes对象
                using (Aes aes = Aes.Create())
                {
                    aes.Padding = PaddingMode.PKCS7;
                    //创建加密机
                    using (var encryptor = aes.CreateEncryptor(encryptKey, aes.IV))
                    {
                        //内存流
                        using (var ms = new MemoryStream())
                        {
                            //加密流
                            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                            {
                                //流写入器
                                using (var sw = new StreamWriter(cs))
                                {
                                    sw.Write(encryptString);
                                }
                                //初始向量
                                var iv = aes.IV;

                                //加密内容字节数组
                                var encryptedContent = ms.ToArray();
                                var result = new byte[iv.Length + encryptedContent.Length];
                                //块拷贝
                                Buffer.BlockCopy(src: iv, srcOffset: 0, dst: result, dstOffset: 0, count: iv.Length);
                                Buffer.BlockCopy(src: encryptedContent, srcOffset: 0, dst: result, dstOffset: iv.Length, count: encryptedContent.Length);
                                //base64编码
                                return Convert.ToBase64String(result);
                            }
                        }
                    }
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="decryptString">待解密字符串</param>
        /// <param name="key">密钥（需要16位的char类型）</param>
        /// <returns></returns>
        public static string AESDecrypt(string decryptString, string key)
        {
            try
            {
                //base64解码
                var fullCipher = Convert.FromBase64String(decryptString);
                var iv = new byte[16];
                var cipher = new byte[fullCipher.Length - iv.Length];
                //块拷贝
                Buffer.BlockCopy(src: fullCipher, srcOffset: 0, dst: iv, 0, iv.Length);
                Buffer.BlockCopy(src: fullCipher, srcOffset: iv.Length, dst: cipher, 0, fullCipher.Length - iv.Length);
                var decryptKey = Encoding.UTF8.GetBytes(key);
                string result;
                //创建aes对象
                using (Aes aes = Aes.Create())
                {
                    //创建解密机
                    using (var decryptor = aes.CreateDecryptor(decryptKey, iv))
                    {
                        //内存流
                        using (var ms = new MemoryStream(cipher))
                        {
                            //加密流
                            using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                            {
                                //流读取器
                                using (var sr = new StreamReader(cs))
                                {
                                    result = sr.ReadToEnd();
                                }
                            }
                        }
                    }
                }
                return result;
            }
            catch
            {
                return string.Empty;
            }
        }

        #endregion

        #region Base64位加密/解密

        /// <summary>
        /// Base64位加密，将字符串转换成base64格式,使用UTF8字符集
        /// </summary>
        /// <param name="content">加密内容</param>
        /// <returns></returns>
        public static string Base64Encode(string content)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(content);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Base64位解密，将base64格式转换utf8
        /// </summary>
        /// <param name="content">解密内容</param>
        /// <returns></returns>
        public static string Base64Decode(string content)
        {
            byte[] bytes = Convert.FromBase64String(content);
            return Encoding.UTF8.GetString(bytes);
        }

        #endregion

        #region SHA1加密

        /// <summary>
        /// 用SHA1加密字符串
        /// </summary>
        /// <param name="encryptString">要加密的字符串</param>
        /// <param name="isReplace">是否替换掉加密后的字符串中的"-"字符</param>
        /// <param name="isToLower">是否把加密后的字符串转小写</param>
        /// <returns></returns>
        public static string SHA1Encrypt(string encryptString, bool isReplace = true, bool isToLower = false)
        {
            var sha1 = SHA1.Create();
            byte[] hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(encryptString));
            string shaStr = BitConverter.ToString(hash);
            if (isReplace)
            {
                shaStr = shaStr.Replace("-", "");
            }
            if (isToLower)
            {
                shaStr = shaStr.ToLower();
            }
            return shaStr;
        }

        #endregion
    }
}
