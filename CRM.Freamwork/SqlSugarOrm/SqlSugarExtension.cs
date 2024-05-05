using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Freamwork.SqlSugarOrm
{
    /// <summary>
    /// SqlSugar扩展，需要在Startup服务启动
    /// </summary>
    public static class SqlSugarExtension
    {
        public static void AddSqlsugarSetup(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            //AddScoped：每次请求，都获取一个新的实例。同一个请求获取多次会得到相同的实例
            //把SugarClient对象注入服务，这里必须采用Scope，因为有事务操作
            services.AddScoped<ISqlSugarClient>(options =>
            {
                //return DbContext.DB;
                return new SqlSugar.SqlSugarClient(new SqlSugar.ConnectionConfig()
                {
                    ConnectionString = DbConfig.ConnectionString,       //连接字符串
                    DbType = DbType.SqlServer,      //数据库类型
                    IsAutoCloseConnection = true,       //(默认false)是否自动释放数据库，设为true我们不需要close或者Using的操作，比较推荐
                    InitKeyType = SqlSugar.InitKeyType.Attribute       //默认SystemTable：从数据库系统表查询；Attribute：不受数据库限制通过实体特性读取
                });
            });
        }
    }
}
