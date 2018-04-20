using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using ZGD.Common;

namespace ZGD.Web.Controllers
{
    public class GalleryController : BaseController
    {
        /// <summary>
        /// 图册首页
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult Index(int page = 1, int t = 0, string key = "")
        {
            string pager = string.Empty, where = "1=1", url = string.Empty;
            if (!string.IsNullOrWhiteSpace(key))
            {
                key = key.FunStr();
                url += "-" + key;
                key = HttpUtility.UrlDecode(key);
                where += " and (p.Title like '%" + key + "%' or p.Keyword like '%" + key + "%' or p.Tags like '%" + key + "%')";
            }

            if (t > 0)
            {
                where += " and p.ClassId=" + t;
                url += "-" + t;
            }
            var result = new ZGD.BLL.Project().GetList(page, 12, where, " p.IsTop desc,p.PubTime desc", url, "/gallerypage/", out pager);
            DataSet ds = new ZGD.BLL.Channel().GetList(0, " ParentId=1 and IsDelete=0", " Id asc");
            ViewBag.ClassType = ds != null && ds.Tables[0].Rows.Count > 0 ? ds.Tables[0] : null;
            var cModel = new ZGD.BLL.Channel().GetModelById(t);
            ViewBag.Pager = pager;
            ViewBag.Page = page;
            ViewBag.SearchKey = key;
            ViewBag.Type = cModel != null ? cModel.Title : key;
            ViewBag.TypeID = t;

            return View(result);
        }

        /// <summary>
        /// 图册详细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            ZGD.BLL.Project bll = new BLL.Project();
            ZGD.BLL.ProjectImg iBll = new BLL.ProjectImg();
            ZGD.Model.Project model = bll.GetModel(id);
            //点击率
            bll.UpdateField(id, " Click=Click+1");

            Model.Channel cType = new BLL.Channel().GetModelById(model.ClassId);
            model.TypeName = cType == null ? "" : cType.Title;

            ViewBag.Pics = iBll.GetList(0, " pID=" + model.Id, " piID asc");

            return View(model);
        }
    }
}
