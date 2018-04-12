using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace ZGD.Web.Controllers
{
    public class ZTController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult Index(int page = 1, string t = "")
        {
            string pager = string.Empty, where = "1=1", url = string.Empty;

            int type = 0;
            if (!string.IsNullOrWhiteSpace(t) && t != "0")
            {
                where += " and nc.ClassId=" + t;
                url += "&t=" + t;
                type = Convert.ToInt32(t);
            }
            var result = new ZGD.BLL.NewsInfo().GetList(page, 10, where, " n.IsTop desc,n.PubTime desc", url, "/news", out pager);
            DataSet ds = new ZGD.BLL.Channel().GetList(0, " ParentId=1 and IsDelete=0", " Id asc");
            ViewBag.ClassType = ds != null && ds.Tables[0].Rows.Count > 0 ? ds.Tables[0] : null;
            var cModel = new ZGD.BLL.Channel().GetModelById(type);
            ViewBag.Pager = pager;
            ViewBag.Page = page;
            //广告
            DataSet ds_ad = new ZGD.BLL.Banner().GetList(0, " aType=2 and IsLock=0", " Sort asc");
            ViewBag.AdList = ds_ad != null ? ds_ad.Tables[0] : null;
            return View(result);
        }
    }
}
