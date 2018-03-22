using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZGD.Web.Admin.SystemLog
{
    public partial class List : ZGD.BasePage.ManagePage
    {
        //public int pcount; //总条数
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //数据绑定
                RptBind("");
            }
        }

        #region 数据列表绑定
        private void RptBind(string strWhere)
        {
            ZGD.BLL.SystemLog bll = new ZGD.BLL.SystemLog();
            DataSet ds = bll.GetList(strWhere);
            DataView dv = ds.Tables[0].DefaultView;
            //利用PAGEDDAGASOURCE类来分页
            PagedDataSource pds = new PagedDataSource();
            AspNetPager1.RecordCount = dv.Count;
            pds.DataSource = dv;
            pds.AllowPaging = true;
            pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;
            pds.PageSize = AspNetPager1.PageSize;
            this.rptList.DataSource = pds;
            this.rptList.DataBind();
        }
        #endregion

        #region 批量删除
        protected void lbtnDel_Click(object sender, EventArgs e)
        {
            ZGD.BLL.SystemLog bll = new ZGD.BLL.SystemLog();
            //清空日志
            int num = bll.Delete(7);

            //保存日志
            SaveLogs("[系统日志]共清空了" + num + "条日志！");
            JscriptMsg("共清空了" + num + "条日志！");
            RptBind("");
        }
        #endregion

        protected void AspNetPager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            AspNetPager1.CurrentPageIndex = e.NewPageIndex;
            RptBind("");
        }

        #region 批量删除
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            ZGD.BLL.SystemLog bll = new ZGD.BLL.SystemLog();
            ZGD.Model.SystemLog model;
            //批量删除
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((Label)rptList.Items[i].FindControl("lb_id")).Text);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("cb_id");
                if (cb.Checked)
                {
                    model = bll.GetModel(id);
                    //删除记录
                    bll.Delete1(id);
                }
            }
            JscriptMsg("批量删除成功");
            RptBind("");
        }
        #endregion

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {

        }
    }
}