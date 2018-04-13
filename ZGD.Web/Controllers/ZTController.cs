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
        public ActionResult Index(int page = 1)
        {
            ZGD.BLL.Channel cBll = new ZGD.BLL.Channel();
            DataSet ds = cBll.GetList(0, " ParentId=21 and KindId=2 and IsDelete=0", " Id desc");
            DataSet ys = cBll.GetZtYear();

            ViewBag.ZtList = ds != null && ds.Tables[0].Rows.Count > 0 ? ds.Tables[0] : null;
            ViewBag.Year = ys != null && ys.Tables[0].Rows.Count > 0 ? ys.Tables[0] : null;
            return View();
        }
    }
}
