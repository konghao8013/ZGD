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
    public partial class CardEdit : ZGD.BasePage.ManagePage
    {
        public int Id;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!int.TryParse(Request.Params["id"] as string, out this.Id))
            {
                JscriptMsg("出现错误您要修改的信息不存在或参数不正确。");
                return;
            }
            if (!Page.IsPostBack)
            {
                //绑定类别
                ChannelTreeBind2(34, "所有部门", 6, this.ddlProperty);
                ShowInfo();
            }
        }

        #region 赋值操作
        private void ShowInfo()
        {
            ZGD.BLL.WXCard bll = new ZGD.BLL.WXCard();
            ZGD.Model.WXCard model = bll.GetModel(this.Id);

            txtName.Text = model.uName;
            txtImgUrl.Text = model.ImgUrl;
            txtPhone.Text = model.Phone;
            txtRole.Text = model.Role;
            txtCardUrl.Text = model.CardUrl;
            ddlProperty.SelectedItem.Text = model.Dept;
            txtRemark.Value = model.Remark;

            if (!string.IsNullOrWhiteSpace(model.ImgUrl))
            {
                imgPanel.InnerHtml = "<img src=\"" + model.ImgUrl + "\" width=\"100\" height=\"100\" />";
            }
            else
            {
                imgPanel.Style["display"] = "none";
            }
        }
        #endregion

        #region 修改操作
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string strErr = "";
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                strErr += "设计师名称为空！\\n";
            }
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }

            ZGD.BLL.WXCard bll = new ZGD.BLL.WXCard();
            ZGD.Model.WXCard model = bll.GetModel(this.Id);
            model.uName = txtName.Text;
            model.Dept = ddlProperty.SelectedValue;
            model.Role = txtRole.Text;
            //缩略图生产
            if (!string.IsNullOrWhiteSpace(txtImgUrl.Text) && model.ImgUrl != txtImgUrl.Text)
            {
                model.ImgUrl = ZGD.Common.Thumbnail.CreateThumbImg(txtImgUrl.Text, 100, 100, "W");
            }
            model.Phone = txtPhone.Text;
            model.Remark = txtRemark.Value;
            bll.Update(model);

            //保存日志
            SaveLogs("[名片模块]编辑名片：" + model.uName);
            JscriptMsg("保存成功", "CardList.aspx");
        }
        #endregion

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("CardList.aspx");
        }

    }
}