using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace ZGD.Web.Controllers
{
    public class XZXController : BaseController
    {
        /// <summary>
        /// 新闻首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ZGD.BLL.NewsInfo news = new BLL.NewsInfo();
            ZGD.BLL.Question qa = new BLL.Question();
            //热门标签
            DataTable dt_tag = new ZGD.BLL.Links().GetList(0, " IsLock=0 and ClassId=70 ", " SortId asc");
            ViewBag.NewsTagList = news.GetTagList(12);
            ViewBag.JRTJ = news.GetList(5, " IsLock=0 and ZxType=1", " Id desc");
            ViewBag.RMWZ = news.GetList(5, " IsLock=0 and ZxType=1 and IsTop=1", " Id desc");
            ViewBag.JZRD = news.GetList(5, " IsLock=0 and ZxType=1 and IsTop=1 and ClassId=2", " Id desc");
            ViewBag.RMQA = qa.GetList(8, " q.IsLock=0 and q.IsGood=1", " q.Id desc");
            ViewBag.JZGL = news.GetList(6, " IsLock=0 and ZxType=1 and ClassId=2", " Id desc");
            ViewBag.ZXCL = news.GetList(9, " IsLock=0 and ZxType=1 and ClassId=3", " Id desc");
            ViewBag.JZSJ = news.GetList(6, " IsLock=0 and ZxType=1 and ClassId=5", " Id desc");
            ViewBag.JJFS = news.GetList(9, " IsLock=0 and ZxType=1 and ClassId=6", " Id desc");
            ViewBag.GHDT = news.GetList(4, " IsLock=0 and ZxType=1 and ClassId=71", " Id desc");
            ViewBag.JZQA = qa.GetList(6, " q.IsLock=0 ", " q.Id desc");
            ViewBag.Tags = dt_tag;
            return View();
        }
    }
}
