﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CRM.Core.Models
{
    /// <summary>
    /// 2019.02.23      Rui     SqlSugar 枚举类
    /// </summary>
    public class SqlSugarEnums
    {
        /// <summary>
        /// SqlSugar新增后动作
        /// </summary>
        public enum SqlSugarAddReturnAction
        {
            /// <summary>
            /// 返回行数，int类型
            /// </summary>
            None = 1,
            /// <summary>
            /// 返回自增，Int类型
            /// </summary>
            ReturnIdentity = 2,
            /// <summary>
            /// 返回自增bigint，long类型
            /// </summary>
            ReturnBigIdentity = 3,
            /// <summary>
            /// 插入并返回实体 ,  只是自identity 添加到 参数的实体里面并返回，没有查2次库，所以有些默认值什么的变动是取不到的你们需要手动进行2次查询获取
            /// </summary>
            ReturnEntity = 4,
            /// <summary>
            /// 插入并返回bool, 并将identity赋值到实体
            /// </summary>
            IdentityIntoEntity = 5
        }

        /// <summary>
        /// 排序枚举
        /// </summary>
        public enum OrderSequence
        {
            /// <summary>
            /// 正序
            /// </summary>
            Asc,
            /// <summary>
            /// 倒序
            /// </summary>
            Desc
        }
    }

    /// <summary>
    /// 分页信息
    /// </summary>
    public class SqlSugarPageInfo
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 页数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalCount { get; set; }
    }

    /// <summary>
    /// 单表排序
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SqlSugarOrder<T>
    {
        /// <summary>
        /// 排序表达式
        /// </summary>
        public Expression<Func<T, object>> OrderExpn { get; set; }

        /// <summary>
        /// 是否降序
        /// </summary>
        public bool IsDesc { get; set; } = true;
    }

    #region 多表排序

    /// <summary>
    /// 排序实体
    /// </summary>
    public class OrderByClause
    {
        /// <summary>
        /// 排序字段
        /// </summary>
        public string Sort { get; set; }

        /// <summary>
        /// 排序类型
        /// </summary>
        public SqlSugarEnums.OrderSequence Order { get; set; }
    }

    #endregion
}
