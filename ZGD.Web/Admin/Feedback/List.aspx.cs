using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZGD.Web.Admin.Feedback
{
    public partial class List : ZGD.BasePage.ManagePage
    {
        public int pcount = 0; //总条数

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ////绑定类别
                //ChannelTreeBind(11, "所有风格", 3, this.ddlFGType);
                ////绑定类别
                //ChannelTreeBind(15, "所有户型", 4, this.ddlHXType);
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

            ZGD.BLL.Feedback bll = new ZGD.BLL.Feedback();
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

        #region 批量删除
        protected void lbtnDel_Click(object sender, EventArgs e)
        {
            ZGD.BLL.Feedback bll = new ZGD.BLL.Feedback();
            //批量删除
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((Label)rptList.Items[i].FindControl("lb_id")).Text);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("cb_id");
                if (cb.Checked)
                {
                    //保存日志
                    SaveLogs("[报名管理]删除报名：" + bll.GetModel(id).UserName);
                    //删除记录
                    bll.Delete(id);
                }
            }
            JscriptMsg("批量删除成功");
            RptBind();
        }
        #endregion

        #region 属性索引
        protected void ddlFType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string SupplierName = Convert.ToString(this.ddlFType.SelectedValue);
            string strsql = "";
            if (string.IsNullOrWhiteSpace(SupplierName))
            {
                Session["strWhereProduct_Fed"] = "";
            }
            else
            {
                strsql += (" and fType=" + SupplierName);
                if (strsql != "")
                {
                    Session["strWhereProduct_Fed"] = " (1=1) " + strsql;
                }
                else
                {
                    Session["strWhereProduct_Fed"] = "";
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
                strsql += "and (UserName like '%" + SupplierName + "%' or UserTel like '%" + SupplierName + "%')";
            }
            if (strsql != "")
            {
                Session["strWhereProduct_Fed"] = " (1=1) " + strsql;
            }
            else
            {
                Session["strWhereProduct_Fed"] = "";
            }

            //重新绑定数据
            RptBind();
        }
        #endregion

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {

        }

        public static string GetFType(int type)
        {
            switch (type)
            {
                case 1:
                    return "装修报价";
                case 2:
                    return "参观展厅";
                case 3:
                    return "查看工地";
                default:
                    return "其他";
            }
        }
    }
}