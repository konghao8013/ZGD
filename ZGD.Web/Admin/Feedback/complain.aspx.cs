﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZGD.Web.Admin.Feedback
{
    public partial class complain : ZGD.BasePage.ManagePage
    {
        public int pcount = 0; //总条数

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //数据绑定
                RptBind();
            }
        }

        #region 数据绑定
        public void RptBind()
        {
            string strWhere = "";
            if (Session["strWhereProduct_Fed"] != null && Session["strWhereProduct_Fed"].ToString() != "")
            {
                strWhere += Session["strWhereProduct_Fed"].ToString();
            }

            ZGD.BLL.Complain bll = new ZGD.BLL.Complain();
            DataSet ds = bll.GetList(strWhere);
            DataView dv = ds.Tables[0].DefaultView;
            //利用PAGEDDAGASOURCE类来分页
            PagedDataSource pds = new PagedDataSource();
            AspNetPager1.RecordCount = dv.Count;
            pds.DataSource = dv;
            pds.AllowPaging = true;
            pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;
            pds.PageSize = AspNetPager1.PageSize;
            //获得总条数
            pcount = bll.GetRecordCount(strWhere);
            if (this.pcount > 0)
            {
                this.lbtnDel.Enabled = true;
            }
            else
            {
                this.lbtnDel.Enabled = false;
            }
            //绑定数据
            rptList.DataSource = pds;
            rptList.DataBind();
        }
        #endregion

        protected void AspNetPager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            AspNetPager1.CurrentPageIndex = e.NewPageIndex;
            RptBind();
        }

        #region 批量删除
        protected void lbtnDel_Click(object sender, EventArgs e)
        {
            ZGD.BLL.Complain bll = new ZGD.BLL.Complain();
            //批量删除
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((Label)rptList.Items[i].FindControl("lb_id")).Text);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("cb_id");
                if (cb.Checked)
                {
                    //保存日志
                    SaveLogs("[投诉管理]删除投诉：" + bll.GetModel(id).uName);
                    //删除记录
                    bll.Delete(id);
                }
            }
            JscriptMsg("批量删除成功");
            RptBind();
        }
        #endregion

        #region 属性索引
        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string SupplierName = Convert.ToString(this.ddlType.SelectedValue);
            string strsql = "";
            if (string.IsNullOrWhiteSpace(SupplierName) && SupplierName != "0")
            {
                Session["strWhereProduct_Complain"] = "";
            }
            else
            {
                strsql += (" and Type=" + SupplierName);
                if (strsql != "")
                {
                    Session["strWhereProduct_Complain"] = " (1=1) " + strsql;
                }
                else
                {
                    Session["strWhereProduct_Complain"] = "";
                }
            }
            //重新绑定数据
            RptBind();
        }
        #endregion

        #region 查询
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            string SupplierName = this.txtKeywords.Text.Trim();
            string strsql = "";
            if (SupplierName != "")
            {
                strsql += "and (uName like '%" + SupplierName + "%' or Phone like '%" + SupplierName + "%' or Area like '%" + SupplierName + "%')";
            }
            if (strsql != "")
            {
                Session["strWhereProduct_Complain"] = " (1=1) " + strsql;
            }
            else
            {
                Session["strWhereProduct_Complain"] = "";
            }

            //重新绑定数据
            RptBind();
        }
        #endregion

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {

        }
    }
}