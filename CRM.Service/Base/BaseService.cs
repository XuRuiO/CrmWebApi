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

        #endregion
    }
}
