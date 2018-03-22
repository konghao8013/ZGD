using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZGD.Web.Admin.Feedback
{
    public partial class ComplainEdit : ZGD.BasePage.ManagePage
    {
        public int Id;
        public string tName = "";
        public ZGD.Model.Complain model = new ZGD.Model.Complain();

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
            ZGD.BLL.Complain bll = new ZGD.BLL.Complain();
            model = bll.GetModel(_id);
            txtName.Text = model.uName;
            txtPhone.Text = model.Phone;
            txtPubTime.Text = model.PubTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
            txtArea.Text = model.Area;
            ddlType.SelectedValue = model.Type.Value.ToString();
            txtCon.Text = model.mCon;
        }
        #endregion

        protected void btnretset_Click(object sender, EventArgs e)
        {
            Response.Redirect("complain.aspx");
        }
    }
}