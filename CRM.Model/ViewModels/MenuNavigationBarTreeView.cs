using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Model.ViewModels
{
    public class MenuNavigationBarTreeView
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 路由地址
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 重定向地址（输入父路径，跳转到指定的子路劲）
        /// </summary>        
        public string Redirect { get; set; }

        /// <summary>
        /// 当前路由元信息
        /// </summary>
        public MenuNavigationBarMeta Meta { get; set; }

        /// <summary>
        /// 当前路由的子路由信息
        /// </summary>
        public List<MenuNavigationBarTreeView> Children { get; set; }
    }

    public class MenuNavigationBarMeta
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 菜单图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 是否隐藏菜单在侧边栏出现，如login，404，edit 页面不需要加载到菜单
        /// </summary>
        public bool? IsHidden { get; set; }

        /// <summary>
        /// 是否固钉在tagsView中，不可被删除
        /// </summary>
        public bool? IsAffix { get; set; }

        /// <summary>
        /// 是否使用Layout布局，基本上父导航为True
        /// </summary>        
        public bool? IsUseLayout { get; set; }

        /// <summary>
        /// 排序序号
        /// </summary>
        public int? OrderSort { get; set; }
    }
}
