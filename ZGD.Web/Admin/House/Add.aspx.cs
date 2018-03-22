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
    public partial class Add : ZGD.BasePage.ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                kEditor.Value = defaultCon;
            }
        }

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

        #region 添加
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

            ZGD.Model.House model = new ZGD.Model.House();
            model.Title = this.txtTitle.Text;
            model.Keyword = this.txtKeyword.Text;
            model.Address = txtAddress.Text;
            model.Description = txtDesc.Value;
            model.ImgUrl = txtImgUrl.Text.Trim();
            ////缩略图生产
            //if (!string.IsNullOrWhiteSpace(model.ImgUrl))
            //{
            //    model.ImageSmall = ZGD.Common.Thumbnail.CreateThumbImg(model.ImgUrl, 300, 300, "H");
            //}
            string con = ZGD.Common.StringHandler.EnCode(Request.Form["kEditor"]);
            model.hContent = string.IsNullOrWhiteSpace(con) ? defaultCon : con;
            model.yhContent = ZGD.Common.StringHandler.EnCode(Request.Form["kEditor2"]);
            model.Click = int.Parse(this.txtClick.Text);
            model.cCount = int.Parse(this.txtCaseCount.Text);
            model.pCount = int.Parse(this.txtProjectCount.Text);
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

            ZGD.BLL.House bll = new ZGD.BLL.House();
            int ReId = bll.Add(model);
            if (ReId > 0)
            {
                //保存日志
                SaveLogs("[小区模块]添加小区：" + model.Title);
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
            this.txtTitle.Text = "";
            this.txtKeyword.Text = "";
            txtAddress.Text = "";
            this.txtImgUrl.Text = "";
            this.txtDesc.Value = "";
            //this.txtTags.Text = "";
            //ddlZxType.SelectedIndex = 0;
        }
    }
}