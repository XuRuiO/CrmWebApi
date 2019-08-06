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
        public async Task<dynamic> AddAsync(T model, SqlSugarEnums.SqlSugarAddReturnAction returnAction, Expression<Func<T, object>> allowColumns = null)
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
        public async Task<dynamic> AddListAsync(List<T> model, SqlSugarEnums.SqlSugarAddReturnAction returnAction, Expression<Func<T, object>> allowColumns = null)
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
        public async Task<bool> UpdateAsync(T model, Expression<Func<T, object>> allowColumns)
        {
            return await db.Updateable(model).UpdateColumnsIF(allowColumns != null, allowColumns).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// 更新操作（批量更新），根据实体更新（主键要有值，主键是更新条件）
        /// </summary>
        /// <param name="model">实体类</param>
        /// <param name="allowColumns">允许指定更新的列</param>
        /// <returns></returns>
        public async Task<bool> UpdateListAsync(List<T> model, Expression<Func<T, object>> allowColumns)
        {
            return await db.Updateable(model).UpdateColumnsIF(allowColumns != null, allowColumns).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// 更新操作，根据条件进行更新
        /// </summary>
        /// <param name="model">实体类</param>
        /// <param name="where">更新条件</param>
        /// <param name="allowColumns">允许指定更新的列</param>
        /// <returns></returns>
        public async Task<bool> UpdateByWhereAsync(T model, Expression<Func<T, bool>> where, Expression<Func<T, object>> allowColumns)
        {
            return await db.Updateable(model).UpdateColumnsIF(allowColumns != null, allowColumns).Where(where).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// 更新操作（批量更新），根据条件进行更新
        /// </summary>
        /// <param name="model">实体类</param>
        /// <param name="where">更新条件</param>
        /// <param name="allowColumns">允许指定更新的列</param>
        /// <returns></returns>
        public async Task<bool> UpdateListByWhereAsync(List<T> model, Expression<Func<T, bool>> where, Expression<Func<T, object>> allowColumns)
        {
            return await db.Updateable(model).UpdateColumnsIF(allowColumns != null, allowColumns).Where(where).ExecuteCommandAsync() > 0;
        }

        #endregion

        #region 删除操作

        /// <summary>
        /// 真删除操作，根据lambda表达式从表中删除指定数据。（慎用）
        /// </summary>
        /// <param name="where">删除条件</param>
        /// <returns></returns>
        public async Task<bool> DeleteTrueAsync(Expression<Func<T, bool>> where)
        {
            return await db.Deleteable<T>(where).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// 假删除操作，实际是根据实体更新（主键要有值，主键是更新条件），更新指定列
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns></returns>
        public async Task<bool> DeleteFalseAsync(T model)
        {
            return await db.Updateable(model).ExecuteCommandAsync() > 0;
        }

        #endregion

        #region 单表查询操作

        /// <summary>
        /// 查询数量
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public async Task<long> QueryCountAsync(Expression<Func<T, bool>> where)
        {
            return await db.Queryable<T>().Where(where).CountAsync();
        }

        /// <summary>
        /// 查询单条
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public async Task<T> QueryFirstAsync(Expression<Func<T, bool>> where)
        {
            return await db.Queryable<T>().Where(where).FirstAsync();
        }

        /// <summary>
        /// 查询单条，可以根据条件排序
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="orders">排序条件</param>
        /// <returns></returns>
        public async Task<T> QueryFirstAsync(Expression<Func<T, bool>> where, List<SqlSugarOrder<T>> orders)
        {
            var query = db.Queryable<T>().Where(where);

            if (orders != null)
            {
                query = orders.Aggregate(query, (current, item) => current.OrderBy(item.OrderExpn, item.IsDesc ? OrderByType.Desc : OrderByType.Asc));
            }

            return await query.FirstAsync();
        }

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="isNoLock"></param>
        /// <returns></returns>
        public async Task<List<T>> QueryAllAsync()
        {
            return await db.Queryable<T>().ToListAsync();
        }

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public async Task<List<T>> QueryAllAsync(Expression<Func<T, bool>> where)
        {
            return await db.Queryable<T>().Where(where).ToListAsync();
        }

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="orders">排序条件</param>
        /// <returns></returns>
        public async Task<List<T>> QueryAllAsync(Expression<Func<T, bool>> where, List<SqlSugarOrder<T>> orders)
        {
            var query = db.Queryable<T>().Where(where);

            if (orders != null)
            {
                query = orders.Aggregate(query, (current, item) => current.OrderBy(item.OrderExpn, item.IsDesc ? OrderByType.Desc : OrderByType.Asc));
            }

            return await query.ToListAsync();
        }

        /// <summary>
        /// 条件分页查询，单表
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="pageInfo">分页信息</param>
        /// <param name="orders">排序条件</param>
        /// <returns></returns>
        public async Task<List<T>> QueryConditionPageAsync(Expression<Func<T, bool>> where, SqlSugarPageInfo pageInfo, List<SqlSugarOrder<T>> orders = null)
        {
            var query = db.Queryable<T>().WhereIF(where != null, where);

            if (orders != null)
            {
                query = orders.Aggregate(query, (current, item) => current.OrderBy(item.OrderExpn, item.IsDesc ? OrderByType.Desc : OrderByType.Asc));
            }

            RefAsync<int> totalCount = 0;

            var result = await query.ToPageListAsync(pageInfo.PageIndex, pageInfo.PageSize, totalCount);

            pageInfo.TotalCount = totalCount;

            return result;
        }

        #endregion

        #region 多表查询操作

        /// <summary>
        /// 多表查询
        /// 根据自定义的表达式，返回匿名对象集合数据
        /// </summary>
        /// <typeparam name="T1">实体1</typeparam>
        /// <typeparam name="T2">实体2</typeparam>
        /// <typeparam name="TResult">返回匿名对象</typeparam>
        /// <param name="joinExpression">关联表达式 (t1,t2) => new JoinQueryInfos(JoinType.Inner, t1.UserNo==t2.UserNo)</param>
        /// <param name="selectExpression">自定义表达式条件，返回匿名对象 (t1, t2) => new { Id =t1.UserNo, Id1 = t2.UserNo}</param>
        /// <param name="whereExpression">条件表达式 (t1, t2) =>t1.UserNo == "")</param>
        /// <param name="orderMuch">排序条件</param>
        /// <returns></returns>
        public async Task<List<TResult>> QueryMuchAnonymityAsync<T1, T2, TResult>(Expression<Func<T1, T2, JoinQueryInfos>> joinExpression, Expression<Func<T1, T2, TResult>> selectExpression, Expression<Func<T1, T2, bool>> whereExpression = null, List<OrderByClause> orderMuch = null) where T1 : class, new()
        {
            return await db.Queryable(joinExpression).WhereIF(whereExpression != null, whereExpression).OrderByIF(orderMuch != null, ParseOrderBy(orderMuch)).Select(selectExpression).ToListAsync();
        }

        /// <summary>
        /// 多表查询
        /// 根据自定义的表达式，返回匿名对象集合数据
        /// </summary>
        /// <typeparam name="T1">实体1</typeparam>
        /// <typeparam name="T2">实体2</typeparam>
        /// <typeparam name="T3">实体3</typeparam>
        /// <typeparam name="TResult">返回匿名对象</typeparam>
        /// <param name="joinExpression"></param>
        /// <param name="selectExpression"></param>
        /// <param name="whereExpression"></param>
        /// <param name="orderMuch"></param>
        /// <returns></returns>
        public async Task<List<TResult>> QueryMuchAnonymityAsync<T1, T2, T3, TResult>(Expression<Func<T1, T2, T3, JoinQueryInfos>> joinExpression, Expression<Func<T1, T2, T3, TResult>> selectExpression, Expression<Func<T1, T2, T3, bool>> whereExpression = null, List<OrderByClause> orderMuch = null) where T1 : class, new()
        {
            return await db.Queryable(joinExpression).WhereIF(whereExpression != null, whereExpression).OrderByIF(orderMuch != null, ParseOrderBy(orderMuch)).Select(selectExpression).ToListAsync();
        }

        /// <summary>
        /// 多表查询
        /// 根据自定义的表达式，返回匿名对象集合数据
        /// </summary>
        /// <typeparam name="T1">实体1</typeparam>
        /// <typeparam name="T2">实体2</typeparam>
        /// <typeparam name="T3">实体3</typeparam>
        /// <typeparam name="T4">实体4</typeparam>
        /// <typeparam name="TResult">返回匿名对象</typeparam>
        /// <param name="joinExpression"></param>
        /// <param name="selectExpression"></param>
        /// <param name="whereExpression"></param>
        /// <param name="orderMuch"></param>
        /// <returns></returns>
        public async Task<List<TResult>> QueryMuchAnonymityAsync<T1, T2, T3, T4, TResult>(Expression<Func<T1, T2, T3, T4, JoinQueryInfos>> joinExpression, Expression<Func<T1, T2, T3, T4, TResult>> selectExpression, Expression<Func<T1, T2, T3, T4, bool>> whereExpression = null, List<OrderByClause> orderMuch = null) where T1 : class, new()
        {
            return await db.Queryable(joinExpression).WhereIF(whereExpression != null, whereExpression).OrderByIF(orderMuch != null, ParseOrderBy(orderMuch)).Select(selectExpression).ToListAsync();
        }

        /// <summary>
        /// 多表查询
        /// 根据自定义的表达式，返回匿名对象集合数据
        /// </summary>
        /// <typeparam name="T1">实体1</typeparam>
        /// <typeparam name="T2">实体2</typeparam>
        /// <typeparam name="T3">实体3</typeparam>
        /// <typeparam name="T4">实体4</typeparam>
        /// <typeparam name="T5">实体5</typeparam>
        /// <typeparam name="TResult">返回匿名对象</typeparam>
        /// <param name="joinExpression"></param>
        /// <param name="selectExpression"></param>
        /// <param name="whereExpression"></param>
        /// <param name="orderMuch"></param>
        /// <returns></returns>
        public async Task<List<TResult>> QueryMuchAnonymityAsync<T1, T2, T3, T4, T5, TResult>(Expression<Func<T1, T2, T3, T4, T5, JoinQueryInfos>> joinExpression, Expression<Func<T1, T2, T3, T4, T5, TResult>> selectExpression, Expression<Func<T1, T2, T3, T4, T5, bool>> whereExpression = null, List<OrderByClause> orderMuch = null) where T1 : class, new()
        {
            return await db.Queryable(joinExpression).WhereIF(whereExpression != null, whereExpression).OrderByIF(orderMuch != null, ParseOrderBy(orderMuch)).Select(selectExpression).ToListAsync();
        }

        /// <summary>
        /// 多表查询
        /// 根据自定义的表达式，返回匿名对象集合数据
        /// </summary>
        /// <typeparam name="T1">实体1</typeparam>
        /// <typeparam name="T2">实体2</typeparam>
        /// <typeparam name="T3">实体3</typeparam>
        /// <typeparam name="T4">实体4</typeparam>
        /// <typeparam name="T5">实体5</typeparam>
        /// <typeparam name="T6">实体6</typeparam>
        /// <typeparam name="TResult">返回匿名对象</typeparam>
        /// <param name="joinExpression"></param>
        /// <param name="selectExpression"></param>
        /// <param name="whereExpression"></param>
        /// <param name="orderMuch"></param>
        /// <returns></returns>
        public async Task<List<TResult>> QueryMuchAnonymityAsync<T1, T2, T3, T4, T5, T6, TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, JoinQueryInfos>> joinExpression, Expression<Func<T1, T2, T3, T4, T5, T6, TResult>> selectExpression, Expression<Func<T1, T2, T3, T4, T5, T6, bool>> whereExpression = null, List<OrderByClause> orderMuch = null) where T1 : class, new()
        {
            return await db.Queryable(joinExpression).WhereIF(whereExpression != null, whereExpression).OrderByIF(orderMuch != null, ParseOrderBy(orderMuch)).Select(selectExpression).ToListAsync();
        }

        /// <summary>
        /// 多表查询
        /// 根据自定义的表达式，返回匿名对象集合数据
        /// </summary>
        /// <typeparam name="T1">实体1</typeparam>
        /// <typeparam name="T2">实体2</typeparam>
        /// <typeparam name="T3">实体3</typeparam>
        /// <typeparam name="T4">实体4</typeparam>
        /// <typeparam name="T5">实体5</typeparam>
        /// <typeparam name="T6">实体6</typeparam>
        /// <typeparam name="T7">实体7</typeparam>
        /// <typeparam name="TResult">返回匿名对象</typeparam>
        /// <param name="joinExpression"></param>
        /// <param name="selectExpression"></param>
        /// <param name="whereExpression"></param>
        /// <param name="orderMuch"></param>
        /// <returns></returns>
        public async Task<List<TResult>> QueryMuchAnonymityAsync<T1, T2, T3, T4, T5, T6, T7, TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, JoinQueryInfos>> joinExpression, Expression<Func<T1, T2, T3, T4, T5, T6, T7, TResult>> selectExpression, Expression<Func<T1, T2, T3, T4, T5, T6, T7, bool>> whereExpression = null, List<OrderByClause> orderMuch = null) where T1 : class, new()
        {
            return await db.Queryable(joinExpression).WhereIF(whereExpression != null, whereExpression).OrderByIF(orderMuch != null, ParseOrderBy(orderMuch)).Select(selectExpression).ToListAsync();
        }

        /// <summary>
        /// 多表查询
        /// 根据自定义的表达式，返回匿名对象集合数据
        /// </summary>
        /// <typeparam name="T1">实体1</typeparam>
        /// <typeparam name="T2">实体2</typeparam>
        /// <typeparam name="T3">实体3</typeparam>
        /// <typeparam name="T4">实体4</typeparam>
        /// <typeparam name="T5">实体5</typeparam>
        /// <typeparam name="T6">实体6</typeparam>
        /// <typeparam name="T7">实体7</typeparam>
        /// <typeparam name="T8">实体8</typeparam>
        /// <typeparam name="TResult">返回匿名对象</typeparam>
        /// <param name="joinExpression"></param>
        /// <param name="selectExpression"></param>
        /// <param name="whereExpression"></param>
        /// <param name="orderMuch"></param>
        /// <returns></returns>
        public async Task<List<TResult>> QueryMuchAnonymityAsync<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, JoinQueryInfos>> joinExpression, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult>> selectExpression, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, bool>> whereExpression = null, List<OrderByClause> orderMuch = null) where T1 : class, new()
        {
            return await db.Queryable(joinExpression).WhereIF(whereExpression != null, whereExpression).OrderByIF(orderMuch != null, ParseOrderBy(orderMuch)).Select(selectExpression).ToListAsync();
        }

        /// <summary>
        /// 多表查询
        /// 根据自定义的实体对象，返回实体对象集合数据
        /// </summary>
        /// <typeparam name="T1">实体1</typeparam>
        /// <typeparam name="T2">实体2</typeparam>
        /// <typeparam name="TResult">返回实体对象</typeparam>
        /// <param name="joinExpression">关联表达式 (t1,t2) => new JoinQueryInfos(JoinType.Inner, t1.UserNo==t2.UserNo)</param>
        /// <param name="selectExpression">自定义指定实体对象（支持自动填充），返回实体对象集合数据</param>
        /// <param name="whereExpression">条件表达式 (t1, t2) =>t1.UserNo == "")</param>
        /// <param name="orderMuch">排序条件</param>
        /// <returns></returns>
        public async Task<List<TResult>> QueryMuchEntityAsync<T1, T2, TResult>(Expression<Func<T1, T2, JoinQueryInfos>> joinExpression, TResult selectExpression, Expression<Func<T1, T2, bool>> whereExpression = null, List<OrderByClause> orderMuch = null) where T1 : class, new()
        {
            return await db.Queryable(joinExpression).WhereIF(whereExpression != null, whereExpression).OrderByIF(orderMuch != null, ParseOrderBy(orderMuch)).Select<TResult>().ToListAsync();
        }

        /// <summary>
        /// 多表查询
        /// 根据自定义的实体对象，返回实体对象集合数据
        /// </summary>
        /// <typeparam name="T1">实体1</typeparam>
        /// <typeparam name="T2">实体2</typeparam>
        /// <typeparam name="T3">实体3</typeparam>
        /// <typeparam name="TResult">返回实体对象</typeparam>
        /// <param name="joinExpression"></param>
        /// <param name="selectExpression"></param>
        /// <param name="whereExpression"></param>
        /// <param name="orderMuch"></param>
        /// <returns></returns>
        public async Task<List<TResult>> QueryMuchEntityAsync<T1, T2, T3, TResult>(Expression<Func<T1, T2, T3, JoinQueryInfos>> joinExpression, TResult selectExpression, Expression<Func<T1, T2, T3, bool>> whereExpression = null, List<OrderByClause> orderMuch = null) where T1 : class, new()
        {
            return await db.Queryable(joinExpression).WhereIF(whereExpression != null, whereExpression).OrderByIF(orderMuch != null, ParseOrderBy(orderMuch)).Select<TResult>().ToListAsync();
        }

        /// <summary>
        /// 多表查询
        /// 根据自定义的实体对象，返回实体对象集合数据
        /// </summary>
        /// <typeparam name="T1">实体1</typeparam>
        /// <typeparam name="T2">实体2</typeparam>
        /// <typeparam name="T3">实体3</typeparam>
        /// <typeparam name="T4">实体4</typeparam>
        /// <typeparam name="TResult">返回实体对象</typeparam>
        /// <param name="joinExpression"></param>
        /// <param name="selectExpression"></param>
        /// <param name="whereExpression"></param>
        /// <param name="orderMuch"></param>
        /// <returns></returns>
        public async Task<List<TResult>> QueryMuchEntityAsync<T1, T2, T3, T4, TResult>(Expression<Func<T1, T2, T3, T4, JoinQueryInfos>> joinExpression, TResult selectExpression, Expression<Func<T1, T2, T3, T4, bool>> whereExpression = null, List<OrderByClause> orderMuch = null) where T1 : class, new()
        {
            return await db.Queryable(joinExpression).WhereIF(whereExpression != null, whereExpression).OrderByIF(orderMuch != null, ParseOrderBy(orderMuch)).Select<TResult>().ToListAsync();
        }

        /// <summary>
        /// 多表查询
        /// 根据自定义的实体对象，返回实体对象集合数据
        /// </summary>
        /// <typeparam name="T1">实体1</typeparam>
        /// <typeparam name="T2">实体2</typeparam>
        /// <typeparam name="T3">实体3</typeparam>
        /// <typeparam name="T4">实体4</typeparam>
        /// <typeparam name="T5">实体5</typeparam>
        /// <typeparam name="TResult">返回实体对象</typeparam>
        /// <param name="joinExpression"></param>
        /// <param name="selectExpression"></param>
        /// <param name="whereExpression"></param>
        /// <param name="orderMuch"></param>
        /// <returns></returns>
        public async Task<List<TResult>> QueryMuchEntityAsync<T1, T2, T3, T4, T5, TResult>(Expression<Func<T1, T2, T3, T4, T5, JoinQueryInfos>> joinExpression, TResult selectExpression, Expression<Func<T1, T2, T3, T4, T5, bool>> whereExpression = null, List<OrderByClause> orderMuch = null) where T1 : class, new()
        {
            return await db.Queryable(joinExpression).WhereIF(whereExpression != null, whereExpression).OrderByIF(orderMuch != null, ParseOrderBy(orderMuch)).Select<TResult>().ToListAsync();
        }

        /// <summary>
        /// 多表查询
        /// 根据自定义的实体对象，返回实体对象集合数据
        /// </summary>
        /// <typeparam name="T1">实体1</typeparam>
        /// <typeparam name="T2">实体2</typeparam>
        /// <typeparam name="T3">实体3</typeparam>
        /// <typeparam name="T4">实体4</typeparam>
        /// <typeparam name="T5">实体5</typeparam>
        /// <typeparam name="T6">实体6</typeparam>
        /// <typeparam name="TResult">返回实体对象</typeparam>
        /// <param name="joinExpression"></param>
        /// <param name="selectExpression"></param>
        /// <param name="whereExpression"></param>
        /// <param name="orderMuch"></param>
        /// <returns></returns>
        public async Task<List<TResult>> QueryMuchEntityAsync<T1, T2, T3, T4, T5, T6, TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, JoinQueryInfos>> joinExpression, TResult selectExpression, Expression<Func<T1, T2, T3, T4, T5, T6, bool>> whereExpression = null, List<OrderByClause> orderMuch = null) where T1 : class, new()
        {
            return await db.Queryable(joinExpression).WhereIF(whereExpression != null, whereExpression).OrderByIF(orderMuch != null, ParseOrderBy(orderMuch)).Select<TResult>().ToListAsync();
        }

        /// <summary>
        /// 多表查询
        /// 根据自定义的实体对象，返回实体对象集合数据
        /// </summary>
        /// <typeparam name="T1">实体1</typeparam>
        /// <typeparam name="T2">实体2</typeparam>
        /// <typeparam name="T3">实体3</typeparam>
        /// <typeparam name="T4">实体4</typeparam>
        /// <typeparam name="T5">实体5</typeparam>
        /// <typeparam name="T6">实体6</typeparam>
        /// <typeparam name="T7">实体7</typeparam>
        /// <typeparam name="TResult">返回实体对象</typeparam>
        /// <param name="joinExpression"></param>
        /// <param name="selectExpression"></param>
        /// <param name="whereExpression"></param>
        /// <param name="orderMuch"></param>
        /// <returns></returns>
        public async Task<List<TResult>> QueryMuchEntityAsync<T1, T2, T3, T4, T5, T6, T7, TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, JoinQueryInfos>> joinExpression, TResult selectExpression, Expression<Func<T1, T2, T3, T4, T5, T6, T7, bool>> whereExpression = null, List<OrderByClause> orderMuch = null) where T1 : class, new()
        {
            return await db.Queryable(joinExpression).WhereIF(whereExpression != null, whereExpression).OrderByIF(orderMuch != null, ParseOrderBy(orderMuch)).Select<TResult>().ToListAsync();
        }

        /// <summary>
        /// 多表查询
        /// 根据自定义的实体对象，返回实体对象集合数据
        /// </summary>
        /// <typeparam name="T1">实体1</typeparam>
        /// <typeparam name="T2">实体2</typeparam>
        /// <typeparam name="T3">实体3</typeparam>
        /// <typeparam name="T4">实体4</typeparam>
        /// <typeparam name="T5">实体5</typeparam>
        /// <typeparam name="T6">实体6</typeparam>
        /// <typeparam name="T7">实体7</typeparam>
        /// <typeparam name="T8">实体8</typeparam>
        /// <typeparam name="TResult">返回实体对象</typeparam>
        /// <param name="joinExpression"></param>
        /// <param name="selectExpression"></param>
        /// <param name="whereExpression"></param>
        /// <param name="orderMuch"></param>
        /// <returns></returns>
        public async Task<List<TResult>> QueryMuchEntityAsync<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, JoinQueryInfos>> joinExpression, TResult selectExpression, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, bool>> whereExpression = null, List<OrderByClause> orderMuch = null) where T1 : class, new()
        {
            return await db.Queryable(joinExpression).WhereIF(whereExpression != null, whereExpression).OrderByIF(orderMuch != null, ParseOrderBy(orderMuch)).Select<TResult>().ToListAsync();
        }

        #endregion

        #region 多表分页查询操作

        /// <summary>
        /// 多表分页查询
        /// 根据自定义的实体对象，返回实体对象集合分页数据
        /// </summary>
        /// <typeparam name="T1">实体1</typeparam>
        /// <typeparam name="T2">实体2</typeparam>
        /// <typeparam name="TResult">返回实体对象集合分页数据</typeparam>
        /// <param name="joinExpression">关联表达式 (t1,t2) => new JoinQueryInfos(JoinType.Inner, t1.UserNo==t2.UserNo)</param>
        /// <param name="selectExpression">自定义指定实体对象（支持自动填充），返回实体对象集合数据</param>
        /// <param name="pageInfo">分页信息</param>
        /// <param name="whereExpression">条件表达式 (t1, t2) =>t1.UserNo == "")</param>
        /// <param name="orderMuch">排序条件</param>
        /// <returns></returns>
        public async Task<List<TResult>> QueryPageMuchEntityAsync<T1, T2, TResult>(Expression<Func<T1, T2, JoinQueryInfos>> joinExpression, TResult selectExpression, SqlSugarPageInfo pageInfo, Expression<Func<T1, T2, bool>> whereExpression = null, List<OrderByClause> orderMuch = null) where T1 : class, new()
        {
            var query = db.Queryable(joinExpression).WhereIF(whereExpression != null, whereExpression).OrderByIF(orderMuch != null, ParseOrderBy(orderMuch)).Select<TResult>();

            RefAsync<int> totalCount = 0;

            var result = await query.ToPageListAsync(pageInfo.PageIndex, pageInfo.PageSize, totalCount);

            pageInfo.TotalCount = totalCount;

            return result;
        }

        /// <summary>
        /// 多表分页查询
        /// 根据自定义的实体对象，返回实体对象集合分页数据
        /// </summary>
        /// <typeparam name="T1">实体1</typeparam>
        /// <typeparam name="T2">实体2</typeparam>
        /// <typeparam name="T3">实体3</typeparam>
        /// <typeparam name="TResult">返回实体对象集合分页数据</typeparam>
        /// <param name="joinExpression"></param>
        /// <param name="selectExpression"></param>
        /// <param name="pageInfo"></param>
        /// <param name="whereExpression"></param>
        /// <param name="orderMuch"></param>
        /// <returns></returns>
        public async Task<List<TResult>> QueryPageMuchEntityAsync<T1, T2, T3, TResult>(Expression<Func<T1, T2, T3, JoinQueryInfos>> joinExpression, TResult selectExpression, SqlSugarPageInfo pageInfo, Expression<Func<T1, T2, T3, bool>> whereExpression = null, List<OrderByClause> orderMuch = null) where T1 : class, new()
        {
            var query = db.Queryable(joinExpression).WhereIF(whereExpression != null, whereExpression).OrderByIF(orderMuch != null, ParseOrderBy(orderMuch)).Select<TResult>();

            RefAsync<int> totalCount = 0;

            var result = await query.ToPageListAsync(pageInfo.PageIndex, pageInfo.PageSize, totalCount);

            pageInfo.TotalCount = totalCount;

            return result;
        }

        /// <summary>
        /// 多表分页查询
        /// 根据自定义的实体对象，返回实体对象集合分页数据
        /// </summary>
        /// <typeparam name="T1">实体1</typeparam>
        /// <typeparam name="T2">实体2</typeparam>
        /// <typeparam name="T3">实体3</typeparam>
        /// <typeparam name="T4">实体4</typeparam>
        /// <typeparam name="TResult">返回实体对象集合分页数据</typeparam>
        /// <param name="joinExpression"></param>
        /// <param name="selectExpression"></param>
        /// <param name="pageInfo"></param>
        /// <param name="whereExpression"></param>
        /// <param name="orderMuch"></param>
        /// <returns></returns>
        public async Task<List<TResult>> QueryPageMuchEntityAsync<T1, T2, T3, T4, TResult>(Expression<Func<T1, T2, T3, T4, JoinQueryInfos>> joinExpression, TResult selectExpression, SqlSugarPageInfo pageInfo, Expression<Func<T1, T2, T3, T4, bool>> whereExpression = null, List<OrderByClause> orderMuch = null) where T1 : class, new()
        {
            var query = db.Queryable(joinExpression).WhereIF(whereExpression != null, whereExpression).OrderByIF(orderMuch != null, ParseOrderBy(orderMuch)).Select<TResult>();

            RefAsync<int> totalCount = 0;

            var result = await query.ToPageListAsync(pageInfo.PageIndex, pageInfo.PageSize, totalCount);

            pageInfo.TotalCount = totalCount;

            return result;
        }

        /// <summary>
        /// 多表分页查询
        /// 根据自定义的实体对象，返回实体对象集合分页数据
        /// </summary>
        /// <typeparam name="T1">实体1</typeparam>
        /// <typeparam name="T2">实体2</typeparam>
        /// <typeparam name="T3">实体3</typeparam>
        /// <typeparam name="T4">实体4</typeparam>
        /// <typeparam name="T5">实体5</typeparam>
        /// <typeparam name="TResult">返回实体对象集合分页数据</typeparam>
        /// <param name="joinExpression"></param>
        /// <param name="selectExpression"></param>
        /// <param name="pageInfo"></param>
        /// <param name="whereExpression"></param>
        /// <param name="orderMuch"></param>
        /// <returns></returns>
        public async Task<List<TResult>> QueryPageMuchEntityAsync<T1, T2, T3, T4, T5, TResult>(Expression<Func<T1, T2, T3, T4, T5, JoinQueryInfos>> joinExpression, TResult selectExpression, SqlSugarPageInfo pageInfo, Expression<Func<T1, T2, T3, T4, T5, bool>> whereExpression = null, List<OrderByClause> orderMuch = null) where T1 : class, new()
        {
            var query = db.Queryable(joinExpression).WhereIF(whereExpression != null, whereExpression).OrderByIF(orderMuch != null, ParseOrderBy(orderMuch)).Select<TResult>();

            RefAsync<int> totalCount = 0;

            var result = await query.ToPageListAsync(pageInfo.PageIndex, pageInfo.PageSize, totalCount);

            pageInfo.TotalCount = totalCount;

            return result;
        }

        /// <summary>
        /// 多表分页查询
        /// 根据自定义的实体对象，返回实体对象集合分页数据
        /// </summary>
        /// <typeparam name="T1">实体1</typeparam>
        /// <typeparam name="T2">实体2</typeparam>
        /// <typeparam name="T3">实体3</typeparam>
        /// <typeparam name="T4">实体4</typeparam>
        /// <typeparam name="T5">实体5</typeparam>
        /// <typeparam name="T6">实体6</typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="joinExpression"></param>
        /// <param name="selectExpression"></param>
        /// <param name="pageInfo"></param>
        /// <param name="whereExpression"></param>
        /// <param name="orderMuch"></param>
        /// <returns></returns>
        public async Task<List<TResult>> QueryPageMuchEntityAsync<T1, T2, T3, T4, T5, T6, TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, JoinQueryInfos>> joinExpression, TResult selectExpression, SqlSugarPageInfo pageInfo, Expression<Func<T1, T2, T3, T4, T5, T6, bool>> whereExpression = null, List<OrderByClause> orderMuch = null) where T1 : class, new()
        {
            var query = db.Queryable(joinExpression).WhereIF(whereExpression != null, whereExpression).OrderByIF(orderMuch != null, ParseOrderBy(orderMuch)).Select<TResult>();

            RefAsync<int> totalCount = 0;

            var result = await query.ToPageListAsync(pageInfo.PageIndex, pageInfo.PageSize, totalCount);

            pageInfo.TotalCount = totalCount;

            return result;
        }

        /// <summary>
        /// 多表分页查询
        /// 根据自定义的实体对象，返回实体对象集合分页数据
        /// </summary>
        /// <typeparam name="T1">实体1</typeparam>
        /// <typeparam name="T2">实体2</typeparam>
        /// <typeparam name="T3">实体3</typeparam>
        /// <typeparam name="T4">实体4</typeparam>
        /// <typeparam name="T5">实体5</typeparam>
        /// <typeparam name="T6">实体6</typeparam>
        /// <typeparam name="T7">实体7</typeparam>
        /// <typeparam name="TResult">返回实体对象集合分页数据</typeparam>
        /// <param name="joinExpression"></param>
        /// <param name="selectExpression"></param>
        /// <param name="pageInfo"></param>
        /// <param name="whereExpression"></param>
        /// <param name="orderMuch"></param>
        /// <returns></returns>
        public async Task<List<TResult>> QueryPageMuchEntityAsync<T1, T2, T3, T4, T5, T6, T7, TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, JoinQueryInfos>> joinExpression, TResult selectExpression, SqlSugarPageInfo pageInfo, Expression<Func<T1, T2, T3, T4, T5, T6, T7, bool>> whereExpression = null, List<OrderByClause> orderMuch = null) where T1 : class, new()
        {
            var query = db.Queryable(joinExpression).WhereIF(whereExpression != null, whereExpression).OrderByIF(orderMuch != null, ParseOrderBy(orderMuch)).Select<TResult>();

            RefAsync<int> totalCount = 0;

            var result = await query.ToPageListAsync(pageInfo.PageIndex, pageInfo.PageSize, totalCount);

            pageInfo.TotalCount = totalCount;

            return result;
        }

        /// <summary>
        /// 多表分页查询
        /// 根据自定义的实体对象，返回实体对象集合分页数据
        /// </summary>
        /// <typeparam name="T1">实体1</typeparam>
        /// <typeparam name="T2">实体2</typeparam>
        /// <typeparam name="T3">实体3</typeparam>
        /// <typeparam name="T4">实体4</typeparam>
        /// <typeparam name="T5">实体5</typeparam>
        /// <typeparam name="T6">实体6</typeparam>
        /// <typeparam name="T7">实体7</typeparam>
        /// <typeparam name="T8">实体8</typeparam>
        /// <typeparam name="TResult">返回实体对象集合分页数据</typeparam>
        /// <param name="joinExpression"></param>
        /// <param name="selectExpression"></param>
        /// <param name="pageInfo"></param>
        /// <param name="whereExpression"></param>
        /// <param name="orderMuch"></param>
        /// <returns></returns>
        public async Task<List<TResult>> QueryPageMuchEntityAsync<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, JoinQueryInfos>> joinExpression, TResult selectExpression, SqlSugarPageInfo pageInfo, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, bool>> whereExpression = null, List<OrderByClause> orderMuch = null) where T1 : class, new()
        {
            var query = db.Queryable(joinExpression).WhereIF(whereExpression != null, whereExpression).OrderByIF(orderMuch != null, ParseOrderBy(orderMuch)).Select<TResult>();

            RefAsync<int> totalCount = 0;

            var result = await query.ToPageListAsync(pageInfo.PageIndex, pageInfo.PageSize, totalCount);

            pageInfo.TotalCount = totalCount;

            return result;
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 排序转换
        /// </summary>
        /// <param name="orderBys">排序</param>
        /// <returns>值</returns>
        private string ParseOrderBy(List<OrderByClause> orderBys)
        {
            var conds = "";
            foreach (var con in orderBys)
            {
                switch (con.Order)
                {
                    case SqlSugarEnums.OrderSequence.Asc:
                        conds += $"{con.Sort} asc,";
                        break;
                    case SqlSugarEnums.OrderSequence.Desc:
                        conds += $"{con.Sort} desc,";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return conds.TrimEnd(',');
        }

        #endregion
    }
}
