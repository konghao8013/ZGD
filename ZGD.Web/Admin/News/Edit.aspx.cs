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
    public partial class Edit : ZGD.BasePage.ManagePage
    {
        public int Id;
        public string url = "List.aspx", size = "图片尺寸400*280px";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!int.TryParse(Request.Params["id"] as string, out this.Id))
            {
                JscriptMsg("您要修改的信息不存在或参数不正确。");
                return;
            }
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
            txtImgUrl.Text = model.ImgUrl;
            txtPubTime.Text = model.PubTime.ToString("yyyy-MM-dd");
            BindChecked(ddlClassId, model.ClassId);
            BindZt(ddlZtId, model.ClassId);

            if (ddlZtId.SelectedIndex > 0)
            {
                size = "图片尺寸400*100px";
            }

            if (!string.IsNullOrWhiteSpace(model.Files))
            {
                fileDiv.Style["display"] = "block";
                fileDiv.InnerHtml = "<a href=\"" + model.Files + "\" target=\"_blank\" />" + model.Files + "</a>";
            }
            else
            {
                fileDiv.Style["display"] = "none";
            }

            //if (!string.IsNullOrWhiteSpace(model.ImgUrl) && model.IsImage == 1)
            //{
            //    string[] img = model.ImgUrl.Split(',');
            //    tr_img_panel.Style["display"] = "block";
            //    imgPanel.InnerHtml = "<img src=\"" + img[0] + "\" />";
            //}
            //else
            //{
            //    imgPanel.Style["display"] = "none";
            //}

            kEditor.Value = ZGD.Common.StringHandler.DeCode(model.Content);
            txtClick.Text = model.Click.ToString();
            //if (model.IsLock == 1)
            //{
            //    cblItem.Items[0].Selected = true;
            //}
            if (model.IsTop == 1)
            {
                cblItem.Items[0].Selected = true;
            }
        }
        #endregion

        #region 修改操作
        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveData(1);
        }

        void SaveData(int isPub)
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
            model.PubTime = string.IsNullOrWhiteSpace(txtPubTime.Text) ? DateTime.Now : Convert.ToDateTime(txtPubTime.Text);

            if (Request.Files != null && Request.Files[0] != null && Request.Files[0].ContentLength > 0)
            {
                var postFiles = Request.Files[0];
                string strfile = postFiles.FileName;
                int filepos = strfile.LastIndexOf(".");//获取后缀名
                string strfileName = strfile.Substring(filepos);//截取后缀名

                BLL.siteconfig siteBll = new BLL.siteconfig();
                Model.SiteConfig siteModel = siteBll.loadConfig();

                string[] hz = siteModel.fileextension.Split(',');
                if (!hz.Contains(strfileName.TrimStart('.')))
                {
                    JscriptMsg("不支持该文件格式的上传！");
                    return;
                }

                string folder = "/UpLoadFiles/" + DateTime.Now.ToString("yyyyMM");
                FileDom.CreateFolder("~" + folder);
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmssffff");//获取时间
                string fileSavePath = folder + "/" + fileName + strfileName;
                postFiles.SaveAs(Server.MapPath("~" + fileSavePath));

                model.Files = fileSavePath;
            }

            string html = Request.Form["kEditor"];
            model.Content = ZGD.Common.StringHandler.EnCode(html);

            if (ddlZtId.SelectedIndex > 0)
            {
                model.ClassId = string.IsNullOrWhiteSpace(model.ClassId) ? ddlZtId.SelectedValue : model.ClassId + "," + ddlZtId.SelectedValue;
            }

            //获取文章内容图片集合
            var imgs = RegexHandler.GetImgList(html);
            //缩略图生产
            if (imgs != null && imgs.Count > 0 && model.ImgUrl != imgs[0].Trim())
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

            model.Click = int.Parse(txtClick.Text.Trim());
            model.IsImage = cbIsImage.Checked ? 1 : 0;

            model.IsPub = isPub;
            model.IsLock = 0;
            model.IsTop = 0;
            //if (cblItem.Items[0].Selected == true)
            //{
            //    model.IsLock = 1;
            //}
            if (cblItem.Items[0].Selected == true)
            {
                model.IsTop = 1;
            }
            if (bll.Update(model))
            {
                //保存版块
                if (bll.DeleteNewsColumns(model.Id))
                {
                    string[] cid = model.ClassId.Split(',');
                    foreach (string item in cid)
                    {
                        bll.AddNewsColumns(new Model.NewsColumns
                        {
                            NewsId = model.Id,
                            ClassId = Convert.ToInt32(item),
                            PubTime = DateTime.Now
                        });
                    }
                }
            }

            //保存日志
            SaveLogs("[文章模块]编辑文章：" + model.Title);
            JscriptMsg("保存成功", url);
        }
        #endregion

        protected void btnReset_Click(object sender, EventArgs e)
        {
            SaveData(0);
        }

    }
}