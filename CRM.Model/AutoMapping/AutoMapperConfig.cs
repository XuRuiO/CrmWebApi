using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Model.AutoMapping
{
    /// <summary>
    /// 2020.03.08      Rui      静态全局 AutoMapper 配置文件
    /// </summary>
    public class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(config =>
            {
                config.AddProfile(new CustomProfile());
            });
        }
    }
}
