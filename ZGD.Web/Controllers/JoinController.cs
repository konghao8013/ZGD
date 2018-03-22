using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace ZGD.Web.Controllers
{
    public class JoinController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Index(int type = 1)
        {
            ViewBag.JoinTypeID = type;
            //风格 户型
            BLL.Channel cBll = new BLL.Channel();
            ViewBag.DataFG = cBll.GetList(" kindId=3 and IsDelete=0 and ParentId>0").Tables[0];
            ViewBag.DataHX = cBll.GetList(" kindId=4 and IsDelete=0 and ParentId>0").Tables[0];
            ViewBag.DataQY = cBll.GetList(" kindId=9 and IsDelete=0 and ParentId>0").Tables[0];

            //设计师
            DataSet ds = new ZGD.BLL.Designer().GetList(5, " IsTop=1 ", " d.Sort desc,d.AddDate desc");
            ViewBag.DES = ds != null ? ds.Tables[0] : null;

            //在建工地
            ZGD.BLL.NewsInfo nBll = new ZGD.BLL.NewsInfo();
            DataSet ds_gongdi1 = nBll.GetGongDiList(3, " n.IsLock=0 and n.ZxType=2 and n.gdStatus<>95 ");
            DataTable dt_gongdi1 = ds_gongdi1 != null ? ds_gongdi1.Tables[0] : null;
            ViewBag.ZJSG = dt_gongdi1;

            BLL.Project pro = new BLL.Project();
            DataSet ds_gongdi2 = pro.GetList(3, " p.IsTop=1 and p.IsLock=0 ", " p.PubTime desc");
            DataTable dt_gongdi2 = ds_gongdi2 != null ? ds_gongdi2.Tables[0] : null;
            ViewBag.GDWG = dt_gongdi2;

            //记录
            ZGD.BLL.Feedback bll = new ZGD.BLL.Feedback();
            DataSet ds_log = bll.GetList(15, " fType=1 ", " Id desc");
            ViewBag.Log = ds_log != null ? ds_log.Tables[0] : null;
            ViewBag.LogCount = bll.GetCount(" fType=1 ");

            switch (type)
            {
                case 1:
                    ViewBag.JoinTypeName = "装修预算报价";
                    break;
                case 2:
                    ViewBag.JoinTypeName = "免费参观展厅";
                    break;
                case 3:
                    ViewBag.JoinTypeName = "免费看工地";
                    break;
            }
            return View();
        }
    }
}
