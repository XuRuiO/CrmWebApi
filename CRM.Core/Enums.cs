using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        /// <summary>
        /// 请求接口的设备类型
        /// </summary>
        public enum TerminalType
        {
            [Description("苹果")]
            Ios = 1,
            [Description("安卓")]
            Android = 2,
            [Description("微信")]
            WeChat = 3,
            [Description("电脑端")]
            PC = 4
        }
    }
}
