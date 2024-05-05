using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.IRepository.IUnitOfWork
{
    /// <summary>
    /// 重新设计SqlSugarClient，创建工作单元接口
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// 获取 sqlsugar client 实例
        /// </summary>
        /// <returns></returns>
        ISqlSugarClient GetDbClient();

        /// <summary>
        /// 开始事务
        /// </summary>
        void BeginTran();

        /// <summary>
        /// 提交事务
        /// </summary>
        void CommitTran();

        /// <summary>
        /// 回滚事务
        /// </summary>
        void RollbackTran();
    }
}
