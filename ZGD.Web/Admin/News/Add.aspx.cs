using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZGD.Common;

namespace ZGD.Web.Admin.News
{
    public partial class Add : ZGD.BasePage.ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //绑定类别
                ChannelTreeBind_Check(8, 1, this.ddlClassId);
            }
        }

        #region 添加
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string strErr = "";
            if (this.txtTitle.Text.Trim().Length == 0)
            {
                strErr += "标题不能为空！\\n";
            }
            if (this.ddlClassId.SelectedIndex == -1)
            {
                strErr += "请选择所属版块！\\n";
            }
            if (this.txtAuthor.Text.Trim().Length == 0)
            {
                strErr += "发布人不能为空！\\n";
            }
            if (string.IsNullOrWhiteSpace(Request.Form["kEditor"]))
            {
                strErr += "内容不能为空！\\n";
            }
            if (!PageValidate.IsNumber(txtClick.Text))
            {
                strErr += "点击次数格式错误！\\n";
            }
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }

            ZGD.Model.NewsInfo model = new ZGD.Model.NewsInfo();
            model.Title = this.txtTitle.Text;
            model.SubTitle = this.txtSubTitle.Text;
            model.Keyword = this.txtKeyword.Text;
            model.Tags = this.txtTags.Text;
            model.Description = txtDesc.Value;
            model.Author = this.txtAuthor.Text;
            model.ClassId = GetChecked(ddlClassId);
            model.UserId = Convert.ToInt32(Session["AdminNo"]);

            model.IsImage = 0;

            if (cbIsImage.Checked == true)
            {
                model.IsImage = 1;
            }
            model.ImgUrl = txtImgUrl.Text.Trim();
            model.Content = ZGD.Common.StringHandler.EnCode(Request.Form["kEditor"]);
            model.Click = int.Parse(this.txtClick.Text);
            model.IsTop = 0;
            model.IsLock = 0;
            if (cblItem.Items[0].Selected == true)
            {
                model.IsLock = 1;
            }
            if (cblItem.Items[1].Selected == true)
            {
                model.IsTop = 1;
            }

            model.PubTime = DateTime.Now;

            ZGD.BLL.NewsInfo bll = new ZGD.BLL.NewsInfo();
            int ReId = bll.Add(model);
            if (ReId > 0)
            {
                //保存日志
                SaveLogs("[新闻模块]添加新闻：" + model.Title);
                JscriptMsg("内容设置成功", "List.aspx");
            }
            else
            {
                JscriptMsg("发布过程中发生错误");
            }
        }
        #endregion

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtClick.Text = "0";
            this.txtTitle.Text = "";
            this.txtSubTitle.Text = "";
            this.txtKeyword.Text = "";
            this.txtAuthor.Text = "";
            this.txtImgUrl.Text = "";
            this.txtDesc.Value = "";
            this.txtTags.Text = "";
            ddlClassId.SelectedIndex = -1;
        }

    }
}