using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CRM.Freamwork.Aop
{
    /// <summary>
    /// 2019.05.30      Rui     日志记录Aop拦截器，继承接口IInterceptor
    /// </summary>
    public class LogAop : IInterceptor
    {
        /// <summary>
        /// 实例化接口IINterceptor的唯一方法Intercept
        /// </summary>
        /// <param name="invocation"></param>
        public void Intercept(IInvocation invocation)
        {            //记录被拦截方法信息的日志信息
            var dataIntercept = $@"{DateTime.Now.ToString("yyyyMMddHHmmss")}/r/n
                                    当前执行方法：{invocation.Method.Name}/r/n
                                    参数是：{string.Join(",", invocation.Arguments.Select(x => (x ?? "").ToString()).ToArray())}";

            try
            {
                //在被拦截的方法执行完毕之后，继续执行当前方法
                invocation.Proceed();
            }
            catch (Exception ex)
            {
                //执行的service 中，出现异常
                dataIntercept += ($"方法执行中，出现异常：{ex.Message + ex.InnerException}");
            }

            dataIntercept += ($"被拦截的方法执行完毕，返回结果：{invocation.ReturnValue}");

            #region 输出到当前项目日志

            var path = Directory.GetCurrentDirectory() + @"\Log";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string fileName = path + $@"\InterceptLog-{DateTime.Now.ToString("yyyyMMddHHmmss")}.log";

            StreamWriter sw = File.AppendText(fileName);
            sw.WriteLine(dataIntercept);
            sw.Close();

            #endregion
        }
    }
}
