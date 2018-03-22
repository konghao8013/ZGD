using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZGD.Common;

namespace ZGD.Web.Admin.House
{
    public partial class Edit : ZGD.BasePage.ManagePage
    {
        public int Id;
        string defaultCon = "<table cellpadding='0' cellspacing='0' border='0'>"
                           + "<tr>"
                           + "<th width='80' align='right'>楼盘地址：</th><td width='500'></td>"
                           + "</tr>"
                           + "<tr>"
                           + "<th align='right'>物管费：</th><td></td>"
                           + "</tr>"
                           + "<tr>"
                           + "<th align='right'>主力户型：</th><td></td>"
                           + "</tr>"
                           + "<tr>"
                           + "<th align='right'>周边配套：</th><td></td>"
                           + "</tr>"
                           + "<tr>"
                           + "<th align='right'>交通配套：</th><td></td>"
                           + "</tr>"
                           + "<tr>"
                           + "<th align='right'>教育配套：</th><td></td>"
                           + "</tr>"
                           + "<tr>"
                           + "<th align='right'>医院配套：</th><td></td>"
                           + "</tr>"
                           + "</table>";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!int.TryParse(Request.Params["id"] as string, out this.Id))
            {
                JscriptMsg("您要修改的信息不存在或参数不正确。");
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
            ZGD.BLL.House bll = new ZGD.BLL.House();
            ZGD.Model.House model = bll.GetModel(_id);

            txtTitle.Text = model.Title;
            txtKeyword.Text = model.Keyword;
            txtAddress.Text = model.Address;
            //ddlZxType.SelectedValue = model.ZxType.ToString();
            txtDesc.Value = model.Description;
            txtImgUrl.Text = model.ImgUrl;
            if (!string.IsNullOrWhiteSpace(model.ImgUrl))
            {
                tr_img_panel.Style["display"] = "block";
                imgPanel.InnerHtml = "<img src=\"" + model.ImgUrl + "\" />";
            }
            else
            {
                imgPanel.Style["display"] = "none";
            }
            string con = ZGD.Common.StringHandler.DeCode(model.hContent);
            kEditor.Value = string.IsNullOrWhiteSpace(con) ? defaultCon : con;
            kEditor2.Value = ZGD.Common.StringHandler.DeCode(model.yhContent);
            txtClick.Text = model.Click.ToString();
            txtCaseCount.Text = model.cCount.ToString();
            txtProjectCount.Text = model.pCount.ToString();
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
            if (this.txtKeyword.Text.Trim().Length == 0)
            {
                strErr += "关键词不能为空！\\n";
            }
            if (this.txtAddress.Text.Trim().Length == 0)
            {
                strErr += "地址不能为空！\\n";
            }
            if (!PageValidate.IsNumber(txtClick.Text))
            {
                strErr += "点击次数格式错误！\\n";
            }
            if (!PageValidate.IsNumber(txtCaseCount.Text))
            {
                strErr += "点楼盘案例数格式错误！\\n";
            }
            if (!PageValidate.IsNumber(txtProjectCount.Text))
            {
                strErr += "楼盘施工户数格式错误！\\n";
            }
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            ZGD.BLL.House bll = new ZGD.BLL.House();
            ZGD.Model.House model = bll.GetModel(this.Id);
            model.Title = this.txtTitle.Text;
            model.Keyword = this.txtKeyword.Text;
            model.Address = txtAddress.Text;
            model.Description = txtDesc.Value;
            model.ImgUrl = txtImgUrl.Text.Trim();
            ////缩略图生产
            //if (!string.IsNullOrWhiteSpace(txtImgUrl.Text) && model.ImgUrl != txtImgUrl.Text.Trim())
            //{
            //    model.ImageSmall = ZGD.Common.Thumbnail.CreateThumbImg(model.ImgUrl, 300, 300, "H");
            //}
            string con = ZGD.Common.StringHandler.EnCode(Request.Form["kEditor"]);
            model.hContent = string.IsNullOrWhiteSpace(con) ? defaultCon : con;
            model.yhContent = ZGD.Common.StringHandler.EnCode(Request.Form["kEditor2"]);
            model.Click = int.Parse(this.txtClick.Text);
            model.cCount = int.Parse(this.txtCaseCount.Text);
            model.pCount = int.Parse(this.txtProjectCount.Text);
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
            if (bll.Update(model))
            {
                //保存日志
                SaveLogs("[小区模块]编辑小区：" + model.Title);
                JscriptMsg("保存成功", "List.aspx");
            }
        }
        #endregion

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("List.aspx");
        }

    }
}