using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CRM.Model.RequestModels
{
    public class UserAddRequest : BaseRequestModel
    {
        /// <summary>
        /// 姓名
        /// </summary>
        [Required(ErrorMessage = "名称必填")]
        public string Name { get; set; }
    }
}
