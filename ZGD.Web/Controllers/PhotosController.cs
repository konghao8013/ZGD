using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace ZGD.Web.Controllers
{
    public class PhotosController : BaseController
    {
        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            if (id != 1 && id != 2)
            {
                return Redirect("/");
            }
            ZGD.BLL.Photos bll = new BLL.Photos();
            DataSet ds = bll.GetList(0, " Type=" + id, " IsTop desc,Sort asc,PubTime desc");
            DataTable dt = ds != null ? ds.Tables[0] : null;
            ViewBag.PhotoType = id;
            return View(dt);
        }
    }
}
