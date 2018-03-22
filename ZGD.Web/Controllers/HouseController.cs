using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace ZGD.Web.Controllers
{
    public class HouseController : BaseController
    {
        /// <summary>
        /// 小区首页
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult Index(int page = 1, string key = "")
        {
            string pager = string.Empty, where = "1=1", url = string.Empty;
            if (!string.IsNullOrWhiteSpace(key))
            {
                key = HttpUtility.UrlDecode(key);
                where += " and (Title like '%" + key + "%' or Keyword like '%" + key + "%')";
            }
            var result = new ZGD.BLL.House().GetList(page, 12, where, "PubTime desc", url, "/house", out pager);
            //热门tags
            DataSet ds_hot = new ZGD.BLL.House().GetList(4, " IsLock=0 ", " Click desc");
            ViewBag.HotHouse = ds_hot != null && ds_hot.Tables[0].Rows.Count > 0 ? ds_hot.Tables[0] : null;
            ViewBag.Pager = pager;
            ViewBag.Page = page;
            ViewBag.SearchKey = key;
            return View(result);
        }

        /// <summary>
        /// 小区详细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int id, int page = 1)
        {
            ZGD.BLL.House bll = new BLL.House();
            ZGD.BLL.Project pBll = new BLL.Project();
            ZGD.Model.House model = bll.GetModel(id);
            //点击率
            bll.UpdateField(id, " Click=Click+1");

            //案例
            string pager = string.Empty, url = string.Empty;
            DataSet ds = pBll.GetList(4, " p.IsLock=0 and p.HouseId=" + model.ID, " p.PubTime desc");
            ViewBag.AboutCase = ds != null && ds.Tables[0].Rows.Count > 0 ? ds.Tables[0] : null;

            DataSet ds_h = bll.GetList(4, " IsLock=0 ", " IsTop desc,Click desc");
            ViewBag.AboutHouse = ds_h != null && ds_h.Tables[0].Rows.Count > 0 ? ds_h.Tables[0] : null;

            return View(model);
        }
    }
}
