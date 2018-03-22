using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

namespace ZGD.Web.Admin.Photos
{
    public partial class List : ZGD.BasePage.ManagePage
    {
        public int pcount = 0; //总条数
        public int pType = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["t"]))
            {
                pType = Convert.ToInt32(Request.QueryString["t"]);
            }
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
            if (Session["strWhereProduct_Photo"] != null && Session["strWhereProduct_Photo"].ToString() != "")
            {
                strWhere += Session["strWhereProduct_Photo"].ToString();
            }
            if (!string.IsNullOrWhiteSpace(strWhere))
            {
                strWhere += " and Type=" + pType;
            }
            else
            {
                strWhere = " Type=" + pType;
            }

            ZGD.BLL.Photos bll = new ZGD.BLL.Photos();
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

        #region 设置操作
        protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int id = Convert.ToInt32(((Label)e.Item.FindControl("lb_id")).Text);
            ZGD.BLL.Photos bll = new ZGD.BLL.Photos();
            ZGD.Model.Photos model = bll.GetModel(id);
            switch (e.CommandName.ToLower())
            {
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
            ZGD.BLL.Photos bll = new ZGD.BLL.Photos();
            ZGD.BLL.ProjectImg imgBll = new ZGD.BLL.ProjectImg();
            ZGD.Model.Photos model;
            //批量删除
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((Label)rptList.Items[i].FindControl("lb_id")).Text);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("cb_id");
                if (cb.Checked)
                {
                    model = bll.GetModel(id);
                    //保存日志
                    SaveLogs("[相册模块]删除相册：" + model.PhotoName);
                    //删除记录
                    bll.Delete(id);
                    imgBll.Delete(id, 3);
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
                        Session["strWhereProduct_Photo"] = " (1=1) " + strsql;
                    }
                    else
                    {
                        Session["strWhereProduct_Photo"] = "";
                    }
                    break;
                case "IsLock":
                    if (SupplierName != "")
                    {
                        strsql += (" and IsLock=1");
                    }
                    if (strsql != "")
                    {
                        Session["strWhereProduct_Photo"] = " (1=1) " + strsql;
                    }
                    else
                    {
                        Session["strWhereProduct_Photo"] = "";
                    }
                    break;
                default:
                    if (strsql != "")
                    {
                        Session["strWhereProduct_Photo"] = " (1=1) " + strsql;
                    }
                    else
                    {
                        Session["strWhereProduct_Photo"] = "";
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
                strsql += "and Title PhotoName '%" + SupplierName + "%'";
            }
            if (strsql != "")
            {
                Session["strWhereProduct_Photo"] = " (1=1) " + strsql;
            }
            else
            {
                Session["strWhereProduct_Photo"] = "";
            }

            //重新绑定数据
            RptBind();
        }
        #endregion

    }
}