using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZGD.Common;

namespace ZGD.Web.Admin.Links
{
    public partial class Edit : ZGD.BasePage.ManagePage
    {
        public int Id;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!int.TryParse(Request.Params["id"] as string, out this.Id))
            {
                JscriptMsg("您要修改的信息不存在或参数不正确。");
                return;
            }
            if (!Page.IsPostBack)
            {
                //绑定类别
                ChannelTreeBind(49, "请选择链接类型", 4, this.ddlClassId);
                ShowInfo(this.Id);
            }
        }

        #region 赋值操作
        private void ShowInfo(int _id)
        {
            ZGD.BLL.Links bll = new ZGD.BLL.Links();
            ZGD.Model.Links model = bll.GetModel(_id);
            txtTitle.Text = model.Title;
            txtWebUrl.Text = model.WebUrl;
            ddlClassId.SelectedValue = model.ClassId.ToString();
            if (model.IsImage == 1)
            {
                cbIsImage.Checked = true;
                imgPanel.InnerHtml = "<img src=\"" + model.ImgUrl + "\" />";
                tr_img_panel.Style["display"] = "block";
            }
            else
            {
                imgPanel.Style["display"] = "none";
                tr_img_panel.Style["display"] = "none";
            }
            txtImgUrl.Text = model.ImgUrl;
            txtUserName.Text = model.UserName;
            txtUserTel.Text = model.UserTel;
            txtUserMail.Text = model.UserMail;
            if (model.IsRed == 1)
            {
                cblItem.Items[0].Selected = true;
            }
            if (model.IsLock == 1)
            {
                cblItem.Items[1].Selected = true;
            }
            txtSortId.Text = model.SortId.ToString();
        }
        #endregion

        #region 添加链接
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string strErr = "";
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                strErr += "标题不能为空！\\n";
            }
            if (string.IsNullOrWhiteSpace(txtWebUrl.Text))
            {
                strErr += "地址不能为空！\\n";
            }
            if (!string.IsNullOrWhiteSpace(txtUserTel.Text))
            {
                if (!PageValidate.IsPhone(txtUserTel.Text))
                {
                    strErr += "联系电话格式错误！\\n";
                }
            }
            if (!string.IsNullOrWhiteSpace(txtUserMail.Text))
            {
                if (!PageValidate.IsEmail(txtUserMail.Text))
                {
                    strErr += "邮箱格式错误！\\n";
                }
            }

            if (!string.IsNullOrWhiteSpace(strErr))
            {
                MessageBox.Show(this, strErr);
                return;
            }

            ZGD.BLL.Links bll = new ZGD.BLL.Links();
            ZGD.Model.Links model = bll.GetModel(this.Id);
            model.Title = txtTitle.Text.Trim();
            model.WebUrl = txtWebUrl.Text.Trim();
            model.ClassId = ddlClassId.SelectedIndex > 0 ? int.Parse(ddlClassId.SelectedValue) : 0;
            model.IsImage = 0;
            if (cbIsImage.Checked == true)
            {
                model.IsImage = 1;
            }
            model.ImgUrl = txtImgUrl.Text.Trim();
            model.UserName = txtUserName.Text.Trim();
            model.UserTel = txtUserTel.Text.Trim();
            model.UserMail = txtUserMail.Text.Trim();
            model.SortId = int.Parse(txtSortId.Text.Trim());
            model.IsRed = 0;
            model.IsLock = 0;
            if (cblItem.Items[0].Selected == true)
            {
                model.IsRed = 1;
            }
            if (cblItem.Items[1].Selected == true)
            {
                model.IsLock = 1;
            }
            bll.Update(model);

            //保存日志
            SaveLogs("[链接管理]编辑链接：" + model.Title);
            JscriptMsg("保存成功", "List.aspx");
        }
        #endregion

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("List.aspx");
        }

    }
}