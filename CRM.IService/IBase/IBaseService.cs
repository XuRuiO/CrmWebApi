using CRM.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CRM.IService.IBase
{
    /// <summary>
    /// 2019.04.11      Rui     业务逻辑层基类接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseService<T> where T : class
    {
        #region 新增操作

        /// <summary>
        /// 新增操作
        /// </summary>
        /// <param name="model">实体类</param>
        /// <param name="returnAction">新增时，返回动作</param>
        /// <param name="allowColumns">允许指定插入的列</param>
        /// <returns></returns>
        Task<dynamic> AddAsync(T model, SqlSugarEnums.SqlSugarAddReturnAction returnAction, Expression<Func<T, object>> allowColumns = null);

        /// <summary>
        /// 新增操作（批量新增）
        /// </summary>
        /// <param name="model">实体类</param>
        /// <param name="returnAction">新增时，返回动作</param>
        /// <param name="allowColumns">允许指定插入的列</param>
        Task<dynamic> AddListAsync(List<T> model, SqlSugarEnums.SqlSugarAddReturnAction returnAction, Expression<Func<T, object>> allowColumns = null);

        #endregion

        #region 更新操作

        /// <summary>
        /// 更新操作，根据实体更新（主键要有值，主键是更新条件）
        /// </summary>
        /// <param name="model">实体类</param>
        /// <param name="allowColumns">允许指定更新的列</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(T model, Expression<Func<T, object>> allowColumns);

        /// <summary>
        /// 更新操作（批量更新），根据实体更新（主键要有值，主键是更新条件）
        /// </summary>
        /// <param name="model">实体类</param>
        /// <param name="allowColumns">允许指定更新的列</param>
        /// <returns></returns>
        Task<bool> UpdateListAsync(List<T> model, Expression<Func<T, object>> allowColumns);

        /// <summary>
        /// 更新操作，根据条件进行更新
        /// </summary>
        /// <param name="model">实体类</param>
        /// <param name="where">更新条件</param>
        /// <param name="allowColumns">允许指定更新的列</param>
        /// <returns></returns>
        Task<bool> UpdateByWhereAsync(T model, Expression<Func<T, bool>> where, Expression<Func<T, object>> allowColumns);

        /// <summary>
        /// 更新操作（批量更新），根据条件进行更新
        /// </summary>
        /// <param name="model">实体类</param>
        /// <param name="where">更新条件</param>
        /// <param name="allowColumns">允许指定更新的列</param>
        /// <returns></returns>
        Task<bool> UpdateListByWhereAsync(List<T> model, Expression<Func<T, bool>> where, Expression<Func<T, object>> allowColumns);

        #endregion

        #region 删除操作

        /// <summary>
        /// 真删除操作，根据lambda表达式从表中删除指定数据。（慎用）
        /// </summary>
        /// <param name="where">删除条件</param>
        /// <returns></returns>
        Task<bool> DeleteTrueAsync(Expression<Func<T, bool>> where);

        /// <summary>
        /// 假删除操作，实际是根据实体更新（主键要有值，主键是更新条件），更新指定列
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns></returns>
        Task<bool> DeleteFalseAsync(T model);

        #endregion

        #region 查询操作

        /// <summary>
        /// 查询数量
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="isNoLock">是否无锁模式，默认无锁</param>
        /// <returns></returns>
        Task<long> QueryCountAsync(Expression<Func<T, bool>> where, bool isNoLock = true);

        /// <summary>
        /// 查询单条
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="isNoLock">是否无锁模式，默认无锁</param>
        /// <returns></returns>
        Task<T> QueryFirstAsync(Expression<Func<T, bool>> where, bool isNoLock = true);

        /// <summary>
        /// 查询单条，可以根据条件排序
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="orders">排序条件</param>
        /// <param name="isNoLock">是否无锁模式，默认无锁</param>
        /// <returns></returns>
        Task<T> QueryFirstAsync(Expression<Func<T, bool>> where, List<SqlSugarOrder<T>> orders, bool isNoLock = true);

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="isNoLock">是否无锁模式，默认无锁</param>
        /// <returns></returns>
        Task<List<T>> QueryAllAsync(bool isNoLock = true);

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="isNoLock">是否无锁模式，默认无锁</param>
        /// <returns></returns>
        Task<List<T>> QueryAllAsync(Expression<Func<T, bool>> where, bool isNoLock = true);

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="orders">排序条件</param>
        /// <param name="isNoLock">是否无锁模式，默认无锁</param>
        /// <returns></returns>
        Task<List<T>> QueryAllAsync(Expression<Func<T, bool>> where, List<SqlSugarOrder<T>> orders, bool isNoLock = true);

        #endregion
    }
}
