using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZGD.Web.Controllers
{
    public class CtrlController : BaseController
    {
        /// <summary>
        /// 站点头部
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult Top(string menuID = "0")
        {
            BLL.Channel cBll = new BLL.Channel();
            ViewBag.menuID = menuID;
            ViewBag.NavList = cBll.GetList(" IsDelete=0 and IsNav=1").Tables[0];

            return View("~/Views/Shared/_Top.cshtml");
        }

        /// <summary>
        /// 站点底部
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult Footer()
        {
            return View("~/Views/Shared/_Footer.cshtml");
        }
    }
}
