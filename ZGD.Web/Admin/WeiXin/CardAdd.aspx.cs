using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZGD.Common;

namespace ZGD.Web.Admin.WeiXin
{
    public partial class CardAdd : ZGD.BasePage.ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //绑定类别
                ChannelTreeBind2(34, "所有部门", 6, this.ddlProperty);
            }
        }

        #region 添加
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string strErr = "";
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                strErr += "名片用户为空！\\n";
            }
            if (string.IsNullOrWhiteSpace(ddlProperty.SelectedValue))
            {
                strErr += "所属部门为空！\\n";
            }
            if (string.IsNullOrWhiteSpace(txtRole.Text))
            {
                strErr += "所在职位为空！\\n";
            }
            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                strErr += "联系电话为空！\\n";
            }
            if (string.IsNullOrWhiteSpace(txtCardUrl.Text))
            {
                strErr += "名片地址为空！\\n";
            }
            if (string.IsNullOrWhiteSpace(txtImgUrl.Text))
            {
                strErr += "名片头像为空！\\n";
            }
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }

            ZGD.Model.WXCard model = new ZGD.Model.WXCard();
            model.uName = txtName.Text;
            model.Dept = ddlProperty.SelectedValue;
            model.Role = txtRole.Text;
            model.ImgUrl = ZGD.Common.Thumbnail.CreateThumbImg(txtImgUrl.Text, 100, 100, "W");
            model.Phone = txtPhone.Text;
            model.CardUrl = txtCardUrl.Text;
            model.Remark = txtRemark.Value;

            ZGD.BLL.WXCard bll = new ZGD.BLL.WXCard();
            int ReId = bll.Add(model);
            if (ReId > 0)
            {
                //保存日志
                SaveLogs("[微名片模块]添加微名片：" + model.uName);
                JscriptMsg("保存成功", "CardList.aspx");
            }
            else
            {
                JscriptMsg("发布过程中发生错误");
            }
        }
        #endregion

        protected void btnReset_Click(object sender, EventArgs e)
        {
            this.ddlProperty.SelectedIndex = 0;
            this.txtName.Text = "";
            this.txtImgUrl.Text = "";
            this.txtRemark.Value = "";
            this.txtRole.Text = "";
            this.txtPhone.Text = "";
            this.txtCardUrl.Text = "";
        }
    }
}