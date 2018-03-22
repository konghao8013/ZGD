using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace ZGD.Web.Controllers
{
    public class NewsController : BaseController
    {
        /// <summary>
        /// 新闻首页
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult Index(int page = 1, string key = "", string t = "")
        {
            string pager = string.Empty, where = "1=1 and ZxType=1", url = string.Empty;
            if (!string.IsNullOrWhiteSpace(key))
            {
                url += "&key=" + key;
                key = HttpUtility.UrlDecode(key);
                where += " and (Title like '%" + key + "%' or Keyword like '%" + key + "%' or Tags like '%" + key + "%')";
            }

            int type = 0;
            if (!string.IsNullOrWhiteSpace(t) && t != "0")
            {
                where += " and ClassId=" + t;
                url += "&t=" + t;
                type = Convert.ToInt32(t);
            }
            var result = new ZGD.BLL.NewsInfo().GetList(page, 10, where, " IsTop desc,PubTime desc", url, "/news", out pager);
            DataSet ds = new ZGD.BLL.Channel().GetList(0, " ParentId=1 and IsDelete=0", " Id asc");
            ViewBag.ClassType = ds != null && ds.Tables[0].Rows.Count > 0 ? ds.Tables[0] : null;
            var cModel = new ZGD.BLL.Channel().GetModelById(type);
            ViewBag.Pager = pager;
            ViewBag.Page = page;
            ViewBag.SearchKey = key;
            ViewBag.Type = cModel != null ? cModel.Title : key;
            ViewBag.TypeID = t;
            //广告
            DataSet ds_ad = new ZGD.BLL.Banner().GetList(0, " aType=2 and IsLock=0", " Sort asc");
            ViewBag.AdList = ds_ad != null ? ds_ad.Tables[0] : null;
            return View(result);
        }

        /// <summary>
        /// 新闻详细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            ZGD.BLL.NewsInfo bll = new BLL.NewsInfo();
            ZGD.BLL.Project pBll = new BLL.Project();
            ZGD.Model.NewsInfo news = bll.GetModel(id);
            //点击率
            bll.UpdateField(id, " Click=Click+1");
            Model.Channel cType = new BLL.Channel().GetModelById(news.ClassId);
            news.ClassName = cType == null ? "家装攻略" : cType.Title;

            //
            int maxId = bll.GetMaxId(" ZxType=1 ");
            int minId = bll.GetMinId(" ZxType=1 ");
            //上一篇
            Model.NewsInfo prev = new Model.NewsInfo();
            if (minId == id)
            {
                ViewBag.PrevNews = null;
            }
            else
            {
                GetPrevNext(news.Id, false, maxId, minId, ref prev);
                ViewBag.PrevNews = prev;
            }
            //下一篇
            Model.NewsInfo next = new Model.NewsInfo();
            if (maxId == id)
            {
                ViewBag.NextNews = null;
            }
            else
            {
                GetPrevNext(news.Id, true, maxId, minId, ref next);
                ViewBag.NextNews = next;
            }
            //相关
            string aboutKey = string.IsNullOrEmpty(news.Keyword) ? "" : news.Keyword.TrimStart(',').TrimEnd(',').Split(',')[0];
            DataTable dt_about = string.IsNullOrWhiteSpace(aboutKey) ? null : bll.GetList(10, " IsLock=0 and (Tags like '%" + aboutKey + "%') ", " PubTime desc");
            ViewBag.AboutNews = dt_about;

            //图集
            BLL.Project pro = new BLL.Project();
            DataSet ds_case = pro.GetList(6, " IsLock=0 ", " PubTime desc");
            ViewBag.HotCase = ds_case != null ? ds_case.Tables[0] : null;
            //广告
            DataSet ds_ad = new ZGD.BLL.Banner().GetList(0, " aType=2 and IsLock=0", " Sort asc");
            ViewBag.AdList = ds_ad != null ? ds_ad.Tables[0] : null;

            return View(news);
        }

        /// <summary>
        /// 递归上一篇 下一篇
        /// </summary>
        /// <returns></returns>
        public void GetPrevNext(int id, bool isNext, int maxId, int minId, ref Model.NewsInfo model)
        {
            if (isNext)
            {
                if (maxId > id)
                {
                    id = id + 1;
                }
            }
            else
            {
                if (minId < id)
                {
                    id = id - 1;
                }
            }
            model = new BLL.NewsInfo().GetModel(id);
            if (model == null || model.ZxType != 1)
            {
                GetPrevNext(id, isNext, maxId, minId, ref model);
            }
        }
    }
}
