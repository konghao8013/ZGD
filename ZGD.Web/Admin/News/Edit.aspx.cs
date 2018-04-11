﻿using System;
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
                ChannelTreeBind_Check(8, 1, this.ddlClassId);
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
            txtSubTitle.Text = model.SubTitle;
            txtKeyword.Text = model.Keyword;
            txtTags.Text = model.Tags;
            txtDesc.Value = model.Description;
            txtAuthor.Text = model.Author;
            BindChecked(ddlClassId, model.ClassId);
            txtImgUrl.Text = model.ImgUrl;

            if (!string.IsNullOrWhiteSpace(model.ImgUrl) && model.IsImage == 1)
            {
                string[] img = model.ImgUrl.Split(',');
                tr_img_panel.Style["display"] = "block";
                imgPanel.InnerHtml = "<img src=\"" + img[0] + "\" />";
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

            ZGD.BLL.NewsInfo bll = new ZGD.BLL.NewsInfo();
            ZGD.Model.NewsInfo model = bll.GetModel(this.Id);

            model.Title = txtTitle.Text.Trim();
            model.SubTitle = this.txtSubTitle.Text;
            model.Keyword = txtKeyword.Text.Trim();
            model.Tags = txtTags.Text.Trim();
            model.Description = txtDesc.Value;
            model.Author = txtAuthor.Text.Trim();
            model.ClassId = GetChecked(ddlClassId);
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