using ZGD.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZGD.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ZGD.BLL.NewsInfo nBll = new ZGD.BLL.NewsInfo();
            //家装攻略
            DataTable dt_news1 = nBll.GetList(10, " IsLock=0 and ZxType=1 and ClassId=2 ", " IsTop desc,PubTime desc");
            //装修材料
            DataTable dt_news2 = nBll.GetList(10, " IsLock=0 and ZxType=1 and ClassId=3 ", " IsTop desc,PubTime desc");
            //家装设计
            DataTable dt_news3 = nBll.GetList(10, " IsLock=0 and ZxType=1 and ClassId=5 ", " IsTop desc,PubTime desc");
            //家居风水
            DataTable dt_news4 = nBll.GetList(10, " IsLock=0 and ZxType=1 and ClassId=6 ", " IsTop desc,PubTime desc");
            //港宏动态
            DataTable dt_news5 = nBll.GetList(6, " IsLock=0 and ZxType=1 and ClassId=71 ", " IsTop desc,PubTime desc");
            
            //首页轮播图
            DataSet ds_slider = new ZGD.BLL.Banner().GetList(0, " aType=1 and IsLock=0", " Sort asc");
            DataTable adList = ds_slider != null ? ds_slider.Tables[0] : null;

            //首页广告
            DataSet ds_ad = new ZGD.BLL.Banner().GetList(0, " aType=2 and IsLock=0", " Sort asc");
            DataTable adIndex = ds_ad != null ? ds_ad.Tables[0] : null;
            
            //风格 户型
            BLL.Channel cBll = new BLL.Channel();
            ViewBag.DataFG = cBll.GetList(" kindId=3 and IsDelete=0 and ParentId>0").Tables[0];
            ViewBag.DataHX = cBll.GetList(" kindId=4 and IsDelete=0 and ParentId>0").Tables[0];
            ViewBag.DataJG = cBll.GetList(" kindId=5 and IsDelete=0 and ParentId>0").Tables[0];
            ViewBag.DataMJ = cBll.GetList(" kindId=8 and IsDelete=0 and ParentId>0").Tables[0];
            ViewBag.DataQY = cBll.GetList(" kindId=9 and IsDelete=0 and ParentId>0").Tables[0];
            ViewBag.DataGN = cBll.GetList(" kindId=10 and IsDelete=0 and ParentId>0").Tables[0];
            ViewBag.DataJD = cBll.GetList(" kindId=11 and IsDelete=0 and ParentId>0").Tables[0];

            //热门标签
            DataTable dt_tag = new ZGD.BLL.Links().GetList(0, " IsLock=0 and ClassId=70 ", " SortId asc");
            ViewBag.Tags = dt_tag;
            
            //报价数量
            int bj_count = new ZGD.BLL.Feedback().GetCount("");
            ViewBag.BjCount = bj_count;

            return View(new IndexDTO
            {
                AdList = adList,
                AdList2 = adIndex,
                News1 = dt_news1,
                News2 = dt_news2,
                News3 = dt_news3,
                News4 = dt_news4,
                News5 = dt_news5
            });
        }

        public ActionResult Page404()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResult Cacl(Model.Feedback model)
        {
            if (model != null)
            {
                BLL.Feedback fBll = new BLL.Feedback();
                if (fBll.GetCount(" UserTel='" + model.UserTel + "' and ClassId=" + model.ClassId + " and ClassId2=" + model.ClassId2 + " and House='" + model.House + "' and Area='" + model.Area + "' and fType=1") > 0)
                {
                    return Json(new AjaxMsgResult() { Success = false, Msg = "请勿重复提交！" });
                }
                string todayStart = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00";
                string todayEnd = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59";
                if (fBll.GetCount(" UserTel='" + model.UserTel + "' and AddTime between '" + todayStart + "' and '" + todayEnd + "'") > 20)
                {
                    return Json(new AjaxMsgResult() { Success = false, Msg = "您的操作太频繁！" });
                }
                if (string.IsNullOrWhiteSpace(model.UserName))
                {
                    return Json(new AjaxMsgResult() { Success = false, Msg = "名字不能为空！" });
                }
                if (string.IsNullOrWhiteSpace(model.UserTel))
                {
                    return Json(new AjaxMsgResult() { Success = false, Msg = "联系方式不能为空！" });
                }
                if (!RegexHandler.IsMobile(model.UserTel))
                {
                    return Json(new AjaxMsgResult() { Success = false, Msg = "请输入正确的手机号！" });
                }
                //if (string.IsNullOrWhiteSpace(model.House))
                //{
                //    return Json(new AjaxMsgResult() { Success = false, Msg = "请输入小区名称！" });
                //}
                if (model.Area <= 0)
                {
                    return Json(new AjaxMsgResult() { Success = false, Msg = "请输入房屋面积！" });
                }
                if (string.IsNullOrWhiteSpace(model.vCode))
                {
                    return Json(new AjaxMsgResult() { Success = false, Msg = "验证码不能为空！" });
                }
                if (model.vCode.ToLower() != Session[DTKeys.SESSION_CODE].ToString().ToLower())
                {
                    return Json(new AjaxMsgResult() { Success = false, Msg = "验证码错误！" });
                }
                model.Remark = (model.hType == 1 ? "新房" : "旧房") + "（" + model.Area + "）平，装修套餐：" + (model.caclType == 1 ? "现代系列" : "世家系列");
                //判断计算单价
                Model.Channel chModel = new BLL.Channel().GetModelById(model.ClassId2);
                model.Price = model.caclType == 2 ? chModel.CaclNum2 : chModel.CaclNum;
                //价格计算
                model.Money = model.hType == 1 ? (model.Price * model.Area * 1.12m).ToString() : (model.Price * model.Area * 1.12m * 1.2m).ToString();
                int join_id = fBll.Add(model);
                if (join_id > 0)
                {
                    string m = Convert.ToDecimal(model.Money).ToString("F2");
                    SMSHelper.SendSms(model.UserTel, model.Remark + "，总报价：" + m + "，详情请咨询客服！");
                    return Json(new AjaxMsgResult() { Success = true, Msg = "提交成功！", Source = m });
                }
            }
            return Json(new AjaxMsgResult() { Success = false, Msg = "提交失败！" });
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResult fJoin(Model.Feedback model)
        {
            if (model != null)
            {
                BLL.Feedback fbBll = new BLL.Feedback();
                if (string.IsNullOrWhiteSpace(model.UserName))
                {
                    return Json(new AjaxMsgResult() { Success = false, Msg = "名字不能为空！" });
                }
                if (string.IsNullOrWhiteSpace(model.UserTel))
                {
                    return Json(new AjaxMsgResult() { Success = false, Msg = "联系方式不能为空！" });
                }
                if (!RegexHandler.IsMobile(model.UserTel))
                {
                    return Json(new AjaxMsgResult() { Success = false, Msg = "请输入正确的手机号！" });
                }
                if (string.IsNullOrWhiteSpace(model.vCode))
                {
                    return Json(new AjaxMsgResult() { Success = false, Msg = "验证码不能为空！" });
                }
                if (model.vCode.ToLower() != Session[DTKeys.SESSION_CODE].ToString().ToLower())
                {
                    return Json(new AjaxMsgResult() { Success = false, Msg = "验证码错误！" });
                }
                if (fbBll.GetCount(" UserTel=" + model.UserTel + " and fType=" + model.fType) > 0)
                {
                    return Json(new AjaxMsgResult() { Success = false, Msg = "您已经报名，请勿重复提交！" });
                }
                int join_id = fbBll.Add(model);
                if (join_id > 0)
                {
                    return Json(new AjaxMsgResult() { Success = true, Msg = "提交成功！" });
                }
            }
            return Json(new AjaxMsgResult() { Success = false, Msg = "提交失败！" });
        }

        /// <summary>
        /// 短信发送
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public JsonResult SendSms(string phone, string code)
        {
            if (string.IsNullOrWhiteSpace(phone))
            {
                return Json(new AjaxMsgResult { Success = false, Msg = "手机号不能为空！" });
            }
            if (string.IsNullOrWhiteSpace(code))
            {
                return Json(new AjaxMsgResult { Success = false, Msg = "图片验证码不能为空！" });
            }
            if (!SystemHandler.CheckImgCode(code))
            {
                return Json(new AjaxMsgResult { Success = false, Msg = "图片验证码错误！" });
            }

            System.Random a = new Random();
            int RandKey = a.Next(100000, 999999);
            string content = "您的验证码为：" + RandKey;
            Session[DTKeys.SESSION_SMS_CODE] = RandKey.ToString();
            AjaxMsgResult result = SMSHelper.SendSms(phone, content);
            return Json(result);
        }
    }
}
