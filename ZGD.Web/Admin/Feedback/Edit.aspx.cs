using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZGD.Web.Admin.Feedback
{
    public partial class Edit : ZGD.BasePage.ManagePage
    {
        public int Id;
        public string tName = "";
        public ZGD.Model.Feedback model = new ZGD.Model.Feedback();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!int.TryParse(Request.Params["id"] as string, out this.Id))
            {
                JscriptMsg("您要回复的信息不存在或参数不正确。");
                return;
            }
            if (!Page.IsPostBack)
            {
                //绑定类别
                ChannelTreeBind(11, "所有风格", 3, this.ddlFGType);
                //绑定类别
                ChannelTreeBind(15, "所有户型", 4, this.ddlHXType);
                //赋值
                ShowInfo(this.Id);
            }
        }

        #region 赋值操作
        private void ShowInfo(int _id)
        {
            ZGD.BLL.Feedback bll = new ZGD.BLL.Feedback();
            model = bll.GetModel(_id);
            var tModel = new ZGD.BLL.Channel().GetModelById(model.ClassId);
            tName = tModel != null ? tModel.Title : "";
            txtName.Text = model.UserName;
            txtPhone.Text = model.UserTel;
            txtQQ.Text = model.UserQQ;
            txtAddTime.Text = model.AddTime.ToString("yyyy-MM-dd HH:mm:ss");
            txtArea.Text = model.Area.ToString();
            txtHouse.Text = model.House;
            txtXQ.Text = model.YaoQiu;
            rbtnSex.SelectedValue = model.Sex;
            ddlHXType.SelectedValue = model.ClassId.ToString();
            ddlFGType.SelectedValue = model.ClassId2.ToString();
            ddlFType.SelectedValue = model.fType.ToString();
            txtRemark.Text = model.Remark;
        }
        #endregion

        #region 回复留言
        protected void btnSave_Click(object sender, EventArgs e)
        {
            ZGD.BLL.Feedback bll = new ZGD.BLL.Feedback();
            ZGD.Model.Feedback model = bll.GetModel(this.Id);
            model.UserName = txtName.Text;
            model.UserTel = txtPhone.Text;
            model.UserQQ = txtQQ.Text;
            model.Area = string.IsNullOrWhiteSpace(txtArea.Text) ? 0 : Convert.ToDecimal(txtArea.Text);
            model.House = txtHouse.Text;
            model.YaoQiu = txtXQ.Text;
            model.Sex = rbtnSex.SelectedValue;
            model.ClassId = string.IsNullOrWhiteSpace(ddlHXType.SelectedValue) ? 0 : Convert.ToInt32(ddlHXType.SelectedValue);
            model.fType = Convert.ToInt32(ddlFType.SelectedValue);
            model.ClassId2 = string.IsNullOrWhiteSpace(ddlFGType.SelectedValue) ? 0 : Convert.ToInt32(ddlFGType.SelectedValue);
            model.Remark = txtRemark.Text;
            bll.Update(model);
            ShowInfo(this.Id);
            //保存日志
            SaveLogs("[报名管理]报名信息：" + model.UserName);
            //Coolite.Ext.Web.Ext.MessageBox.Alert("提示", "留言回复成功").Show();
            Page.RegisterClientScriptBlock("back", "<script type=\"text/javascript\">alert('保存成功！');window.location.href='List.aspx';</script>");
        }
        #endregion

        protected void btnretset_Click(object sender, EventArgs e)
        {
            Response.Redirect("List.aspx");
        }
    }
}