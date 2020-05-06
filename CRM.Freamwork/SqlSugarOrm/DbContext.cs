using CRM.Core;
using CRM.Core.Helpers;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Freamwork.SqlSugarOrm
{
    /// <summary>
    /// 2020.01.01     SqlSugarOrm 数据库上下文对象
    /// </summary>
    public class DbContext
    {
        public static SqlSugarClient DB
        {
            get
            {
                if (string.IsNullOrEmpty(DbConfig.ConnectionString))
                {
                    throw new ArgumentNullException("数据库连接字符串为空！");
                }

                //创建SqlSugarClient对象
                var _DB = new SqlSugarClient(new ConnectionConfig
                {
                    ConnectionString = DbConfig.ConnectionString,       //连接字符串
                    DbType = DbType.SqlServer,      //数据库类型
                    IsAutoCloseConnection = true,     //(默认false)是否自动释放数据库，设为true我们不需要close或者Using的操作，比较推荐
                    InitKeyType = InitKeyType.Attribute,    //默认SystemTable：从数据库系统表查询；Attribute：不受数据库限制通过实体特性读取
                    IsShardSameThread = true,     //设为true相同线程是同一个SqlConnection
                    MoreSettings = new ConnMoreSettings
                    {
                        IsAutoRemoveDataCache = true,        //为true表示可以自动删除二级缓存
                        IsWithNoLockQuery = true        //true表式无锁模式，查询的时候默认会加上.With(SqlWith.NoLock)，可以用With(SqlWith.Null)让全局的失效
                    }
                });

                //暂时只支持查询单表全局过滤器
                _DB.QueryFilter.Add(new SqlFilterItem()
                {
                    FilterValue = filterDb =>
                    {
                        return new SqlFilterResult() { Sql = $"Enabled={(int)Enums.TableEnabled.启用} AND Deleted={(int)Enums.TableDeleted.未删除}" };
                    },
                    IsJoinQuery = false
                });

                //是否启用SqlSugar 打印sql日志功能
                if (ConfigsHelper.GetIsEnableSqlSugarLog())
                {
                    //SQL执行前事件
                    _DB.Aop.OnLogExecuting = (sql, pars) =>
                    {
                        string sqlstr = $"{sql}+'\r\n'+{_DB.Utilities.SerializeObject(pars.ToDictionary(x => x.ParameterName, x => x.Value))}";
                    };
                }

                return _DB;
            }
        }
    }
}
