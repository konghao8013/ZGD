using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZGD.Web.Admin.ST
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
            }
        }

        #region 数据绑定
        public void RptBind()
        {
            string strWhere = "";
            if (Session["strWhereProduct_ST"] != null && Session["strWhereProduct_ST"].ToString() != "")
            {
                strWhere += Session["strWhereProduct_ST"].ToString();
            }

            ZGD.BLL.ServiceTools bll = new ZGD.BLL.ServiceTools();
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

        protected void AspNetPager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            AspNetPager1.CurrentPageIndex = e.NewPageIndex;
            RptBind();
        }

        #region 设置操作
        protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int id = Convert.ToInt32(((Label)e.Item.FindControl("lb_id")).Text);
            ZGD.BLL.ServiceTools bll = new ZGD.BLL.ServiceTools();
            ZGD.Model.ServiceTools model = bll.GetModel(id);
            switch (e.CommandName.ToLower())
            {
                case "ibtnlock":
                    if (model.Stuats == 1)
                        bll.UpdateField(id, "Stuats=0");
                    else
                        bll.UpdateField(id, "Stuats=1");
                    break;
            }
            RptBind();
        }
        #endregion
        
        #region 批量删除
        protected void lbtnDel_Click(object sender, EventArgs e)
        {
            ZGD.BLL.ServiceTools bll = new ZGD.BLL.ServiceTools();
            //批量删除
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((Label)rptList.Items[i].FindControl("lb_id")).Text);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("cb_id");
                if (cb.Checked)
                {
                    //删除记录
                    bll.Delete(id);
                    //保存日志
                    var model = bll.GetModel(id);
                    SaveLogs("[客服管理]删除客服：" + model.Title + "（" + model.Num + "）");
                }
            }
            JscriptMsg("批量删除成功");
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
                case "IsLock":
                    if (SupplierName != "")
                    {
                        strsql += (" and Stuats=0");
                    }
                    if (strsql != "")
                    {
                        Session["strWhereProduct_ST"] = " (1=1) " + strsql;
                    }
                    else
                    {
                        Session["strWhereProduct_ST"] = "";
                    }
                    break;
                case "IsRed":
                    if (SupplierName != "")
                    {
                        strsql += (" and Stuats=1");
                    }
                    if (strsql != "")
                    {
                        Session["strWhereProduct_ST"] = " (1=1) " + strsql;
                    }
                    else
                    {
                        Session["strWhereProduct_ST"] = "";
                    }
                    break;
                default:
                    if (strsql != "")
                    {
                        Session["strWhereProduct_ST"] = " (1=1) " + strsql;
                    }
                    else
                    {
                        Session["strWhereProduct_ST"] = "";
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
                strsql += "and (Title like '%" + SupplierName + "%' or Num like '%" + SupplierName + "%')";
            }
            if (strsql != "")
            {
                Session["strWhereProduct_ST"] = " (1=1) " + strsql;
            }
            else
            {
                Session["strWhereProduct_ST"] = "";
            }

            //重新绑定数据
            RptBind();
        }
        #endregion
    }
}