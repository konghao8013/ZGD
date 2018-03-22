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

        /// <summary>
        /// 周年庆专题 
        /// </summary>
        /// <returns></returns>
        public ActionResult year()
        {
            ViewBag.ACount = new BLL.ActivityJoin().GetCount(" aID=1");
            return View();
        }

        /// <summary>
        /// 开业庆典 
        /// </summary>
        /// <returns></returns>
        public ActionResult open()
        {
            ViewBag.ACount = new BLL.ActivityJoin().GetCount(" aID=2");
            int days = DateTimeHelper.GetTimeSpan(Convert.ToDateTime("2016-8-28"), 4);
            string[] d_str = new string[] { "0", "0" };
            if (days.ToString().Length == 1)
            {
                d_str[0] = "0";
                d_str[1] = days.ToString();
            }
            else
            {
                d_str[0] = days.ToString().Substring(0, 1);
                d_str[1] = days.ToString().Substring(1, 1);
            }
            ViewBag.Days = d_str;
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult zt()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult xd()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult ysj()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult sj()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult jnh()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult gx()
        {
            var join = new BLL.ActivityJoin();
            ViewBag.ACount = join.GetCount(" aID=4");
            DataSet ds = join.GetList(" aID=4");
            ViewBag.JoinList = ds != null && ds.Tables[0] != null ? ds.Tables[0] : null;
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult complain()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult repair()
        {
            return View();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public JsonResult complainjoin(Model.Complain model)
        {
            if (string.IsNullOrWhiteSpace(model.uName))
            {
                return Json(new AjaxMsgResult() { Success = false, Msg = "请输入您的姓名！" });
            }
            if (string.IsNullOrWhiteSpace(model.Phone))
            {
                return Json(new AjaxMsgResult() { Success = false, Msg = "请输入您的联系手机！" });
            }
            if (!RegexHandler.IsMobile(model.Phone))
            {
                return Json(new AjaxMsgResult() { Success = false, Msg = "请输入正确的手机号！" });
            }
            if (string.IsNullOrWhiteSpace(model.Area))
            {
                return Json(new AjaxMsgResult() { Success = false, Msg = "请输入您的小区名称！" });
            }
            if (string.IsNullOrWhiteSpace(model.mCon))
            {
                return Json(new AjaxMsgResult() { Success = false, Msg = "请输入投诉内容！" });
            }
            if (string.IsNullOrWhiteSpace(model.vCode))
            {
                return Json(new AjaxMsgResult() { Success = false, Msg = "验证码不能为空！" });
            }
            if (model.vCode.ToLower() != Session[DTKeys.SESSION_CODE].ToString().ToLower())
            {
                return Json(new AjaxMsgResult() { Success = false, Msg = "验证码错误！" });
            }
            BLL.Complain cBll = new BLL.Complain();
            int join_id = cBll.Add(model);
            if (join_id > 0)
            {
                return Json(new AjaxMsgResult() { Success = true, Msg = "提交成功！" });
            }
            return Json(new AjaxMsgResult() { Success = false, Msg = "提交失败！" });
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public JsonResult repairjoin(Model.Repair model)
        {
            if (string.IsNullOrWhiteSpace(model.uName))
            {
                return Json(new AjaxMsgResult() { Success = false, Msg = "请输入您的姓名！" });
            }
            if (string.IsNullOrWhiteSpace(model.Phone))
            {
                return Json(new AjaxMsgResult() { Success = false, Msg = "请输入您的联系手机！" });
            }
            if (!RegexHandler.IsMobile(model.Phone))
            {
                return Json(new AjaxMsgResult() { Success = false, Msg = "请输入正确的手机号！" });
            }
            if (string.IsNullOrWhiteSpace(model.Address))
            {
                return Json(new AjaxMsgResult() { Success = false, Msg = "请输入楼盘地址！" });
            }
            if (string.IsNullOrWhiteSpace(model.WorkDate))
            {
                return Json(new AjaxMsgResult() { Success = false, Msg = "请输入维修时间！" });
            }
            if (string.IsNullOrWhiteSpace(model.Remark))
            {
                return Json(new AjaxMsgResult() { Success = false, Msg = "请输入维修说明！" });
            }
            if (string.IsNullOrWhiteSpace(model.vCode))
            {
                return Json(new AjaxMsgResult() { Success = false, Msg = "验证码不能为空！" });
            }
            if (model.vCode.ToLower() != Session[DTKeys.SESSION_CODE].ToString().ToLower())
            {
                return Json(new AjaxMsgResult() { Success = false, Msg = "验证码错误！" });
            }
            BLL.Repair rBll = new BLL.Repair();
            int join_id = rBll.Add(model);
            if (join_id > 0)
            {
                return Json(new AjaxMsgResult() { Success = true, Msg = "提交成功！" });
            }
            return Json(new AjaxMsgResult() { Success = false, Msg = "提交失败！" });
        }
    }
}
