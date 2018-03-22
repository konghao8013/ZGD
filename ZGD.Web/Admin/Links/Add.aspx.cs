using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZGD.Common;

namespace ZGD.Web.Admin.Links
{
    public partial class Add : ZGD.BasePage.ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //绑定类别
                ChannelTreeBind(37, "请选择链接类型", 7, this.ddlClassId);
            }
        }

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
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }

            ZGD.Model.Links model = new ZGD.Model.Links();
            ZGD.BLL.Links bll = new ZGD.BLL.Links();

            model.Title = txtTitle.Text.Trim();
            model.WebUrl = txtWebUrl.Text.Trim();
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
            model.ClassId = ddlClassId.SelectedIndex > 0 ? int.Parse(ddlClassId.SelectedValue) : 0;
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
            bll.Add(model);

            //保存日志
            SaveLogs("[链接管理]增加链接：" + model.Title);
            ddlClassId.SelectedIndex = 0;
            cbIsImage.Checked = false;
            this.txtTitle.Text = "";
            this.txtImgUrl.Text = "";
            this.txtUserMail.Text = "";
            this.txtUserName.Text = "";
            this.txtUserTel.Text = "";
            JscriptMsg("提示保存成功");
        }
        #endregion

        protected void Button1_Click(object sender, EventArgs e)
        {
            ddlClassId.SelectedIndex = 0;
            cbIsImage.Checked = false;
            this.txtTitle.Text = "";
            this.txtImgUrl.Text = "";
            this.txtUserMail.Text = "";
            this.txtUserName.Text = "";
            this.txtUserTel.Text = "";
            this.txtWebUrl.Text = "";
        }

    }
}