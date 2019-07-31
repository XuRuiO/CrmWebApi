using CRM.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

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

        #region 单表查询操作

        /// <summary>
        /// 查询数量
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        Task<long> QueryCountAsync(Expression<Func<T, bool>> where);

        /// <summary>
        /// 查询单条
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        Task<T> QueryFirstAsync(Expression<Func<T, bool>> where);

        /// <summary>
        /// 查询单条，可以根据条件排序
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="orders">排序条件</param>
        /// <returns></returns>
        Task<T> QueryFirstAsync(Expression<Func<T, bool>> where, List<SqlSugarOrder<T>> orders);

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        Task<List<T>> QueryAllAsync();

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        Task<List<T>> QueryAllAsync(Expression<Func<T, bool>> where);

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="orders">排序条件</param>
        /// <returns></returns>
        Task<List<T>> QueryAllAsync(Expression<Func<T, bool>> where, List<SqlSugarOrder<T>> orders);

        /// <summary>
        /// 条件分页查询，单表
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="pageInfo">分页信息</param>
        /// <param name="orders">排序条件</param>
        /// <returns></returns>
        Task<List<T>> QueryConditionPageAsync(Expression<Func<T, bool>> where, SqlSugarPageInfo pageInfo, List<SqlSugarOrder<T>> orders = null);

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
        Task<List<TResult>> QueryMuchAnonymityAsync<T1, T2, TResult>(Expression<Func<T1, T2, JoinQueryInfos>> joinExpression, Expression<Func<T1, T2, TResult>> selectExpression, Expression<Func<T1, T2, bool>> whereExpression = null, List<OrderByClause> orderMuch = null) where T1 : class, new();

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
        Task<List<TResult>> QueryMuchAnonymityAsync<T1, T2, T3, TResult>(Expression<Func<T1, T2, T3, JoinQueryInfos>> joinExpression, Expression<Func<T1, T2, T3, TResult>> selectExpression, Expression<Func<T1, T2, T3, bool>> whereExpression = null, List<OrderByClause> orderMuch = null) where T1 : class, new();

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
        Task<List<TResult>> QueryMuchAnonymityAsync<T1, T2, T3, T4, TResult>(Expression<Func<T1, T2, T3, T4, JoinQueryInfos>> joinExpression, Expression<Func<T1, T2, T3, T4, TResult>> selectExpression, Expression<Func<T1, T2, T3, T4, bool>> whereExpression = null, List<OrderByClause> orderMuch = null) where T1 : class, new();

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
        Task<List<TResult>> QueryMuchAnonymityAsync<T1, T2, T3, T4, T5, TResult>(Expression<Func<T1, T2, T3, T4, T5, JoinQueryInfos>> joinExpression, Expression<Func<T1, T2, T3, T4, T5, TResult>> selectExpression, Expression<Func<T1, T2, T3, T4, T5, bool>> whereExpression = null, List<OrderByClause> orderMuch = null) where T1 : class, new();

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
        Task<List<TResult>> QueryMuchAnonymityAsync<T1, T2, T3, T4, T5, T6, TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, JoinQueryInfos>> joinExpression, Expression<Func<T1, T2, T3, T4, T5, T6, TResult>> selectExpression, Expression<Func<T1, T2, T3, T4, T5, T6, bool>> whereExpression = null, List<OrderByClause> orderMuch = null) where T1 : class, new();

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
        Task<List<TResult>> QueryMuchAnonymityAsync<T1, T2, T3, T4, T5, T6, T7, TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, JoinQueryInfos>> joinExpression, Expression<Func<T1, T2, T3, T4, T5, T6, T7, TResult>> selectExpression, Expression<Func<T1, T2, T3, T4, T5, T6, T7, bool>> whereExpression = null, List<OrderByClause> orderMuch = null) where T1 : class, new();

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
        Task<List<TResult>> QueryMuchAnonymityAsync<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, JoinQueryInfos>> joinExpression, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult>> selectExpression, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, bool>> whereExpression = null, List<OrderByClause> orderMuch = null) where T1 : class, new();

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
        Task<List<TResult>> QueryMuchEntityAsync<T1, T2, TResult>(Expression<Func<T1, T2, JoinQueryInfos>> joinExpression, TResult selectExpression, Expression<Func<T1, T2, bool>> whereExpression = null, List<OrderByClause> orderMuch = null) where T1 : class, new();

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
        Task<List<TResult>> QueryMuchEntityAsync<T1, T2, T3, TResult>(Expression<Func<T1, T2, T3, JoinQueryInfos>> joinExpression, TResult selectExpression, Expression<Func<T1, T2, T3, bool>> whereExpression = null, List<OrderByClause> orderMuch = null) where T1 : class, new();

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
        Task<List<TResult>> QueryMuchEntityAsync<T1, T2, T3, T4, TResult>(Expression<Func<T1, T2, T3, T4, JoinQueryInfos>> joinExpression, TResult selectExpression, Expression<Func<T1, T2, T3, T4, bool>> whereExpression = null, List<OrderByClause> orderMuch = null) where T1 : class, new();

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
        Task<List<TResult>> QueryMuchEntityAsync<T1, T2, T3, T4, T5, TResult>(Expression<Func<T1, T2, T3, T4, T5, JoinQueryInfos>> joinExpression, TResult selectExpression, Expression<Func<T1, T2, T3, T4, T5, bool>> whereExpression = null, List<OrderByClause> orderMuch = null) where T1 : class, new();

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
        Task<List<TResult>> QueryMuchEntityAsync<T1, T2, T3, T4, T5, T6, TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, JoinQueryInfos>> joinExpression, TResult selectExpression, Expression<Func<T1, T2, T3, T4, T5, T6, bool>> whereExpression = null, List<OrderByClause> orderMuch = null) where T1 : class, new();

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
        Task<List<TResult>> QueryMuchEntityAsync<T1, T2, T3, T4, T5, T6, T7, TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, JoinQueryInfos>> joinExpression, TResult selectExpression, Expression<Func<T1, T2, T3, T4, T5, T6, T7, bool>> whereExpression = null, List<OrderByClause> orderMuch = null) where T1 : class, new();

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
        Task<List<TResult>> QueryMuchEntityAsync<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, JoinQueryInfos>> joinExpression, TResult selectExpression, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, bool>> whereExpression = null, List<OrderByClause> orderMuch = null) where T1 : class, new();

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
        Task<List<TResult>> QueryPageMuchEntityAsync<T1, T2, TResult>(Expression<Func<T1, T2, JoinQueryInfos>> joinExpression, TResult selectExpression, SqlSugarPageInfo pageInfo, Expression<Func<T1, T2, bool>> whereExpression = null, List<OrderByClause> orderMuch = null) where T1 : class, new();

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
        Task<List<TResult>> QueryPageMuchEntityAsync<T1, T2, T3, TResult>(Expression<Func<T1, T2, T3, JoinQueryInfos>> joinExpression, TResult selectExpression, SqlSugarPageInfo pageInfo, Expression<Func<T1, T2, T3, bool>> whereExpression = null, List<OrderByClause> orderMuch = null) where T1 : class, new();

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
        Task<List<TResult>> QueryPageMuchEntityAsync<T1, T2, T3, T4, TResult>(Expression<Func<T1, T2, T3, T4, JoinQueryInfos>> joinExpression, TResult selectExpression, SqlSugarPageInfo pageInfo, Expression<Func<T1, T2, T3, T4, bool>> whereExpression = null, List<OrderByClause> orderMuch = null) where T1 : class, new();

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
        Task<List<TResult>> QueryPageMuchEntityAsync<T1, T2, T3, T4, T5, TResult>(Expression<Func<T1, T2, T3, T4, T5, JoinQueryInfos>> joinExpression, TResult selectExpression, SqlSugarPageInfo pageInfo, Expression<Func<T1, T2, T3, T4, T5, bool>> whereExpression = null, List<OrderByClause> orderMuch = null) where T1 : class, new();

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
        /// <typeparam name="TResult">返回实体对象集合分页数据</typeparam>
        /// <param name="joinExpression"></param>
        /// <param name="selectExpression"></param>
        /// <param name="pageInfo"></param>
        /// <param name="whereExpression"></param>
        /// <param name="orderMuch"></param>
        /// <returns></returns>
        Task<List<TResult>> QueryPageMuchEntityAsync<T1, T2, T3, T4, T5, T6, TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, JoinQueryInfos>> joinExpression, TResult selectExpression, SqlSugarPageInfo pageInfo, Expression<Func<T1, T2, T3, T4, T5, T6, bool>> whereExpression = null, List<OrderByClause> orderMuch = null) where T1 : class, new();

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
        Task<List<TResult>> QueryPageMuchEntityAsync<T1, T2, T3, T4, T5, T6, T7, TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, JoinQueryInfos>> joinExpression, TResult selectExpression, SqlSugarPageInfo pageInfo, Expression<Func<T1, T2, T3, T4, T5, T6, T7, bool>> whereExpression = null, List<OrderByClause> orderMuch = null) where T1 : class, new();

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
        Task<List<TResult>> QueryPageMuchEntityAsync<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, JoinQueryInfos>> joinExpression, TResult selectExpression, SqlSugarPageInfo pageInfo, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, bool>> whereExpression = null, List<OrderByClause> orderMuch = null) where T1 : class, new();

        #endregion
    }
}
