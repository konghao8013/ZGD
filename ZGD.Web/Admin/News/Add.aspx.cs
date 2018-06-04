using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZGD.Common;
using System.Globalization;

namespace ZGD.Web.Admin.News
{
    public partial class Add : ZGD.BasePage.ManagePage
    {
        public string url = "List.aspx", size = "图片尺寸400*280px";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Request.Params["zt"]) && Convert.ToInt32(Request.Params["zt"]) > 0)
            {
                url = "../Zt/List.aspx";
                size = "图片尺寸400*100px";
            }

            if (!Page.IsPostBack)
            {
                //绑定类别
                ChannelTreeBind_Check(8, 1, this.ddlClassId);
                ChannelTreeBind(21, "请选择专题", 2, this.ddlZtId);
                txtPubTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
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
            model.PubTime = string.IsNullOrWhiteSpace(txtPubTime.Text) ? DateTime.Now : Convert.ToDateTime(txtPubTime.Text);

            string html = Request.Form["kEditor"];
            model.Content = ZGD.Common.StringHandler.EnCode(html);

            if (ddlZtId.SelectedIndex > 0)
            {
                model.ClassId = string.IsNullOrWhiteSpace(model.ClassId) ? ddlZtId.SelectedValue : model.ClassId + "," + ddlZtId.SelectedValue;
            }

            model.UserId = Convert.ToInt32(Session["AdminNo"]);

            model.IsImage = 0;
            if (cbIsImage.Checked == true)
            {
                model.IsImage = 1;
            }

            //获取文章内容图片集合
            var imgs = RegexHandler.GetImgList(html);

            //缩略图生产
            if (imgs != null && imgs.Count > 0)
            {
                //存储网络图片
                string f_img = imgs[0];

                if (f_img.IndexOf("http://", StringComparison.CurrentCultureIgnoreCase) > -1 || f_img.IndexOf("https://", StringComparison.CurrentCultureIgnoreCase) > -1)
                {
                    string newFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo) + ".jpg";
                    string path = "/UpLoadFiles/HttpTmp/" + newFileName;
                    HttpHelper.SaveHttpImg(f_img, Server.MapPath(path));
                    f_img = path;
                }

                if (Convert.ToInt32(Request.Params["zt"]) > 0)
                {
                    model.ImgUrl = ZGD.Common.Thumbnail.CreateThumbImg(f_img, 400, 100, "H");
                }
                else
                {
                    model.ImgUrl = ZGD.Common.Thumbnail.CreateThumbImg(f_img, 440, 300, "H");
                }
            }

            model.Click = int.Parse(this.txtClick.Text);
            model.IsTop = 0;
            model.IsLock = 0;
            //if (cblItem.Items[0].Selected == true)
            //{
            //    model.IsLock = 1;
            //}
            if (cblItem.Items[0].Selected == true)
            {
                model.IsTop = 1;
            }


            ZGD.BLL.NewsInfo bll = new ZGD.BLL.NewsInfo();
            int ReId = bll.Add(model);
            if (ReId > 0)
            {
                //保存版块
                string[] cid = model.ClassId.Split(',');
                foreach (string item in cid)
                {
                    bll.AddNewsColumns(new Model.NewsColumns
                    {
                        NewsId = ReId,
                        ClassId = Convert.ToInt32(item),
                        PubTime = DateTime.Now
                    });
                }

                //保存日志
                SaveLogs("[文章模块]添加文章：" + model.Title);
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
            ddlZtId.SelectedIndex = 0;
        }

    }
}