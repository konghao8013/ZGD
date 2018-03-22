using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace ZGD.Web.Controllers
{
    public class CaseController : BaseController
    {
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="page"></param>
        ///// <param name="fg"></param>
        ///// <param name="hx"></param>
        ///// <param name="mj"></param>
        ///// <param name="jg"></param>
        ///// <param name="d"></param>
        ///// <param name="h">所属小区</param>
        ///// <param name="sort"></param>
        ///// <returns></returns>
        //public ActionResult Index(int page = 1, string key = "", string fg = "", string hx = "", string mj = "",
        //    string jg = "", string gn = "", string d = "", string h = "", string sort = "0")
        //{
        //    BLL.Channel cBll = new BLL.Channel();
        //    Model.Channel filterFG = null;
        //    Model.Channel filterHX = null;
        //    Model.Channel filterJG = null;
        //    Model.Channel filterMJ = null;
        //    Model.Channel filterGN = null;
        //    Model.House filterHouse = null;
        //    Model.Designer filterDes = null;
        //    #region 筛选条件
        //    string pager = string.Empty, where = "1=1", order = "", url = string.Empty;

        //    if (!string.IsNullOrWhiteSpace(key))
        //    {
        //        key = HttpUtility.UrlDecode(key);
        //        where += " and (p.Title like '%" + key + "%' or p.Keyword like '%" + key + "%')";
        //        url += "&key=" + key;
        //    }
        //    if (!string.IsNullOrEmpty(fg))
        //    {
        //        if (Convert.ToInt32(fg) != 0)
        //        {
        //            filterFG = cBll.GetModelById(Convert.ToInt32(fg));
        //            where += " and p.fgID=" + fg;
        //            url += "&fg=" + fg;
        //        }
        //    }
        //    if (!string.IsNullOrEmpty(hx))
        //    {
        //        if (Convert.ToInt32(hx) != 0)
        //        {
        //            filterHX = cBll.GetModelById(Convert.ToInt32(hx));
        //            where += " and p.hxID=" + hx;
        //            url += "&hx=" + hx;
        //        }
        //    }
        //    if (!string.IsNullOrEmpty(mj))
        //    {
        //        if (Convert.ToInt32(mj) != 0)
        //        {
        //            filterMJ = cBll.GetModelById(Convert.ToInt32(mj));
        //            where += " and p.mjID=" + mj;
        //            url += "&mj=" + mj;
        //        }
        //    }
        //    if (!string.IsNullOrEmpty(jg))
        //    {
        //        if (Convert.ToInt32(jg) != 0)
        //        {
        //            filterJG = cBll.GetModelById(Convert.ToInt32(jg));
        //            where += " and p.jgID=" + jg;
        //            url += "&fg=" + jg;
        //        }
        //    }
        //    if (!string.IsNullOrEmpty(gn))
        //    {
        //        if (Convert.ToInt32(gn) != 0)
        //        {
        //            filterJG = cBll.GetModelById(Convert.ToInt32(gn));
        //            where += " and p.gnID=" + gn;
        //            url += "&gn=" + gn;
        //        }
        //    }
        //    if (!string.IsNullOrEmpty(d))
        //    {
        //        if (Convert.ToInt32(d) != 0)
        //        {
        //            filterDes = new BLL.Designer().GetModel(Convert.ToInt32(d));
        //            where += " and p.DesignerId=" + d;
        //            url += "&d=" + d;
        //        }
        //    }
        //    if (!string.IsNullOrEmpty(h))
        //    {
        //        if (Convert.ToInt32(h) > 0)
        //        {
        //            filterHouse = new BLL.House().GetModel(Convert.ToInt32(h));
        //            where += " and p.HouseId=" + h;
        //            url += "&h=" + h;
        //        }
        //    }
        //    if (!string.IsNullOrEmpty(sort))
        //    {
        //        if (sort == "1")
        //        {
        //            order = " p.IsTop desc,p.PubTime desc";
        //        }
        //        url += "&sort=" + sort;
        //    }
        //    #endregion

        //    //查询条件
        //    ViewBag.SearchKey = key;
        //    ViewBag.FilterFG = fg;
        //    ViewBag.FilterHX = hx;
        //    ViewBag.FilterJG = jg;
        //    ViewBag.FilterMJ = mj;
        //    ViewBag.filterGN = gn;
        //    ViewBag.FilterDes = d;
        //    ViewBag.FilterHouse = h;
        //    ViewBag.FilterSort = sort;

        //    //查询条件值
        //    ViewBag.FilterFG_Val = filterFG != null ? filterFG.Title : "";
        //    ViewBag.FilterHX_Val = filterHX != null ? filterHX.Title : "";
        //    ViewBag.FilterJG_Val = filterJG != null ? filterJG.Title : "";
        //    ViewBag.FilterMJ_Val = filterMJ != null ? filterMJ.Title : "";
        //    ViewBag.FilterGN_Val = filterGN != null ? filterGN.Title : "";
        //    ViewBag.FilterHouse_Val = filterHouse != null ? filterHouse.Title : "";
        //    ViewBag.FilterDes_Val = filterDes != null ? filterDes.Name : "";

        //    ViewBag.DataFG = cBll.GetList(" kindId=3 and IsDelete=0 and ParentId>0").Tables[0];
        //    ViewBag.DataHX = cBll.GetList(" kindId=4 and IsDelete=0 and ParentId>0").Tables[0];
        //    ViewBag.DataJG = cBll.GetList(" kindId=5 and IsDelete=0 and ParentId>0").Tables[0];
        //    ViewBag.DataMJ = cBll.GetList(" kindId=8 and IsDelete=0 and ParentId>0").Tables[0];
        //    ViewBag.DataGN = cBll.GetList(" kindId=10 and IsDelete=0 and ParentId>0").Tables[0];
        //    DataSet ds = new BLL.Designer().GetList("");
        //    ViewBag.DataDes = ds != null ? ds.Tables[0] : null;

        //    BLL.Project pro = new BLL.Project();
        //    DataTable dt = pro.GetList(page, 16, where, order, url, "/Case", out pager);
        //    //string ids = "";
        //    //if (dt != null && dt.Rows.Count > 0)
        //    //{
        //    //    foreach (DataRow dr in dt.Rows)
        //    //    {
        //    //        ids += dr["Id"].ToString() + ",";
        //    //    }
        //    //}
        //    //ids = string.IsNullOrWhiteSpace(ids) ? "0" : ids.TrimEnd(',');
        //    ////获取设计师案例集合
        //    //DataTable dt_img = new ZGD.BLL.ProjectImg().GetList(0, "pID in (" + ids + ") and Type=1", "PubTime desc");
        //    //ViewBag.CaseImg = dt_img;
        //    ViewBag.Pager = pager;
        //    return View(dt);
        //}

        /// <summary>
        /// 条件唯一
        /// </summary>
        /// <param name="page"></param>
        /// <param name="t">筛选类型 10功能间 3风格 4户型 5价格 8面积 1设计师 2楼盘</param>
        /// <param name="v">对应类型值</param>
        /// <param name="key"></param>
        /// <param name="d"></param>
        /// <param name="h">所属小区</param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public ActionResult Index(int page = 1, string t = "", string v = "", string key = "", string sort = "0")
        {
            BLL.Channel cBll = new BLL.Channel();
            Model.Channel filter = null;
            Model.House filterHouse = null;
            Model.Designer filterDes = null;
            string filter_val = string.Empty;

            #region 筛选条件
            string pager = string.Empty, where = "1=1", order = "", url = string.Empty;

            if (!string.IsNullOrWhiteSpace(key))
            {
                key = HttpUtility.UrlDecode(key);
                where += " and (p.Title like '%" + key + "%' or p.Keyword like '%" + key + "%')";
                url += "&key=" + key;
            }
            if (!string.IsNullOrEmpty(t) && !string.IsNullOrWhiteSpace(v))
            {
                url += "&t=" + t + "&v=" + v;
                int val = Convert.ToInt32(v);
                if (t == "1")
                {
                    filterDes = new BLL.Designer().GetModel(val);
                    filter_val = filterDes == null ? "" : filterDes.Name;
                    where += " and p.DesignerId=" + v;
                }
                else if (t == "2")
                {
                    filterHouse = new BLL.House().GetModel(val);
                    filter_val = filterHouse == null ? "" : filterHouse.Title;
                    where += " and p.HouseId=" + v;
                }
                else
                {
                    filter = cBll.GetModelById(val);
                    filter_val = filter == null ? "" : filter.Title;
                    //10功能间 3风格 4户型 5价格 8面积
                    if (t == "10")
                    {
                        where += " and p.gnID=" + v;
                    }
                    else if (t == "3")
                    {
                        where += " and p.fgID=" + v;
                    }
                    else if (t == "4")
                    {
                        where += " and p.hxID=" + v;
                    }
                    else if (t == "5")
                    {
                        where += " and p.jgID=" + v;
                    }
                    else if (t == "8")
                    {
                        where += " and p.mjID=" + v;
                    }
                }
            }

            if (!string.IsNullOrEmpty(sort))
            {
                if (sort == "1")
                {
                    order = " p.IsTop desc,p.PubTime desc";
                }
                url += "&sort=" + sort;
            }
            #endregion

            //查询条件
            ViewBag.SearchKey = key;
            ViewBag.Filter = v;
            ViewBag.FilterType = t;
            ViewBag.FilterSort = sort;

            //查询条件值
            ViewBag.Filter_Val = filter_val;

            ViewBag.DataFG = cBll.GetList(" kindId=3 and IsDelete=0 and ParentId>0").Tables[0];
            ViewBag.DataHX = cBll.GetList(" kindId=4 and IsDelete=0 and ParentId>0").Tables[0];
            ViewBag.DataJG = cBll.GetList(" kindId=5 and IsDelete=0 and ParentId>0").Tables[0];
            ViewBag.DataMJ = cBll.GetList(" kindId=8 and IsDelete=0 and ParentId>0").Tables[0];
            ViewBag.DataGN = cBll.GetList(" kindId=10 and IsDelete=0 and ParentId>0").Tables[0];

            //设计师
            DataSet ds = new BLL.Designer().GetList("");
            ViewBag.DataDes = ds != null ? ds.Tables[0] : null;

            BLL.Project pro = new BLL.Project();
            DataTable dt = pro.GetList(page, 16, where, order, url, "/Case", out pager);
            //string ids = "";
            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        ids += dr["Id"].ToString() + ",";
            //    }
            //}
            //ids = string.IsNullOrWhiteSpace(ids) ? "0" : ids.TrimEnd(',');
            ////获取设计师案例集合
            //DataTable dt_img = new ZGD.BLL.ProjectImg().GetList(0, "pID in (" + ids + ") and Type=1", "PubTime desc");
            //ViewBag.CaseImg = dt_img;
            ViewBag.Pager = pager;
            return View(dt);
        }

        /// <summary>
        /// 案例详细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            ZGD.BLL.Project bll = new BLL.Project();
            ZGD.BLL.ProjectImg pBll = new BLL.ProjectImg();
            ZGD.BLL.Designer dBll = new BLL.Designer();
            ZGD.Model.Project pro = bll.GetModel(id);
            //点击率
            bll.UpdateField(id, " Click=Click+1");
            //
            int maxId = bll.GetMaxId();
            int minId = bll.GetMinId();
            //上一篇
            Model.Project prev = new Model.Project();
            GetPrevNext(pro.Id, false, maxId, minId, ref prev);
            //下一篇
            Model.Project next = new Model.Project();
            GetPrevNext(pro.Id, true, maxId, minId, ref next);
            //相关
            DataSet ds_case = string.IsNullOrWhiteSpace(pro.Keyword) ? null : bll.GetList(6, " p.IsLock=0 and p.fgID=" + pro.fgID, " PubTime desc");
            ViewBag.AboutCase = ds_case != null ? ds_case.Tables[0] : null;

            return View(new CaseDTO
            {
                CaseModel = pro,
                ProjectImgs = pBll.GetList(0, " pID=" + id + " and Type=1", " PubTime desc"),
                Designer = dBll.GetModel(pro.DesignerId),
                NextCase = next,
                PrevCase = prev,
                NewTags = new ZGD.BLL.NewsInfo().GetTagsByKeyword(pro.Keyword)
            });
        }

        /// <summary>
        /// 递归上一篇 下一篇
        /// </summary>
        /// <returns></returns>
        public void GetPrevNext(int id, bool isNext, int maxId, int minId, ref Model.Project model)
        {
            if (isNext)
            {
                if (maxId > id)
                {
                    id = id + 1;
                }
            }
            else
            {
                if (minId < id)
                {
                    id = id - 1;
                }
            }
            model = new BLL.Project().GetModel(id);
            if (model == null)
            {
                GetPrevNext(id, isNext, maxId, minId, ref model);
            }
        }
    }
}
