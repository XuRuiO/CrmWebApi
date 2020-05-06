using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Freamwork.SqlSugarOrm
{
    /// <summary>
    /// 2020.03.15      SqlSugar扩展，需要在Startup服务启动
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
                    ConnectionString = DbConfig.ConnectionString,//必填, 数据库连接字符串
                    DbType = DbType.SqlServer,//必填, 数据库类型
                    IsAutoCloseConnection = true,//默认false, 时候知道关闭数据库连接, 设置为true无需使用using或者Close操作
                    InitKeyType = SqlSugar.InitKeyType.Attribute//默认SystemTable, 字段信息读取, 如：该属性是不是主键，标识列等等信息
                });
            });
        }
    }
}
