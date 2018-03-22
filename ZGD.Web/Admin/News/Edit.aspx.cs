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
                //绑定类别
                ChannelTreeBind(1, "新闻类型", 0, this.ddlClassId);
                //赋值
                ShowInfo(this.Id);
            }
        }

        #region 赋值操作
        private void ShowInfo(int _id)
        {
            ZGD.BLL.NewsInfo bll = new ZGD.BLL.NewsInfo();
            ZGD.Model.NewsInfo model = bll.GetModel(_id);

            cbIsImage.Checked = model.IsImage == 1 ? true : false;
            txtTitle.Text = model.Title;
            txtKeyword.Text = model.Keyword;
            txtTags.Text = model.Tags;
            //ddlZxType.SelectedValue = model.ZxType.ToString();
            txtDesc.Value = model.Description;
            txtAuthor.Text = model.Author;
            ddlClassId.SelectedValue = model.ClassId.ToString();
            txtImgUrl.Text = model.ImgUrl;
            if (!string.IsNullOrWhiteSpace(model.ImgUrl)&&model.IsImage==1)
            {
                tr_img_panel.Style["display"] = "block";
                imgPanel.InnerHtml = "<img src=\"" + model.ImgUrl + "\" />";
            }
            else
            {
                imgPanel.Style["display"] = "none";
            }
            kEditor.Value = ZGD.Common.StringHandler.DeCode(model.Content);
            txtClick.Text = model.Click.ToString();
            if (model.IsLock == 1)
            {
                cblItem.Items[0].Selected = true;
            }
            if (model.IsTop == 1)
            {
                cblItem.Items[1].Selected = true;
            }
        }
        #endregion

        #region 修改操作
        protected void btnSave_Click(object sender, EventArgs e)
        {
            ZGD.BLL.NewsInfo bll = new ZGD.BLL.NewsInfo();
            ZGD.Model.NewsInfo model = bll.GetModel(this.Id);

            model.Title = txtTitle.Text.Trim();
            model.Keyword = txtKeyword.Text.Trim();
            model.Tags = txtTags.Text.Trim();
            model.Description = txtDesc.Value;
            model.Author = txtAuthor.Text.Trim();
            model.ClassId = ddlClassId.SelectedIndex > 0 ? int.Parse(ddlClassId.SelectedValue) : 0;
            model.ZxType = 1;
            model.ImgUrl = txtImgUrl.Text.Trim();
            model.Content = ZGD.Common.StringHandler.EnCode(Request["kEditor"].ToString());
            model.Click = int.Parse(txtClick.Text.Trim());
            model.IsImage = cbIsImage.Checked ? 1 : 0;

            model.IsLock = 0;
            model.IsTop = 0;
            if (cblItem.Items[0].Selected == true)
            {
                model.IsLock = 1;
            }
            if (cblItem.Items[1].Selected == true)
            {
                model.IsTop = 1;
            }
            bll.Update(model);

            //保存日志
            SaveLogs("[新闻模块]编辑新闻：" + model.Title);
            JscriptMsg("保存成功", "List.aspx");
        }
        #endregion

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("List.aspx");
        }

    }
}