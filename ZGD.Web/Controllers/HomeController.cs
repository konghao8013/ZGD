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
            DataTable dt_news1 = nBll.GetList(10, " IsLock=0 and ClassId=2 ", " IsTop desc,PubTime desc");
            //装修材料
            DataTable dt_news2 = nBll.GetList(10, " IsLock=0 and ClassId=3 ", " IsTop desc,PubTime desc");
            //家装设计
            DataTable dt_news3 = nBll.GetList(10, " IsLock=0 and ClassId=5 ", " IsTop desc,PubTime desc");
            //家居风水
            DataTable dt_news4 = nBll.GetList(10, " IsLock=0 and ClassId=6 ", " IsTop desc,PubTime desc");
            //港宏动态
            DataTable dt_news5 = nBll.GetList(6, " IsLock=0 and ClassId=71 ", " IsTop desc,PubTime desc");
            
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
