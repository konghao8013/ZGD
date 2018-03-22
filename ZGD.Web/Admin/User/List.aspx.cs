using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

namespace ZGD.Web.Admin.User
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
            if (Session["strWhereProduct_User"] != null && Session["strWhereProduct_User"].ToString() != "")
            {
                strWhere = Session["strWhereProduct_User"].ToString();
            }

            ZGD.BLL.User bll = new ZGD.BLL.User();
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
            //绑定数据
            rptList.DataSource = pds;
            rptList.DataBind();
        }
        #endregion

        #region 设置操作
        protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int id = Convert.ToInt32(((Label)e.Item.FindControl("lb_id")).Text);
            ZGD.BLL.User bll = new ZGD.BLL.User();
            ZGD.Model.User model = bll.GetModel(id);
            switch (e.CommandName.ToLower())
            {
                case "ibtnlock":
                    if (model.IsLock == 1)
                        bll.UpdateField(id, "IsLock=0");
                    else
                        bll.UpdateField(id, "IsLock=1");
                    break;
            }
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
                case "IsLock":
                    if (SupplierName != "")
                    {
                        strsql += (" and IsLock=1");
                    }
                    if (strsql != "")
                    {
                        Session["strWhereProduct_User"] = " (1=1) " + strsql;
                    }
                    else
                    {
                        Session["strWhereProduct_User"] = "";
                    }
                    break;
                default:
                    if (strsql != "")
                    {
                        Session["strWhereProduct_User"] = " (1=1) " + strsql;
                    }
                    else
                    {
                        Session["strWhereProduct_User"] = "";
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
                strsql += "and (Nickname like '%" + SupplierName + "%' or Phone like '%" + SupplierName + "%')";
            }
            if (strsql != "")
            {
                Session["strWhereProduct_User"] = " (1=1) " + strsql;
            }
            else
            {
                Session["strWhereProduct_User"] = "";
            }

            //重新绑定数据
            RptBind();
        }
        #endregion

    }
}