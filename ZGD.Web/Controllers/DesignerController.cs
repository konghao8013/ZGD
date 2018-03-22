using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace ZGD.Web.Controllers
{
    public class DesignerController : BaseController
    {
        public ActionResult Index(int page = 1)
        {
            string pager = string.Empty, where = "1=1", url = string.Empty;

            DataTable dt = new ZGD.BLL.Designer().GetList(page, 16, where, " Sort desc,AddDate desc", url, "/designer", out pager);
            ViewBag.Pager = pager;
            return View(dt);
        }

        /// <summary>
        /// 详细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int id, int page = 1)
        {
            ZGD.BLL.Project bll = new BLL.Project();
            ZGD.BLL.Designer dBll = new BLL.Designer();
            ZGD.Model.Designer model = dBll.GetModel(id);

            //点击率
            dBll.UpdateField(id, " Clicks=Clicks+1");

            //相关
            string pager = string.Empty, url = "&id=" + id;
            string where = " p.IsLock=0 and p.DesignerId=" + model.ID;
            DataTable dt = new ZGD.BLL.Project().GetList(page, 6, where, " p.PubTime desc", url, "/designer", out pager);
            ViewBag.AboutCase = dt;
            ViewBag.Pager = pager;
            return View(model);
        }
    }
}
