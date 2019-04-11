using System;
using System.Collections.Generic;
using System.Text;
using CRM.Core.Helpers;
using System.Linq;
using SqlSugar;

namespace CRM.Repository.SqlSugarOrm
{
    /// <summary>
    /// SqlSugarOrm 数据库上下文对象
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
                    InitKeyType = InitKeyType.SystemTable,    //默认SystemTable, 字段信息读取, 如：该属性是不是主键，是不是标识列等等信息
                    IsShardSameThread = true,     //设为true相同线程是同一个SqlConnection
                    MoreSettings = new ConnMoreSettings
                    {
                        IsAutoRemoveDataCache = true        //为true表示可以自动删除二级缓存
                    }
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
