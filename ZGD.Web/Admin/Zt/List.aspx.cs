using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

namespace ZGD.Web.Admin.Zt
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
                ChannelTreeBind(21, "请选择专题", 2, this.ddlClassId);
            }
        }

        #region 数据绑定
        public void RptBind()
        {
            string strWhere = "";
            if (Session["strWhereProduct_Zt"] != null && Session["strWhereProduct_Zt"].ToString() != "")
            {
                strWhere = Session["strWhereProduct_Zt"].ToString();
            }


            ZGD.BLL.NewsInfo bll = new ZGD.BLL.NewsInfo();
            DataSet ds = bll.GetZtList(strWhere);
            DataView dv = ds.Tables[0].DefaultView;
            //利用PAGEDDAGASOURCE类来分页
            PagedDataSource pds = new PagedDataSource();
            AspNetPager1.RecordCount = dv.Count;
            pds.DataSource = dv;
            pds.AllowPaging = true;
            pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;
            pds.PageSize = AspNetPager1.PageSize;

            //获得总条数
            if (dv.Count > 0)
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
            ZGD.BLL.NewsInfo bll = new ZGD.BLL.NewsInfo();
            ZGD.Model.NewsInfo model = bll.GetModel(id);
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
            ZGD.BLL.NewsInfo bll = new ZGD.BLL.NewsInfo();
            ZGD.Model.NewsInfo model;
            //批量删除
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((Label)rptList.Items[i].FindControl("lb_id")).Text);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("cb_id");
                if (cb.Checked)
                {
                    model = bll.GetModel(id);
                    //保存日志
                    SaveLogs("[专题文章模块]删除专题文章：" + model.Title);
                    //删除记录
                    bll.Delete(id);
                }
            }
            JscriptMsg("删除成功。");
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
        protected void ddlClassId_SelectedIndexChanged(object sender, EventArgs e)
        {
            string SupplierName = this.ddlClassId.SelectedValue;
            string strsql = "";
            if (SupplierName != "")
            {
                strsql = " and nc.ClassId='" + SupplierName + "'";
            }
            if (strsql != "")
            {
                Session["strWhereProduct_Zt"] = strsql;
            }
            else
            {
                Session["strWhereProduct_Zt"] = "";
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
                        strsql += (" and n.IsTop=1");
                    }
                    if (strsql != "")
                    {
                        Session["strWhereProduct_Zt"] = strsql;
                    }
                    else
                    {
                        Session["strWhereProduct_Zt"] = "";
                    }
                    break;
                case "IsLock":
                    if (SupplierName != "")
                    {
                        strsql += (" and n.IsLock=1");
                    }
                    if (strsql != "")
                    {
                        Session["strWhereProduct_Zt"] = strsql;
                    }
                    else
                    {
                        Session["strWhereProduct_Zt"] = "";
                    }
                    break;
                default:
                    if (strsql != "")
                    {
                        Session["strWhereProduct_Zt"] = strsql;
                    }
                    else
                    {
                        Session["strWhereProduct_Zt"] = "";
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
                strsql += " and n.Title like '%" + SupplierName + "%'";
            }
            if (strsql != "")
            {
                Session["strWhereProduct_Zt"] = strsql;
            }
            else
            {
                Session["strWhereProduct_Zt"] = "";
            }

            //重新绑定数据
            RptBind();
        }
        #endregion

    }
}