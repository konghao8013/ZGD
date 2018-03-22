using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZGD.Web.Admin.Links
{
    public partial class List : ZGD.BasePage.ManagePage
    {
        public int pcount = 0; //总条数

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //绑定类别
                ChannelTreeBind(37, "请选择链接类型", 7, this.ddlClassId);
                //数据绑定
                RptBind();
            }
        }

        #region 数据绑定
        public void RptBind()
        {
            string strWhere = "";
            if (Session["strWhereProduct_links"] != null && Session["strWhereProduct_links"].ToString() != "")
            {
                strWhere += Session["strWhereProduct_links"].ToString();
            }

            ZGD.BLL.Links bll = new ZGD.BLL.Links();
            DataSet ds = bll.GetListPage(strWhere);
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
                this.lbtnAudit.Enabled = true;
            }
            else
            {
                this.lbtnDel.Enabled = false;
                this.lbtnAudit.Enabled = false;
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

        #region 批量审核
        protected void lbtnAudit_Click(object sender, EventArgs e)
        {
            ZGD.BLL.Links bll = new ZGD.BLL.Links();
            //批量审核
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((Label)rptList.Items[i].FindControl("lb_id")).Text);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("cb_id");
                if (cb.Checked)
                {
                    //审核
                    bll.UpdateField(id, "IsLock=0");
                    //保存日志
                    SaveLogs("[链接管理]审核链接：" + bll.GetModel(id).Title);
                }
            }
            JscriptMsg("批量审核成功");
            RptBind();
        }
        #endregion

        #region 批量删除
        protected void lbtnDel_Click(object sender, EventArgs e)
        {
            ZGD.BLL.Links bll = new ZGD.BLL.Links();
            //批量删除
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((Label)rptList.Items[i].FindControl("lb_id")).Text);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("cb_id");
                if (cb.Checked)
                {
                    //删除图片
                    DeleteFile(bll.GetModel(id).ImgUrl);
                    //保存日志
                    SaveLogs("[链接管理]删除链接：" + bll.GetModel(id).Title);
                    //删除记录
                    bll.Delete(id);
                }
            }
            JscriptMsg("批量删除成功");
            RptBind();
        }
        #endregion

        #region 类别索引
        protected void ddlClassId_SelectedIndexChanged(object sender, EventArgs e)
        {
            string SupplierName = this.ddlClassId.SelectedValue;
            string strsql = "";
            if (SupplierName != "")
            {
                strsql = " and l.ClassId='" + SupplierName + "'";
            }
            if (strsql != "")
            {
                Session["strWhereProduct_links"] = " (1=1) " + strsql;
            }
            else
            {
                Session["strWhereProduct_links"] = "";
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
                case "IsLock":
                    if (SupplierName != "")
                    {
                        strsql += (" and l.IsLock=1");
                    }
                    if (strsql != "")
                    {
                        Session["strWhereProduct_links"] = " (1=1) " + strsql;
                    }
                    else
                    {
                        Session["strWhereProduct_links"] = "";
                    }
                    break;
                case "IsRed":
                    if (SupplierName != "")
                    {
                        strsql += (" and l.IsRed=1");
                    }
                    if (strsql != "")
                    {
                        Session["strWhereProduct_links"] = " (1=1) " + strsql;
                    }
                    else
                    {
                        Session["strWhereProduct_links"] = "";
                    }
                    break;
                default:
                    if (strsql != "")
                    {
                        Session["strWhereProduct_links"] = " (1=1) " + strsql;
                    }
                    else
                    {
                        Session["strWhereProduct_links"] = "";
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
                strsql += "and Title l.like '%" + SupplierName + "%'";
            }
            if (strsql != "")
            {
                Session["strWhereProduct_links"] = " (1=1) " + strsql;
            }
            else
            {
                Session["strWhereProduct_links"] = "";
            }

            //重新绑定数据
            RptBind();
        }
        #endregion
    }
}