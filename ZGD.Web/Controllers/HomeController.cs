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
            //党内要闻
            DataTable dt_news1 = nBll.GetList(8, " n.IsLock=0 and nc.ClassId=9 ", " n.IsTop desc,n.Id desc");
            DataTable dt_news1_img = nBll.GetList(4, " n.IsLock=0 and nc.ClassId=9 and ImgUrl is not null and ImgUrl<>''", " n.IsTop desc,n.Id desc");
            //工作简讯
            DataTable dt_news2 = nBll.GetList(6, " n.IsLock=0 and nc.ClassId=10 ", " n.IsTop desc,n.Id desc");
            DataTable dt_news2_img = nBll.GetList(4, " n.IsLock=0 and nc.ClassId=10 and ImgUrl is not null and ImgUrl<>''", " n.IsTop desc,n.Id desc");
            //关于致公
            DataTable dt_news3 = nBll.GetList(6, " n.IsLock=0 and nc.ClassId=19 ", " n.IsTop desc,n.Id desc");
            //自身建设
            DataTable dt_news4 = nBll.GetList(5, " n.IsLock=0 and nc.ClassId=11 ", " n.IsTop desc,n.Id desc");
            //宣传思想
            DataTable dt_news5 = nBll.GetList(5, " n.IsLock=0 and nc.ClassId=13 ", " n.IsTop desc,n.Id desc");
            //参政议政
            DataTable dt_news6 = nBll.GetList(5, " n.IsLock=0 and nc.ClassId=12 ", " n.IsTop desc,n.Id desc");
            //社会服务
            DataTable dt_news7 = nBll.GetList(5, " n.IsLock=0 and nc.ClassId=15 ", " n.IsTop desc,n.Id desc");
            //海外联谊
            DataTable dt_news8 = nBll.GetList(5, " n.IsLock=0 and nc.ClassId=14 ", " n.IsTop desc,n.Id desc");
            //致公风采
            DataTable dt_news9 = nBll.GetList(5, " n.IsLock=0 and nc.ClassId=16 ", " n.IsTop desc,n.Id desc");
            //通知公告
            DataTable dt_news10 = nBll.GetList(4, " n.IsLock=0 and nc.ClassId=20 ", " n.IsTop desc,n.Id desc");

            ZGD.BLL.Channel cBll = new ZGD.BLL.Channel();
            //专题
            DataSet ds_zt = cBll.GetList(3, " IsDelete=0 and KindId=2 and ParentId=21", " Id desc");
            DataTable dt_zt = ds_zt != null ? ds_zt.Tables[0] : null;

            //首页广告
            DataSet ds_ad = new ZGD.BLL.Banner().GetList(0, " aType=2 and IsLock=0", " Sort asc");
            DataTable adIndex = ds_ad != null ? ds_ad.Tables[0] : null;

            //图册
            DataSet ds_pic = new BLL.Project().GetList(6, " p.IsLock=0 and p.IsTop=1 ", " p.Id desc");
            DataTable dt_pic = ds_pic != null ? ds_pic.Tables[0] : null;

            return View(new IndexDTO
            {
                AdList = adIndex,
                News1 = dt_news1,
                News1_Img = dt_news1_img,
                News2 = dt_news2,
                News2_Img = dt_news2_img,
                News3 = dt_news3,
                News4 = dt_news4,
                News5 = dt_news5,
                News6 = dt_news6,
                News7 = dt_news7,
                News8 = dt_news8,
                News9 = dt_news9,
                News10 = dt_news10,
                ZT = dt_zt,
                Pics = dt_pic
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
