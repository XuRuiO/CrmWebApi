using CRM.IRepository.IBase;
using System;
using System.Collections.Generic;
using System.Text;
using SqlSugar;
using CRM.Repository.SqlSugarOrm;
using System.Threading.Tasks;
using System.Linq.Expressions;
using CRM.Core.Models;
using System.Linq;

namespace CRM.Repository.Base
{
    /// <summary>
    /// 2019.04.11      Rui     仓储层基类实现类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseRepository<T> : IBaseRepository<T> where T : class, new()
    {
        /// <summary>
        /// 定义一个私有的SqlSugarClient用来接收DB对象
        /// </summary>
        private SqlSugarClient db { get; set; }

        /// <summary>
        /// 构造函数获取SqlSugarClient对象
        /// </summary>
        public BaseRepository()
        {
            db = DbContext.DB;
        }

        #region 新增操作

        /// <summary>
        /// 新增操作
        /// </summary>
        /// <param name="model">实体类</param>
        /// <param name="returnAction">新增时，返回动作</param>
        /// <param name="allowColumns">允许指定插入的列</param>
        /// <returns></returns>
        public async Task<dynamic> Add(T model, SqlSugarEnums.SqlSugarAddReturnAction returnAction, Expression<Func<T, object>> allowColumns = null)
        {
            switch (returnAction)
            {
                case SqlSugarEnums.SqlSugarAddReturnAction.None:
                    return allowColumns == null
                        ? await db.Insertable(model).ExecuteCommandAsync()
                        : await db.Insertable(model).InsertColumns(allowColumns).ExecuteCommandAsync();
                case SqlSugarEnums.SqlSugarAddReturnAction.ReturnIdentity:
                    return allowColumns == null
                        ? await db.Insertable(model).ExecuteReturnIdentityAsync()
                        : await db.Insertable(model).InsertColumns(allowColumns).ExecuteReturnIdentityAsync();
                case SqlSugarEnums.SqlSugarAddReturnAction.ReturnBigIdentity:
                    return allowColumns == null
                        ? await db.Insertable(model).ExecuteReturnBigIdentityAsync()
                        : await db.Insertable(model).InsertColumns(allowColumns).ExecuteReturnBigIdentityAsync();
                case SqlSugarEnums.SqlSugarAddReturnAction.ReturnEntity:
                    return allowColumns == null
                        ? await db.Insertable(model).ExecuteReturnEntityAsync()
                        : await db.Insertable(model).InsertColumns(allowColumns).ExecuteReturnEntityAsync();
                case SqlSugarEnums.SqlSugarAddReturnAction.IdentityIntoEntity:
                    return allowColumns == null
                        ? await db.Insertable(model).ExecuteCommandIdentityIntoEntityAsync()
                        : await db.Insertable(model).InsertColumns(allowColumns).ExecuteCommandIdentityIntoEntityAsync();
                default:
                    return allowColumns == null
                        ? await db.Insertable(model).ExecuteCommandAsync()
                        : await db.Insertable(model).InsertColumns(allowColumns).ExecuteCommandAsync();
            }
        }

        /// <summary>
        /// 新增操作（批量新增）
        /// </summary>
        /// <param name="model">实体类</param>
        /// <param name="returnAction">新增时，返回动作</param>
        /// <param name="allowColumns">允许指定插入的列</param>
        /// <returns></returns>
        public async Task<dynamic> AddList(List<T> model, SqlSugarEnums.SqlSugarAddReturnAction returnAction, Expression<Func<T, object>> allowColumns = null)
        {
            switch (returnAction)
            {
                case SqlSugarEnums.SqlSugarAddReturnAction.None:
                    return allowColumns == null
                        ? await db.Insertable(model).ExecuteCommandAsync()
                        : await db.Insertable(model).InsertColumns(allowColumns).ExecuteCommandAsync();
                case SqlSugarEnums.SqlSugarAddReturnAction.ReturnIdentity:
                    return allowColumns == null
                        ? await db.Insertable(model).ExecuteReturnIdentityAsync()
                        : await db.Insertable(model).InsertColumns(allowColumns).ExecuteReturnIdentityAsync();
                case SqlSugarEnums.SqlSugarAddReturnAction.ReturnBigIdentity:
                    return allowColumns == null
                        ? await db.Insertable(model).ExecuteReturnBigIdentityAsync()
                        : await db.Insertable(model).InsertColumns(allowColumns).ExecuteReturnBigIdentityAsync();
                case SqlSugarEnums.SqlSugarAddReturnAction.ReturnEntity:
                    return allowColumns == null
                        ? await db.Insertable(model).ExecuteReturnEntityAsync()
                        : await db.Insertable(model).InsertColumns(allowColumns).ExecuteReturnEntityAsync();
                case SqlSugarEnums.SqlSugarAddReturnAction.IdentityIntoEntity:
                    return allowColumns == null
                        ? await db.Insertable(model).ExecuteCommandIdentityIntoEntityAsync()
                        : await db.Insertable(model).InsertColumns(allowColumns).ExecuteCommandIdentityIntoEntityAsync();
                default:
                    return allowColumns == null
                        ? await db.Insertable(model).ExecuteCommandAsync()
                        : await db.Insertable(model).InsertColumns(allowColumns).ExecuteCommandAsync();
            }
        }

        #endregion

        #region 修改操作

        /// <summary>
        /// 更新操作，根据实体更新（主键要有值，主键是更新条件）
        /// </summary>
        /// <param name="model">实体类</param>
        /// <param name="allowColumns">允许指定更新的列</param>
        /// <returns></returns>
        public async Task<bool> Update(T model, Expression<Func<T, object>> allowColumns)
        {
            var sugarUpdate = await Task.Run(() => db.Updateable(model));
            if (allowColumns != null)
            {
                sugarUpdate = await Task.Run(() => sugarUpdate.UpdateColumns(allowColumns));
            }

            return await sugarUpdate.ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// 更新操作（批量更新），根据实体更新（主键要有值，主键是更新条件）
        /// </summary>
        /// <param name="model">实体类</param>
        /// <param name="allowColumns">允许指定更新的列</param>
        /// <returns></returns>
        public async Task<bool> UpdateList(List<T> model, Expression<Func<T, object>> allowColumns)
        {
            var sugarUpdate = await Task.Run(() => db.Updateable(model));
            if (allowColumns != null)
            {
                sugarUpdate = await Task.Run(() => sugarUpdate.UpdateColumns(allowColumns));
            }

            return await sugarUpdate.ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// 更新操作，根据条件进行更新
        /// </summary>
        /// <param name="model">实体类</param>
        /// <param name="where">更新条件</param>
        /// <param name="allowColumns">允许指定更新的列</param>
        /// <returns></returns>
        public async Task<bool> UpdateByWhere(T model, Expression<Func<T, bool>> where, Expression<Func<T, object>> allowColumns)
        {
            var sugarUpdate = await Task.Run(() => db.Updateable(model));
            if (allowColumns != null)
            {
                sugarUpdate = await Task.Run(() => sugarUpdate.UpdateColumns(allowColumns));
            }

            return await sugarUpdate.Where(where).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// 更新操作（批量更新），根据条件进行更新
        /// </summary>
        /// <param name="model">实体类</param>
        /// <param name="where">更新条件</param>
        /// <param name="allowColumns">允许指定更新的列</param>
        /// <returns></returns>
        public async Task<bool> UpdateListByWhere(List<T> model, Expression<Func<T, bool>> where, Expression<Func<T, object>> allowColumns)
        {
            var sugarUpdate = await Task.Run(() => db.Updateable(model));
            if (allowColumns != null)
            {
                sugarUpdate = await Task.Run(() => sugarUpdate.UpdateColumns(allowColumns));
            }

            return await sugarUpdate.Where(where).ExecuteCommandAsync() > 0;
        }

        #endregion

        #region 删除操作

        /// <summary>
        /// 真删除操作，根据lambda表达式从表中删除指定数据。（慎用）
        /// </summary>
        /// <param name="where">删除条件</param>
        /// <returns></returns>
        public async Task<bool> DeleteTrue(Expression<Func<T, bool>> where)
        {
            return await db.Deleteable<T>(where).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// 假删除操作，实际是根据实体更新（主键要有值，主键是更新条件），更新指定列
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns></returns>
        public async Task<bool> DeleteFalse(T model)
        {
            return await db.Updateable(model).ExecuteCommandAsync() > 0;
        }

        #endregion

        #region 查询操作

        /// <summary>
        /// 查询单条
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="isNoLock">是否无锁模式，默认无锁</param>
        /// <returns></returns>
        public async Task<T> QueryFirst(Expression<Func<T, bool>> where, bool isNoLock = true)
        {
            var query = await Task.Run(() => db.Queryable<T>());

            if (isNoLock)
            {
                query = await Task.Run(() => query.With(SqlWith.NoLock));
            }

            return await query.FirstAsync(where);
        }

        /// <summary>
        /// 查询单条，可以根据条件排序
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="orders">排序条件</param>
        /// <param name="isNoLock">是否无锁模式，默认无锁</param>
        /// <returns></returns>
        public async Task<T> QueryFirst(Expression<Func<T, bool>> where, List<SqlSugarOrder<T>> orders, bool isNoLock = true)
        {
            var query = await Task.Run(() => db.Queryable<T>());

            if (isNoLock)
            {
                query = await Task.Run(() => query.With(SqlWith.NoLock));
            }

            if (orders != null)
            {
                query = await Task.Run(() => query = orders.Aggregate(query, (current, item) => current.OrderBy(item.OrderExpn, item.isDesc ? OrderByType.Desc : OrderByType.Asc)));
            }

            return await query.FirstAsync();
        }

        #endregion
    }
}
