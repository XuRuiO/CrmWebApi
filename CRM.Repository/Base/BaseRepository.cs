using CRM.IRepository.IBase;
using System;
using System.Collections.Generic;
using System.Text;
using SqlSugar;
using CRM.Repository.SqlSugarOrm;
using System.Threading.Tasks;
using System.Linq.Expressions;
using CRM.Core.Models;

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
        /// 构造函数
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
                        ? await Task.Run(() => db.Insertable(model).ExecuteCommand())
                        : await Task.Run(() => db.Insertable(model).InsertColumns(allowColumns).ExecuteCommand());
                case SqlSugarEnums.SqlSugarAddReturnAction.ReturnIdentity:
                    return allowColumns == null
                        ? await Task.Run(() => db.Insertable(model).ExecuteReturnIdentity())
                        : await Task.Run(() => db.Insertable(model).InsertColumns(allowColumns).ExecuteReturnIdentity());
                case SqlSugarEnums.SqlSugarAddReturnAction.ReturnBigIdentity:
                    return allowColumns == null
                        ? await Task.Run(() => db.Insertable(model).ExecuteReturnBigIdentity())
                        : await Task.Run(() => db.Insertable(model).InsertColumns(allowColumns).ExecuteReturnBigIdentity());
                case SqlSugarEnums.SqlSugarAddReturnAction.ReturnEntity:
                    return allowColumns == null
                        ? await Task.Run(() => db.Insertable(model).ExecuteReturnEntity())
                        : await Task.Run(() => db.Insertable(model).InsertColumns(allowColumns).ExecuteReturnEntity());
                case SqlSugarEnums.SqlSugarAddReturnAction.IdentityIntoEntity:
                    return allowColumns == null
                        ? await Task.Run(() => db.Insertable(model).ExecuteCommandIdentityIntoEntity())
                        : await Task.Run(() => db.Insertable(model).InsertColumns(allowColumns).ExecuteCommandIdentityIntoEntity());
                default:
                    return allowColumns == null
                        ? await Task.Run(() => db.Insertable(model).ExecuteCommand())
                        : await Task.Run(() => db.Insertable(model).InsertColumns(allowColumns).ExecuteCommand());
            }
        }

        #endregion
    }
}
