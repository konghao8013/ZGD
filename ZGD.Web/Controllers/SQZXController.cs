using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace ZGD.Web.Controllers
{
    public class SQZXController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int page = 1)
        {
            int pagesize = 10;
            string pager = string.Empty, where = "", url = string.Empty;
            //where += " Status=1 ";
            var result = new ZGD.BLL.Activity().GetList(page, pagesize, where, "PubTime desc", url, "/sqzx", out pager);
            ViewBag.Pager = pager;
            return View(result);
        }
        /// <summary>
        /// 详细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            ZGD.BLL.Activity bll = new BLL.Activity();
            ZGD.Model.Activity model = bll.GetModel(id);
            //点击率
            bll.UpdateField(id, " Click=Click+1");
            DataSet ds = bll.GetList(8, " ID<>" + id, " ID desc");
            ViewBag.AboutAct = ds != null ? ds.Tables[0] : null;
            return View(model);
        }

        /// <summary>
        /// 活动报名
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Join(Model.ActivityJoin model)
        {
            Model.AjaxMsgResult result = new Model.AjaxMsgResult();
            result.Msg = "报名失败！";
            result.Success = false;
            if (model == null)
            {
                result.Msg = "未找到活动报名信息！";
                return Json(result);
            }
            if (!Common.RegexHandler.IsMobile(model.Phone))
            {
                result.Msg = "手机号码格式有误！";
                return Json(result);
            }
            ZGD.BLL.Activity bllAct = new BLL.Activity();
            ZGD.Model.Activity actModel = bllAct.GetModel(model.aID);
            if (actModel == null)
            {
                result.Msg = "未找到活动信息！";
                return Json(result);
            }
            if (actModel.Status == -1)
            {
                result.Msg = "该活动已结束！";
                return Json(result);
            }
            ZGD.BLL.ActivityJoin bll = new BLL.ActivityJoin();
            DataSet ds = bll.GetList(" aID=" + model.aID + " and Phone='" + model.Phone + "'");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                result.Msg = "此次活动您已经报名，请勿重复提交！";
                return Json(result);
            }
            int msg = bll.Add(model);
            if (msg > 0)
            {
                try
                {
                    //报名短信
                    string sms = "尊敬的" + model.UserName + "，恭喜您报名成功，您的报名编号为：" + msg;
                    SMSHelper.SendSms(model.Phone, sms);
                }
                catch (Exception ex)
                {
                    ZGD.Common.LogHepler.ErrorLog(ex);
                }
                //报名数
                actModel.JoinCount = actModel.JoinCount + 1;
                bllAct.UpdateField(actModel.ID, "JoinCount=" + actModel.JoinCount);
                result.Msg = "报名成功！";
                result.Success = true;
            }
            return Json(result);
        }
    }
}
