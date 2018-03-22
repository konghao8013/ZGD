using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

namespace ZGD.Web.Admin.Project
{
    public partial class List : ZGD.BasePage.ManagePage
    {
        public int pcount = 0; //总条数

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //数据绑定
                RptBind();
                //绑定类别
                ChannelTreeBind(11, "所有风格", 3, this.ddlFG);
                ChannelTreeBind(15, "所有户型", 4, this.ddlHX);
                ChannelTreeBind(62, "所有面积", 8, this.ddlMJ);
                ChannelTreeBind(77, "所有功能间", 10, this.ddlGN);
            }
        }

        #region 数据绑定
        public void RptBind()
        {
            string strWhere = "";
            if (Session["strWhereProduct_Pro"] != null && Session["strWhereProduct_Pro"].ToString() != "")
            {
                strWhere += Session["strWhereProduct_Pro"].ToString();
            }

            ZGD.BLL.Project bll = new ZGD.BLL.Project();
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
            pcount = bll.GetCount(strWhere);
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

        #region 设置操作
        protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int id = Convert.ToInt32(((Label)e.Item.FindControl("lb_id")).Text);
            ZGD.BLL.Project bll = new ZGD.BLL.Project();
            ZGD.Model.Project model = bll.GetModel(id);
            switch (e.CommandName.ToLower())
            {
                case "ibtnlock":
                    if (model.IsLock == 1)
                        bll.UpdateField(id, "IsLock=0");
                    else
                        bll.UpdateField(id, "IsLock=1");
                    break;
                case "ibtntop":
                    if (model.IsTop == 1)
                        bll.UpdateField(id, "IsTop=0");
                    else
                        bll.UpdateField(id, "IsTop=1");
                    break;
            }
            RptBind();
        }
        #endregion

        #region 批量删除
        protected void lbtnDel_Click(object sender, EventArgs e)
        {
            ZGD.BLL.Project bll = new ZGD.BLL.Project();
            ZGD.BLL.ProjectImg imgBll = new ZGD.BLL.ProjectImg();
            ZGD.Model.Project model;
            //批量删除
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((Label)rptList.Items[i].FindControl("lb_id")).Text);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("cb_id");
                if (cb.Checked)
                {
                    model = bll.GetModel(id);
                    //保存日志
                    SaveLogs("[案例模块]删除案例：" + model.Title);
                    //删除记录
                    bll.Delete(id);
                    imgBll.Delete(id, 1);
                }
            }
            JscriptMsg("删除成功");
            RptBind();
        }
        #endregion

        //翻页
        protected void AspNetPager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            AspNetPager1.CurrentPageIndex = e.NewPageIndex;
            RptBind();
        }

        #region 类别索引
        protected void ddlFG_SelectedIndexChanged(object sender, EventArgs e)
        {
            string SupplierName = this.ddlFG.SelectedValue;
            string strsql = "";
            if (SupplierName != "")
            {
                strsql += " and fgID='" + SupplierName + "'";
            }
            if (strsql != "")
            {
                Session["strWhereProduct_Pro"] = " (1=1) " + strsql;
            }
            else
            {
                Session["strWhereProduct_Pro"] = "";
            }
            //重新绑定数据
            RptBind();
        }
        protected void ddlHX_SelectedIndexChanged(object sender, EventArgs e)
        {
            string SupplierName = this.ddlHX.SelectedValue;
            string strsql = "";
            if (SupplierName != "")
            {
                strsql += " and hxID='" + SupplierName + "'";
            }
            if (strsql != "")
            {
                Session["strWhereProduct_Pro"] = " (1=1) " + strsql;
            }
            else
            {
                Session["strWhereProduct_Pro"] = "";
            }
            //重新绑定数据
            RptBind();
        }
        protected void ddlGN_SelectedIndexChanged(object sender, EventArgs e)
        {
            string SupplierName = this.ddlGN.SelectedValue;
            string strsql = "";
            if (SupplierName != "")
            {
                strsql += " and gnID='" + SupplierName + "'";
            }
            if (strsql != "")
            {
                Session["strWhereProduct_Pro"] = " (1=1) " + strsql;
            }
            else
            {
                Session["strWhereProduct_Pro"] = "";
            }
            //重新绑定数据
            RptBind();
        }
        protected void ddlMJ_SelectedIndexChanged(object sender, EventArgs e)
        {
            string SupplierName = this.ddlMJ.SelectedValue;
            string strsql = "";
            if (SupplierName != "")
            {
                strsql += " and mjID='" + SupplierName + "'";
            }
            if (strsql != "")
            {
                Session["strWhereProduct_Pro"] = " (1=1) " + strsql;
            }
            else
            {
                Session["strWhereProduct_Pro"] = "";
            }
            //重新绑定数据
            RptBind();
        }
        #endregion

        #region 属性索引
        protected void ddlProperty_SelectedIndexChanged(object sender, EventArgs e)
        {
            string SupplierName = Convert.ToString(this.ddlProperty.SelectedValue);
            string strsql = "";
            switch (SupplierName)
            {
                case "IsTop":
                    if (SupplierName != "")
                    {
                        strsql += (" and IsTop=1");
                    }
                    if (strsql != "")
                    {
                        Session["strWhereProduct_Pro"] = " (1=1) " + strsql;
                    }
                    else
                    {
                        Session["strWhereProduct_Pro"] = "";
                    }
                    break;
                case "IsLock":
                    if (SupplierName != "")
                    {
                        strsql += (" and IsLock=1");
                    }
                    if (strsql != "")
                    {
                        Session["strWhereProduct_Pro"] = " (1=1) " + strsql;
                    }
                    else
                    {
                        Session["strWhereProduct_Pro"] = "";
                    }
                    break;
                default:
                    if (strsql != "")
                    {
                        Session["strWhereProduct_Pro"] = " (1=1) " + strsql;
                    }
                    else
                    {
                        Session["strWhereProduct_Pro"] = "";
                    }
                    break;
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
                strsql += "and Title like '%" + SupplierName + "%'";
            }
            if (strsql != "")
            {
                Session["strWhereProduct_Pro"] = " (1=1) " + strsql;
            }
            else
            {
                Session["strWhereProduct_Pro"] = "";
            }

            //重新绑定数据
            RptBind();
        }
        #endregion

    }
}