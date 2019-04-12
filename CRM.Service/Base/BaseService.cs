using CRM.Core.Models;
using CRM.IRepository.IBase;
using CRM.IService.IBase;
using CRM.Repository.Base;
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
        //采用autoioc注入时，将控制器引用的依赖项 CRM.Repository 去掉
        public IBaseRepository<T> baseDal = new BaseRepository<T>();

        #region 新增操作

        /// <summary>
        /// 新增操作
        /// </summary>
        /// <param name="model">实体类</param>
        /// <param name="returnAction">新增时，返回动作</param>
        /// <param name="allowColumns">允许指定插入的列</param>
        /// <returns></returns>
        public async Task<dynamic> Add(T model, SqlSugarEnums.SqlSugarAddReturnAction returnAction, Expression<Func<T, object>> allowColumns = null)
            => await baseDal.Add(model, returnAction, allowColumns);

        /// <summary>
        /// 新增操作（批量新增）
        /// </summary>
        /// <param name="model">实体类</param>
        /// <param name="returnAction">新增时，返回动作</param>
        /// <param name="allowColumns">允许指定插入的列</param>
        public async Task<dynamic> AddList(List<T> model, SqlSugarEnums.SqlSugarAddReturnAction returnAction, Expression<Func<T, object>> allowColumns = null)
            => await baseDal.AddList(model, returnAction, allowColumns);

        #endregion

        #region 更新操作

        /// <summary>
        /// 更新操作，根据实体更新（主键要有值，主键是更新条件）
        /// </summary>
        /// <param name="model">实体类</param>
        /// <param name="allowColumns">允许指定更新的列</param>
        /// <returns></returns>
        public async Task<bool> Update(T model, Expression<Func<T, object>> allowColumns)
            => await baseDal.Update(model, allowColumns);

        /// <summary>
        /// 更新操作（批量更新），根据实体更新（主键要有值，主键是更新条件）
        /// </summary>
        /// <param name="model">实体类</param>
        /// <param name="allowColumns">允许指定更新的列</param>
        /// <returns></returns>
        public async Task<bool> UpdateList(List<T> model, Expression<Func<T, object>> allowColumns)
            => await baseDal.UpdateList(model, allowColumns);

        /// <summary>
        /// 更新操作，根据条件进行更新
        /// </summary>
        /// <param name="model">实体类</param>
        /// <param name="where">更新条件</param>
        /// <param name="allowColumns">允许指定更新的列</param>
        /// <returns></returns>
        public async Task<bool> UpdateByWhere(T model, Expression<Func<T, bool>> where, Expression<Func<T, object>> allowColumns)
            => await baseDal.UpdateByWhere(model, where, allowColumns);

        /// <summary>
        /// 更新操作（批量更新），根据条件进行更新
        /// </summary>
        /// <param name="model">实体类</param>
        /// <param name="where">更新条件</param>
        /// <param name="allowColumns">允许指定更新的列</param>
        /// <returns></returns>
        public async Task<bool> UpdateListByWhere(List<T> model, Expression<Func<T, bool>> where, Expression<Func<T, object>> allowColumns)
            => await baseDal.UpdateListByWhere(model, where, allowColumns);

        #endregion

        #region 删除操作

        /// <summary>
        /// 真删除操作，根据lambda表达式从表中删除指定数据。（慎用）
        /// </summary>
        /// <param name="where">删除条件</param>
        /// <returns></returns>
        public async Task<bool> DeleteTrue(Expression<Func<T, bool>> where)
            => await baseDal.DeleteTrue(where);

        /// <summary>
        /// 假删除操作，实际是根据实体更新（主键要有值，主键是更新条件），更新指定列
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns></returns>
        public async Task<bool> DeleteFalse(T model)
            => await baseDal.DeleteFalse(model);

        #endregion

        #region 查询操作

        /// <summary>
        /// 查询单条
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="isNoLock">是否无锁模式，默认无锁</param>
        /// <returns></returns>
        public async Task<T> QueryFirst(Expression<Func<T, bool>> where, bool isNoLock = true)
            => await baseDal.QueryFirst(where, isNoLock);

        /// <summary>
        /// 查询单条，可以根据条件排序
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="orders">排序条件</param>
        /// <param name="isNoLock">是否无锁模式，默认无锁</param>
        /// <returns></returns>
        public async Task<T> QueryFirst(Expression<Func<T, bool>> where, List<SqlSugarOrder<T>> orders, bool isNoLock = true)
            => await baseDal.QueryFirst(where, orders, isNoLock);

        #endregion
    }
}
