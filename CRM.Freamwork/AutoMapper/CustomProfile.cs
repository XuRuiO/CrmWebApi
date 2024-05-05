using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Freamwork.AutoMapper
{
    /// <summary>
    /// 自定义配置映射的源数据，目标数据
    /// </summary>
    public class CustomProfile : Profile
    {
        /// <summary>
        /// 配置构造函数，用来创建映射关系
        /// </summary>
        public CustomProfile()
        {
            // 说明：映射的前提是表中属性名、类型需要相同，不相同的话需要自己通过AutoMapper中的ForMember，具体用法请百度
            // 例如：CreateMap<源数据,目标数据>();

        }
    }
}
