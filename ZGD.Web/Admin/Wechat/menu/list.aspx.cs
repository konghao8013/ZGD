using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZGD.Common;
using System.Text;
using System.Data;

namespace ZGD.Web.Admin.wechat.menu
{
    public partial class list : ZGD.BasePage.ManagePage
    {
        private BLL.wechat n_wechat = new BLL.wechat();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("sys_wechat_setting_menu", DTEnums.ActionEnum.View.ToString()); //检查权限
                RptBind(" menu_sort asc,id desc");
            }
        }

        #region 数据绑定=================================
        private void RptBind( string _orderby)
        {
            DataTable dt = n_wechat.menu_list(0);
            this.rptList.DataSource = dt;
            this.rptList.DataBind();
        }
        #endregion

        //美化列表
        protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Literal LitFirst = (Literal)e.Item.FindControl("LitFirst");
                HiddenField hidLayer = (HiddenField)e.Item.FindControl("hidLayer");
                string LitStyle = "<span style=\"display:inline-block;width:{0}px;\"></span>{1}{2}";
                string LitImg1 = "<span class=\"folder-open\"></span>";
                string LitImg2 = "<span class=\"folder-line\"></span>";

                int classLayer = Convert.ToInt32(hidLayer.Value);
                if (classLayer ==0 )
                {
                    LitFirst.Text = LitImg1;
                }
                else
                {
                    LitFirst.Text = string.Format(LitStyle,   24, LitImg2, LitImg1);
                }
            }
        }

        //保存排序
        protected void btnSave_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("sys_wechat_setting_menu", DTEnums.ActionEnum.Edit.ToString()); //检查权限
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                int sortId;
                if (!int.TryParse(((TextBox)rptList.Items[i].FindControl("txtSortId")).Text.Trim(), out sortId))
                {
                    sortId = 99;
                }
                n_wechat.menu_update_fild(id, "menu_sort=" + sortId.ToString());
            }
            AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "保存微信菜单"); //记录日志
            JscriptMsg("保存排序成功！", Utils.CombUrlTxt("list.aspx", "keywords={0}", ""), "Success", "");
        }

        //批量删除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("sys_wechat_setting_menu", DTEnums.ActionEnum.Delete.ToString()); //检查权限
            int sucCount = 0;
            int errorCount = 0;
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    //检查该分类下是否还有频道
                    int channelCount = n_wechat.menu_count(id);
                    if (channelCount > 0)
                    {
                        sucCount += 1;
                    }
                    //删除成功后对应的目录及文件
                    if (n_wechat.menu_Delete(id)>0)
                    {
                        //sucCount += 1;
                    }
                    else
                    {
                        errorCount += 1;
                    }
                }
            }
            AddAdminLog(DTEnums.ActionEnum.Delete.ToString(), "删除频道分类成功" + sucCount + "条，失败" + errorCount + "条"); //记录日志
            JscriptMsg("删除成功" + (sucCount - errorCount) + "条，失败" + errorCount + "条！", Utils.CombUrlTxt("list.aspx", "keywords={0}",""), "Success", "");
        }

        protected void btn_create_menu_Click(object sender, EventArgs e)
        {
            if (n_wechat.create_menu())
            {
                JscriptMsg("生成菜单成功", Utils.CombUrlTxt("list.aspx", "keywords={0}", ""), "Success", "");
            }
            else
            {
                JscriptMsg("生成菜单失败", Utils.CombUrlTxt("list.aspx", "keywords={0}", ""), "Error", "");
            }
        }
    }
}