using CRM.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CRM.IRepository.IBase
{
    /// <summary>
    /// 2019.04.11      Rui     仓储层基类接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseRepository<T> where T : class
    {
        #region 新增操作

        /// <summary>
        /// 新增操作
        /// </summary>
        /// <param name="model">实体类</param>
        /// <param name="returnAction">新增时，返回动作</param>
        /// <param name="allowColumns">允许指定插入的列</param>
        /// <returns></returns>
        Task<dynamic> Add(T model, SqlSugarEnums.SqlSugarAddReturnAction returnAction, Expression<Func<T, object>> allowColumns = null);

        #endregion
    }
}
