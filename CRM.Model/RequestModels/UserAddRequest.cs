using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Model.RequestModels
{
    public class UserAddRequest : BaseRequestModel
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
    }
}
