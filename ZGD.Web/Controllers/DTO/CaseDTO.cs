using ZGD.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZGD.Web.Controllers
{
    public class CaseDTO
    {
        /// <summary>
        /// 
        /// </summary>
        public Project CaseModel { set; get; }
        public Project PrevCase { set; get; }
        public Project NextCase { set; get; }
        public List<string> NewTags { set; get; }
        /// <summary>
        /// 设计师
        /// </summary>
        public Designer Designer
        {
            set;
            get;
        }
        /// <summary>
        /// 设=
        /// </summary>
        public DataTable ProjectImgs
        {
            set;
            get;
        }
    }
}
