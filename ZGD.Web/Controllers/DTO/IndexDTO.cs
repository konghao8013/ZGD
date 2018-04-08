using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZGD.Web.Controllers
{
    public class IndexDTO
    {
        /// <summary>
        /// 首页广告
        /// </summary>
        public DataTable AdList { get; set; }
        /// <summary>
        /// 首页广告
        /// </summary>
        public DataTable AdList2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DataTable News1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DataTable News2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DataTable News3 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DataTable News4 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DataTable News5 { get; set; }
    }
}
