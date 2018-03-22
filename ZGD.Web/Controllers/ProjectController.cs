using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace ZGD.Web.Controllers
{
    public class ProjectController : BaseController
    {
        /// <summary>
        /// 工地首页
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult Index(int page = 1, string key = "", string h = "", string hx = "", string qy = "", string jd = "")
        {
            string pager = string.Empty, where = " p.ZxType=2", url = string.Empty;

            BLL.Channel cBll = new BLL.Channel();
            Model.Channel filterHX = null;
            Model.Channel filterQY = null;
            Model.Channel filterJD = null;

            if (!string.IsNullOrWhiteSpace(key))
            {
                key = HttpUtility.UrlDecode(key);
                where += " and (p.Title like '%" + key + "%' or p.Keyword like '%" + key + "%')";
            }
            if (!string.IsNullOrEmpty(h))
            {
                if (Convert.ToInt32(h) > 0)
                {
                    //filterJG = cBll.GetModelById(Convert.ToInt32(jg));
                    where += " and p.AssId=" + h;
                    url += "&h=" + h;
                }
            }
            if (!string.IsNullOrEmpty(hx))
            {
                if (Convert.ToInt32(hx) > 0)
                {
                    filterHX = cBll.GetModelById(Convert.ToInt32(hx));
                    where += " and p.hxID=" + hx;
                    url += "&hx=" + hx;
                }
            }
            if (!string.IsNullOrEmpty(qy))
            {
                if (Convert.ToInt32(qy) > 0)
                {
                    filterQY = cBll.GetModelById(Convert.ToInt32(qy));
                    where += " and p.AreaId=" + qy;
                    url += "&qy=" + qy;
                }
            }
            if (!string.IsNullOrEmpty(jd))
            {
                if (Convert.ToInt32(jd) > 0)
                {
                    filterJD = cBll.GetModelById(Convert.ToInt32(jd));
                    where += " and p.gdStatus=" + jd;
                    url += "&h=" + jd;
                }
            }

            ViewBag.FilterH = h;
            ViewBag.FilterHX = hx;
            ViewBag.FilterQY = qy;
            ViewBag.FilterJD = jd;

            //ViewBag.FilterH_Val = FilterH != null ? FilterH.Title : "";
            ViewBag.FilterHX_Val = filterHX != null ? filterHX.Title : "";
            ViewBag.FilterQY_Val = filterQY != null ? filterQY.Title : "";
            ViewBag.FilterJD_Val = filterJD != null ? filterJD.Title : "";

            ViewBag.DataJD = cBll.GetList(" kindId=11 and IsDelete=0 and ParentId>0").Tables[0];

            //广告
            DataSet ds_ad = new ZGD.BLL.Banner().GetList(0, " aType=2 and IsLock=0", " Sort asc");
            ViewBag.AdList = ds_ad != null ? ds_ad.Tables[0] : null;

            var result = new ZGD.BLL.NewsInfo().GetList_GD(page, 10, where, " p.Id desc", url, "/project", out pager);
            ViewBag.Pager = pager;
            ViewBag.Page = page;
            ViewBag.SearchKey = key;
            return View(result);
        }

        /// <summary>
        /// 工地详细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            ZGD.BLL.NewsInfo bll = new BLL.NewsInfo();
            ZGD.BLL.ProjectImg pBll = new ZGD.BLL.ProjectImg();
            BLL.Channel cBll = new BLL.Channel();
            ZGD.Model.NewsInfo news = bll.GetModel(id);

            //点击率
            bll.UpdateField(id, " Click=Click+1");

            //
            //int maxId = bll.GetMaxId();
            //int minId = bll.GetMinId();
            ////上一篇
            //Model.NewsInfo prev = new Model.NewsInfo();
            //GetPrevNext(news.Id, false, maxId, minId, ref prev);
            //ViewBag.PrevNews = prev;
            ////下一篇
            //Model.NewsInfo next = new Model.NewsInfo();
            //GetPrevNext(news.Id, true, maxId, minId, ref next);
            //ViewBag.NextNews = next;
            //热门工地
            DataTable dt_about = bll.GetList(24, " IsLock=0 and ZxType=2 ", " Click desc,PubTime desc");
            ViewBag.AboutNews = dt_about;
            //广告
            DataSet ds_ad = new ZGD.BLL.Banner().GetList(0, " aType=2 and IsLock=0", " Sort asc");
            ViewBag.AdList = ds_ad != null ? ds_ad.Tables[0] : null;
            ViewBag.Imgs = pBll.GetList(0, " pID=" + id + " and Type=5", " PubTime desc");
            ViewBag.Imgs_S = pBll.GetGdStatus(id).Tables[0];
            ViewBag.DataJD = cBll.GetList(" kindId=11 and IsDelete=0 and ParentId>0").Tables[0];

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
            if (model == null || model.ZxType != 2)
            {
                GetPrevNext(id, isNext, maxId, minId, ref model);
            }
        }
    }
}
