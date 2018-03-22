
using ZGD.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZGD.Web.Admin.Activity
{
    public partial class Edit : ZGD.BasePage.ManagePage
    {
        public int Id;
        public string tName = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!int.TryParse(Request.Params["id"] as string, out this.Id))
            {
                JscriptMsg("您要修改的信息不存在或参数不正确。");
                return;
            }

            if (!Page.IsPostBack)
            {
                //赋值
                ShowInfo(this.Id);
            }
        }
        private void ShowInfo(int _id)
        {
            ZGD.BLL.Activity bll = new ZGD.BLL.Activity();
            ZGD.Model.Activity model = bll.GetModel(_id);

            txtTitle.Text = model.Title;
            txtKeyword.Text = model.Keyword;
            txtDesc.Value = model.Description;
            txtsDate.Text = model.sDate;
            txteDate.Text = model.eDate;
            txtImgUrl.Text = model.ImgUrl;
            txtActivityUrl.Text = model.aUrl;
            cblItem.SelectedValue = model.Status.ToString();
            kEditor.Value = ZGD.Common.StringHandler.DeCode(model.aCon);
            txtClick.Text = model.Click.ToString();
            if (!string.IsNullOrWhiteSpace(model.ImgUrl))
            {
                imgPanel.InnerHtml = "<img src=\"" + model.ImgUrl + "\" />";
            }
            else
            {
                imgPanel.Style["display"] = "none";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string strErr = "";
            if (this.txtTitle.Text.Trim().Length == 0)
            {
                strErr += "活动标题不能为空！\\n";
            }
            if (this.txtsDate.Text.Trim().Length == 0)
            {
                strErr += "活动开始时间不能为空！\\n";
            }
            if (this.txteDate.Text.Trim().Length == 0)
            {
                strErr += "活动结束时间不能为空！\\n";
            }
            if (this.txtDesc.Value.Trim().Length == 0)
            {
                strErr += "内容不能为空！\\n";
            }
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }

            ZGD.BLL.Activity bll = new ZGD.BLL.Activity();
            ZGD.Model.Activity model = bll.GetModel(this.Id);

            model.Title = this.txtTitle.Text;
            model.Keyword = this.txtKeyword.Text;
            model.sDate = Convert.ToDateTime(txtsDate.Text).ToString("yyyy-MM-dd HH:mm");
            model.eDate = Convert.ToDateTime(txteDate.Text).ToString("yyyy-MM-dd HH:mm");
            model.Description = txtDesc.Value;
            model.ImgUrl = txtImgUrl.Text;
            model.aUrl = txtActivityUrl.Text;
            model.aCon = ZGD.Common.StringHandler.EnCode(Request.Form["kEditor"]);
            model.Status = Convert.ToInt32(cblItem.SelectedValue);
            model.Click = string.IsNullOrWhiteSpace(txtClick.Text) ? 0 : Convert.ToInt32(txtClick.Text);
            model.JoinCount = string.IsNullOrWhiteSpace(txtJoinCount.Text) ? 0 : Convert.ToInt32(txtJoinCount.Text);
            if (bll.Update(model))
            {
                //保存日志
                SaveLogs("[活动模块]添加活动：" + model.Title);
                JscriptMsg("保存成功", "List.aspx");
            }
            else
            {
                JscriptMsg("发布过程中发生错误");
            }
        }

        protected void btnretset_Click(object sender, EventArgs e)
        {
            Response.Redirect("List.aspx");
        }
    }
}