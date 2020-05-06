using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Core.Helpers
{
    /// <summary>
    /// 2020.03.01      Rui     封装Http同步、异步请求
    /// </summary>
    public class HttpHelper
    {
        #region Get 同步、异步请求

        /// <summary>
        /// Get同步请求，无参数
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <returns></returns>
        public static string Get(string url)
        {
            Task<string> result = GetAsync(url);
            result.Wait();
            return result.Result;
        }

        /// <summary>
        /// Get同步请求，有参数
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="paramsDic">请求参数</param>
        /// <returns></returns>
        public static string Get(string url, Dictionary<string, string> paramsDic)
        {
            var newParams = GetQueryString(paramsDic);
            string newUrl = $"{url}{newParams}";
            return Get(newUrl);
        }

        /// <summary>
        /// get异步请求，无参数
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <returns></returns>
        public static async Task<string> GetAsync(string url)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage resp = await client.GetAsync(url);
            HttpContent respContent = resp.Content;
            return await respContent.ReadAsStringAsync();
        }

        /// <summary>
        /// Get异步请求，有参数
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="paramsDic">请求参数</param>
        /// <returns></returns>
        public static async Task<string> GetAsync(string url, Dictionary<string, string> paramsDic)
        {
            var newParams = GetQueryString(paramsDic);
            string newUrl = $"{url}{newParams}";
            return await GetAsync(newUrl);
        }

        #endregion

        #region Post 同步、异步请求

        /// <summary>
        /// Post同步请求，无参数
        /// </summary>
        /// <param name="url"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string Post(string url, string content = "")
        {
            Task<string> str = PostAsync(url, content);
            str.Wait();
            return str.Result;
        }

        /// <summary>
        /// Post同步请求，有参数
        /// </summary>
        /// <param name="url"></param>
        /// <param name="paramsDic"></param>
        /// <returns></returns>
        public static string Post(string url, Dictionary<string, string> paramsDic)
        {
            Task<string> str = PostAsync(url, paramsDic);
            str.Wait();
            return str.Result;
        }

        /// <summary>
        /// Post同步请求，无参数
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<string> PostAsync(string url, string content = "")
        {
            HttpClient client = new HttpClient();
            using (MemoryStream ms = new MemoryStream())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(content);
                ms.Write(bytes, 0, bytes.Length);
                //设置指针读取位置，否则发送无效
                ms.Seek(0, SeekOrigin.Begin);
                HttpContent hc = new StreamContent(ms);
                HttpResponseMessage resp = await client.PostAsync(url, hc);
                return await resp.Content.ReadAsStringAsync();
            }
        }

        /// <summary>
        /// Post同步请求，有参数
        /// </summary>
        /// <param name="url"></param>
        /// <param name="paramsDic"></param>
        /// <returns></returns>
        public static async Task<string> PostAsync(string url, Dictionary<string, string> paramsDic)
        {
            HttpClient client = new HttpClient();
            FormUrlEncodedContent data = new FormUrlEncodedContent(paramsDic);
            var r = await client.PostAsync(url, data);
            return await r.Content.ReadAsStringAsync();
        }

        #endregion

        /// <summary>
        /// 参数拼接，参数之间用&连接 如：?a=1&b=2&c=3
        /// </summary>
        /// <param name="paramsDic"></param>
        /// <returns></returns>
        public static string GetQueryString(Dictionary<string, string> paramsDic)
        {
            StringBuilder builder = new StringBuilder();
            if (paramsDic.Count > 0)
            {
                builder.Append("?");
                int i = 0;
                foreach (var item in paramsDic)
                {
                    if (i > 0)
                    {
                        builder.Append("&");
                    }
                    builder.Append($"{item.Key}={item.Value}");
                    i++;
                }
            }

            return builder.ToString();
        }
    }
}
