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
        public ActionResult Top(int menuID = 0, string title = "")
        {
            BLL.Channel cBll = new BLL.Channel();
            ViewBag.menuID = menuID;
            ViewBag.NavTitle = title;
            ViewBag.DataFG_i = cBll.GetList(" kindId=3 and IsDelete=0 and ParentId>0").Tables[0];
            ViewBag.DataHX_i = cBll.GetList(" kindId=4 and IsDelete=0 and ParentId>0").Tables[0];
            ViewBag.DataJG_i = cBll.GetList(" kindId=5 and IsDelete=0 and ParentId>0").Tables[0];
            ViewBag.DataMJ_i = cBll.GetList(" kindId=8 and IsDelete=0 and ParentId>0").Tables[0];
            ViewBag.SearchKey = Request.QueryString["key"];
            DataSet ds = new ZGD.BLL.Channel().GetList(0, " ParentId=1 and IsDelete=0", " Id asc");
            ViewBag.ClassType_i = ds != null && ds.Tables[0].Rows.Count > 0 ? ds.Tables[0] : null;

            //热门tags
            DataTable dt_tag = new ZGD.BLL.Links().GetList(4, " IsLock=0 and ClassId=70 ", " SortId asc");
            ViewBag.Tags = dt_tag;
            return View("~/Views/Ctrl/_Top.cshtml");
        }

        /// <summary>
        /// 站点底部
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult Footer()
        {
            return View("~/Views/Ctrl/_Footer.cshtml");
        }

        /// <summary>
        /// 通用 热门小区
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult HotHouse(int top = 4)
        {
            DataTable dt = new ZGD.BLL.NewsInfo().GetList(top, " IsLock=0", " Click desc");
            return View("~/Views/Ctrl/_HotHouse.cshtml");
        }

        /// <summary>
        /// 通用 最新活动
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult HotAct(int top = 2)
        {
            DataTable dt = new ZGD.BLL.NewsInfo().GetList(top, " IsLock=0 and IsTop=1 ", " PubTime desc");
            return View("~/Views/Ctrl/_HotAct.cshtml", dt);
        }
    }
}
