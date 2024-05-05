using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Model.ViewModels
{
    /// <summary>
    /// Token信息视图
    /// </summary>
    public class TokenInfoView
    {
        public string Token { get; set; }

        public string Type { get; set; }

        public string Expiration { get; set; }
    }
}
