
using ZGD.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZGD.Web.Admin.Activity
{
    public partial class Add : ZGD.BasePage.ManagePage
    {
        public int Id;
        public string tName = "";

        protected void Page_Load(object sender, EventArgs e)
        {
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

            ZGD.Model.Activity model = new ZGD.Model.Activity();
            model.Title = this.txtTitle.Text;
            model.Keyword = this.txtKeyword.Text;
            model.Description = this.txtDesc.Value;
            model.sDate = Convert.ToDateTime(txtsDate.Text).ToString("yyyy-MM-dd HH:mm");
            model.eDate = Convert.ToDateTime(txteDate.Text).ToString("yyyy-MM-dd HH:mm");
            model.aCon = ZGD.Common.StringHandler.EnCode(Request.Form["kEditor"]);
            model.ImgUrl = txtImgUrl.Text;
            model.aUrl = txtActivityUrl.Text;
            model.PubTime = DateTime.Now;
            model.Status = 0;
            model.Click = string.IsNullOrWhiteSpace(txtClick.Text) ? 0 : Convert.ToInt32(txtClick.Text);

            ZGD.BLL.Activity bll = new ZGD.BLL.Activity();
            int ReId = bll.Add(model);
            if (ReId > 0)
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
            txtTitle.Text = "";
            txtKeyword.Text = "";
            txteDate.Text = "";
            txtsDate.Text = "";
            txtImgUrl.Text = "";
            txtDesc.Value = "";
            txtActivityUrl.Text = "";
            kEditor.Value = "";
            txtClick.Text = "0";
        }
    }
}