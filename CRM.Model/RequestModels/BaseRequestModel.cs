using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CRM.Model.RequestModels
{
    /// <summary>
    /// 请求基类，非分页查询请求，请直接继承该类
    /// </summary>
    public class BaseRequestModel
    {
        /// <summary>
        /// 请求接口的移动端类型：1（Ios），2（Android），3（WeChat）,4（PC）
        /// </summary>
        [Required(ErrorMessage = "请求接口的移动端类型不能为空")]
        public int TerminalType { get; set; }
    }
}
