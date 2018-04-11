using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using ZGD.Common;

namespace ZGD.Web.Controllers
{
    public class PageController : BaseController
    {
        string xmlPath = "~/XmlConfig/About.xml";
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(string type = "About")
        {
            string content = ZGD.Common.XMLDom.ReadXml(xmlPath, "SiteCon/" + type);
            ViewBag.PageData = content;
            ViewBag.PageKey = type;
            return View();
        }
    }
}
