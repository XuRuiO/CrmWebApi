﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Model.ViewPageModels
{
    /// <summary>
    /// 基类分页模型（响应），不需要继承该类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BasePageModel<T>
    {
        /// <summary>
        /// 集合数据
        /// </summary>
        public IList<T> Models { get; set; }

        /// <summary>
        /// 总数
        /// </summary>
        public int Total { get; set; }
    }
}
