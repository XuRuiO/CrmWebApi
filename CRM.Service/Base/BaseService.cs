using CRM.Core.Models;
using CRM.IRepository.IBase;
using CRM.IService.IBase;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Service.Base
{
    /// <summary>
    /// 2019.04.11      Rui     业务逻辑层基类实现类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseService<T> : IBaseService<T> where T : class, new()
    {
        //通过在子类的构造函数中注入，这里是基类，不用构造函数
        public IBaseRepository<T> baseDal;

        #region 新增操作

        /// <summary>
        /// 新增操作
        /// </summary>
        /// <param name="model">实体类</param>
        /// <param name="returnAction">新增时，返回动作</param>
        /// <param name="allowColumns">允许指定插入的列</param>
        /// <returns></returns>
        public async Task<dynamic> AddAsync(T model, SqlSugarEnums.SqlSugarAddReturnAction returnAction, Expression<Func<T, object>> allowColumns = null)
            => await baseDal.AddAsync(model, returnAction, allowColumns);

        /// <summary>
        /// 新增操作（批量新增）
        /// </summary>
        /// <param name="model">实体类</param>
        /// <param name="returnAction">新增时，返回动作</param>
        /// <param name="allowColumns">允许指定插入的列</param>
        public async Task<dynamic> AddListAsync(List<T> model, SqlSugarEnums.SqlSugarAddReturnAction returnAction, Expression<Func<T, object>> allowColumns = null)
            => await baseDal.AddListAsync(model, returnAction, allowColumns);

        #endregion

        #region 更新操作

        /// <summary>
        /// 更新操作，根据实体更新（主键要有值，主键是更新条件）
        /// </summary>
        /// <param name="model">实体类</param>
        /// <param name="allowColumns">允许指定更新的列</param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(T model, Expression<Func<T, object>> allowColumns)
            => await baseDal.UpdateAsync(model, allowColumns);

        /// <summary>
        /// 更新操作（批量更新），根据实体更新（主键要有值，主键是更新条件）
        /// </summary>
        /// <param name="model">实体类</param>
        /// <param name="allowColumns">允许指定更新的列</param>
        /// <returns></returns>
        public async Task<bool> UpdateListAsync(List<T> model, Expression<Func<T, object>> allowColumns)
            => await baseDal.UpdateListAsync(model, allowColumns);

        /// <summary>
        /// 更新操作，根据条件进行更新
        /// </summary>
        /// <param name="model">实体类</param>
        /// <param name="where">更新条件</param>
        /// <param name="allowColumns">允许指定更新的列</param>
        /// <returns></returns>
        public async Task<bool> UpdateByWhereAsync(T model, Expression<Func<T, bool>> where, Expression<Func<T, object>> allowColumns)
            => await baseDal.UpdateByWhereAsync(model, where, allowColumns);

        /// <summary>
        /// 更新操作（批量更新），根据条件进行更新
        /// </summary>
        /// <param name="model">实体类</param>
        /// <param name="where">更新条件</param>
        /// <param name="allowColumns">允许指定更新的列</param>
        /// <returns></returns>
        public async Task<bool> UpdateListByWhereAsync(List<T> model, Expression<Func<T, bool>> where, Expression<Func<T, object>> allowColumns)
            => await baseDal.UpdateListByWhereAsync(model, where, allowColumns);

        #endregion

        #region 删除操作

        /// <summary>
        /// 真删除操作，根据lambda表达式从表中删除指定数据。（慎用）
        /// </summary>
        /// <param name="where">删除条件</param>
        /// <returns></returns>
        public async Task<bool> DeleteTrueAsync(Expression<Func<T, bool>> where)
            => await baseDal.DeleteTrueAsync(where);

        /// <summary>
        /// 假删除操作，实际是根据实体更新（主键要有值，主键是更新条件），更新指定列
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns></returns>
        public async Task<bool> DeleteFalseAsync(T model)
            => await baseDal.DeleteFalseAsync(model);

        #endregion

        #region 查询操作

        /// <summary>
        /// 查询数量
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="isNoLock">是否无锁模式，默认无锁</param>
        /// <returns></returns>
        public async Task<long> QueryCountAsync(Expression<Func<T, bool>> where, bool isNoLock = true)
            => await baseDal.QueryCountAsync(where, isNoLock);

        /// <summary>
        /// 查询单条
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="isNoLock">是否无锁模式，默认无锁</param>
        /// <returns></returns>
        public async Task<T> QueryFirstAsync(Expression<Func<T, bool>> where, bool isNoLock = true)
            => await baseDal.QueryFirstAsync(where, isNoLock);

        /// <summary>
        /// 查询单条，可以根据条件排序
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="orders">排序条件</param>
        /// <param name="isNoLock">是否无锁模式，默认无锁</param>
        /// <returns></returns>
        public async Task<T> QueryFirstAsync(Expression<Func<T, bool>> where, List<SqlSugarOrder<T>> orders, bool isNoLock = true)
            => await baseDal.QueryFirstAsync(where, orders, isNoLock);

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="isNoLock">是否无锁模式，默认无锁</param>
        /// <returns></returns>
        public async Task<List<T>> QueryAllAsync(bool isNoLock = true)
            => await baseDal.QueryAllAsync(isNoLock);

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="isNoLock">是否无锁模式，默认无锁</param>
        /// <returns></returns>
        public async Task<List<T>> QueryAllAsync(Expression<Func<T, bool>> where, bool isNoLock = true)
            => await baseDal.QueryAllAsync(where, isNoLock);

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="orders">排序条件</param>
        /// <param name="isNoLock">是否无锁模式，默认无锁</param>
        /// <returns></returns>
        public async Task<List<T>> QueryAllAsync(Expression<Func<T, bool>> where, List<SqlSugarOrder<T>> orders, bool isNoLock = true)
            => await baseDal.QueryAllAsync(where, orders, isNoLock);

        /// <summary>
        /// 条件分页查询，单表
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="pageInfo">分页信息</param>
        /// <param name="orders">排序条件</param>
        /// <param name="isNoLock">是否无锁模式，默认无锁</param>
        /// <returns></returns>
        public async Task<List<T>> QueryConditionPageAsync(Expression<Func<T, bool>> where, SqlSugarPageInfo pageInfo, List<SqlSugarOrder<T>> orders = null, bool isNoLock = true)
            => await baseDal.QueryConditionPageAsync(where, pageInfo, orders, isNoLock);

        #endregion
    }
}
