using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZGD.Common;

namespace ZGD.Web.Admin.ST
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
                ShowInfo(this.Id);
            }
        }

        #region 赋值操作
        private void ShowInfo(int _id)
        {
            ZGD.BLL.ServiceTools bll = new ZGD.BLL.ServiceTools();
            ZGD.Model.ServiceTools model = bll.GetModel(_id);
            txtTitle.Text = model.Title;
            txtNum.Text = model.Num;
            ddlClassId.SelectedValue = model.Type.ToString();
            cbStatus.Checked = model.Stuats == 0 ? false : true;
        }
        #endregion

        #region 添加客服
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string strErr = "";
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                strErr += "客服标题不能为空！\\n";
            }
            if (string.IsNullOrWhiteSpace(txtNum.Text))
            {
                strErr += "客服号码不能为空！\\n";
            }
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }

            ZGD.BLL.ServiceTools bll = new ZGD.BLL.ServiceTools();
            ZGD.Model.ServiceTools model = bll.GetModel(this.Id);
            model.Title = txtTitle.Text.Trim();
            model.Num = txtNum.Text.Trim();
            model.Type = ddlClassId.SelectedIndex > 0 ? int.Parse(ddlClassId.SelectedValue) : 0;
            model.Stuats = cbStatus.Checked ? 1 : 0;
            bll.Update(model);

            //保存日志
            SaveLogs("[客服管理]编辑客服：" + model.Title);
            JscriptMsg("保存成功", "List.aspx");
        }
        #endregion

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("List.aspx");
        }

    }
}