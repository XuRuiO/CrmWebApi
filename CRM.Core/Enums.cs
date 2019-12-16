using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core
{
    /// <summary>
    /// 枚举类，存放的基本都是整个解决方案需要使用的公共枚举
    /// </summary>
    public class Enums
    {
        /// <summary>
        /// 启用状态，表系统参数
        /// </summary>
        public enum TableEnabled
        {
            禁用 = 0,
            启用 = 1
        }

        /// <summary>
        /// 删除状态，表系统参数
        /// </summary>
        public enum TableDeleted
        {
            未删除 = 0,
            已删除 = 1
        }
    }
}
