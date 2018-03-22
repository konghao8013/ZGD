using System;
using System.Collections.Generic;
using System.Text;
using ZGD.Common;

namespace ZGD.DAL
{
    /// <summary>
    /// 数据访问类:站点配置
    /// </summary>
    public partial class siteconfig
    {
        private static object lockHelper = new object();

        /// <summary>
        ///  读取站点配置文件
        /// </summary>
        public Model.SiteConfig loadConfig(string configFilePath)
        {
            return (Model.SiteConfig)SerializerHelper.Load(typeof(Model.SiteConfig), configFilePath);
        }

        /// <summary>
        /// 写入站点配置文件
        /// </summary>
        public Model.SiteConfig saveConifg(Model.SiteConfig model, string configFilePath)
        {
            lock (lockHelper)
            {
                SerializerHelper.Save(model, configFilePath);
            }
            return model;
        }

    }
}
