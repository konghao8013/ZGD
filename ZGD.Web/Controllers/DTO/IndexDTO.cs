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
        public Model.Banner Tl_Ad { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DataTable News1 { get; set; }
        public DataTable News1_Img { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DataTable News2 { get; set; }
        public DataTable News2_Img { get; set; }
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
        /// <summary>
        /// 
        /// </summary>
        public DataTable News6 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DataTable News7 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DataTable News8 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DataTable News9 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DataTable News10 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DataTable ZT { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DataTable Pics { get; set; }
    }
}
