using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

namespace ZGD.Web.Admin.WeiXin
{
    public partial class CardList : ZGD.BasePage.ManagePage
    {
        public int pcount = 0; //总条数

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //数据绑定
                RptBind();
                //绑定类别
                ChannelTreeBind2(34, "所有部门", 6, this.ddlProperty);
            }
        }

        #region 数据绑定
        public void RptBind()
        {
            string strWhere = "";
            if (Session["strWhereProduct_WXCard"] != null && Session["strWhereProduct_WXCard"].ToString() != "")
            {
                strWhere = Session["strWhereProduct_WXCard"].ToString();
            }

            ZGD.BLL.WXCard bll = new ZGD.BLL.WXCard();
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

        #region 类别索引
        protected void ddlProperty_SelectedIndexChanged(object sender, EventArgs e)
        {
            string SupplierName = this.ddlProperty.SelectedValue;
            string strsql = "";
            if (SupplierName != "")
            {
                strsql = " and Dept='" + SupplierName + "'";
            }
            if (strsql != "")
            {
                Session["strWhereProduct_WXCard"] = " (1=1) " + strsql;
            }
            else
            {
                Session["strWhereProduct_WXCard"] = "";
            }
            //重新绑定数据
            RptBind();
        }
        #endregion

        #region 批量删除
        protected void lbtnDel_Click(object sender, EventArgs e)
        {
            ZGD.BLL.WXCard bll = new ZGD.BLL.WXCard();
            ZGD.Model.WXCard model;
            //批量删除
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((Label)rptList.Items[i].FindControl("lb_id")).Text);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("cb_id");
                if (cb.Checked)
                {
                    model = bll.GetModel(id);
                    //保存日志
                    SaveLogs("[微名片模块]删除微名片：" + model.uName);
                    //删除记录
                    bll.Delete(id);
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

        #region 查询
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            string SupplierName = this.txtKeywords.Text.Trim();
            string strsql = "";
            if (SupplierName != "")
            {
                strsql += "and (uName like '%" + SupplierName + "%' or Phone like '%" + SupplierName + "%')";
            }
            if (strsql != "")
            {
                Session["strWhereProduct_WXCard"] = " (1=1) " + strsql;
            }
            else
            {
                Session["strWhereProduct_WXCard"] = "";
            }

            //重新绑定数据
            RptBind();
        }
        #endregion
    }
}