using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZGD.Common;

namespace ZGD.Web.Admin.ST
{
    public partial class Add : ZGD.BasePage.ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

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

            ZGD.Model.ServiceTools model = new ZGD.Model.ServiceTools();
            ZGD.BLL.ServiceTools bll = new ZGD.BLL.ServiceTools();

            model.Title = txtTitle.Text.Trim();
            model.Num = txtNum.Text.Trim();
            model.Type = ddlClassId.SelectedIndex > 0 ? int.Parse(ddlClassId.SelectedValue) : 0;
            model.Stuats = cbStatus.Checked ? 1 : 0;
            model.PubTime = DateTime.Now;
            bll.Add(model);

            //保存日志
            SaveLogs("[客服管理]增加客服：" + model.Title);
            ddlClassId.SelectedIndex = 0;
            this.txtTitle.Text = "";
            this.txtNum.Text = "";
            cbStatus.Text = "";
            cbStatus.Checked = false;
            JscriptMsg("提示保存成功");
        }
        #endregion

        protected void Button1_Click(object sender, EventArgs e)
        {
            ddlClassId.SelectedIndex = 0;
            this.txtTitle.Text = "";
            this.txtNum.Text = "";
            cbStatus.Checked = false;
        }

    }
}