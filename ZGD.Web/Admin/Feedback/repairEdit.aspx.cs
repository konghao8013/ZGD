using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZGD.Web.Admin.Feedback
{
    public partial class repairEdit : ZGD.BasePage.ManagePage
    {
        public int Id;
        public string tName = "";
        public ZGD.Model.Repair model = new ZGD.Model.Repair();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!int.TryParse(Request.Params["id"] as string, out this.Id))
            {
                JscriptMsg("您要回复的信息不存在或参数不正确。");
                return;
            }
            if (!Page.IsPostBack)
            {
                //赋值
                ShowInfo(this.Id);
            }
        }

        #region 赋值操作
        private void ShowInfo(int _id)
        {
            ZGD.BLL.Repair bll = new ZGD.BLL.Repair();
            model = bll.GetModel(_id);
            txtName.Text = model.uName + "(" + model.Sex + ")";
            txtPhone.Text = model.Phone;
            txtPubTime.Text = model.PubTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
            txtRapairTime.Text = model.WorkDate;
            txtAddress.Text = model.Address;
            txtCon.Text = model.Remark;
            txtManager.Text = model.Manager;
            txtDeser.Text = model.Des;
        }
        #endregion

        protected void btnretset_Click(object sender, EventArgs e)
        {
            Response.Redirect("complain.aspx");
        }
    }
}