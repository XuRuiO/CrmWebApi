using CRM.Core.CustomExtensions;
using CRM.IRepository.IUnitOfWork;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ISqlSugarClient sqlSugarClient;

        /// <summary>
        /// 通过构造函数，注入 Sugar Client 实例
        /// </summary>
        /// <param name="sqlSugarClient"></param>
        public UnitOfWork(ISqlSugarClient sqlSugarClient)
        {
            this.sqlSugarClient = sqlSugarClient;
        }

        /// <summary>
        /// 保证每次 scope 访问，多个仓储类，都用一个 client 实例，注意这不是单例模式
        /// </summary>
        /// <returns></returns>
        public ISqlSugarClient GetDbClient()
        {
            return sqlSugarClient;
        }

        /// <summary>
        /// 开始事务
        /// </summary>
        public void BeginTran()
        {
            GetDbClient().Ado.BeginTran();
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        public void CommitTran()
        {
            try
            {
                GetDbClient().Ado.CommitTran();
            }
            catch (Exception ex)
            {
                GetDbClient().Ado.RollbackTran();
                throw new CustomerException($"事务处理异常：{ex.ToString()}", Core.Models.ApiResponseStatusCode.Error, true);
            }
        }

        public void RollbackTran()
        {
            GetDbClient().Ado.RollbackTran();
        }
    }
}
