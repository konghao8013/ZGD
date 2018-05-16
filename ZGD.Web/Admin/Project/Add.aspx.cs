using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZGD.Common;
using System.IO;

namespace ZGD.Web.Admin.Project
{
    public partial class Add : ZGD.BasePage.ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //绑定类别
                ChannelTreeBind(52, "请选择图册分类", 5, this.ddlClassId);
                txtPubTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        #region 添加
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string strErr = "";
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                strErr += "图册标题不能为空！\\n";
            }
            if (ddlClassId.SelectedIndex == 0)
            {
                strErr += "请选择图册分类！\\n";
            }
            if (string.IsNullOrWhiteSpace(txtImgUrl.Text))
            {
                strErr += "请上传图册LOGO！\\n";
            }
            if (string.IsNullOrWhiteSpace(hidImg.Value))
            {
                strErr += "请上传图册图片！\\n";
            }
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }

            ZGD.Model.Project model = new ZGD.Model.Project();

            model.Title = this.txtTitle.Text;
            model.Keyword = this.txtKeyword.Text;
            model.Description = this.txtDescription.Text;
            model.Author = this.txtAuthor.Text;
            model.ClassId = Convert.ToInt32(ddlClassId.SelectedValue);
            model.ImgUrl = txtImgUrl.Text.Trim();
            model.PubTime = string.IsNullOrWhiteSpace(txtPubTime.Text) ? DateTime.Now : Convert.ToDateTime(txtPubTime.Text);
            model.UserId = Convert.ToInt32(Session["AdminNo"]);
            model.Click = string.IsNullOrEmpty(txtClick.Text) ? 0 : int.Parse(this.txtClick.Text);
            model.PubTime = DateTime.Now;
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

            ZGD.BLL.Project bll = new ZGD.BLL.Project();
            int ReId = bll.Add(model);
            if (ReId > 0)
            {
                //保存户型图、项目图
                ZGD.BLL.ProjectImg pBll = new ZGD.BLL.ProjectImg();
                ZGD.Model.ProjectImg pModel = null;

                //户型图
                string[] img_list = hidImg.Value.TrimEnd(',').Split(',');
                string[] title_list = hidTitle.Value.TrimEnd(',').Split(',');
                int idx = 0;
                foreach (string item in img_list)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        //缩略图生产
                        if (idx == 0)
                        {
                            model.ImageSmall = ZGD.Common.Thumbnail.CreateThumbImg(item, 440, 300, "H");
                            model.ImgUrl = ZGD.Common.Thumbnail.CreateThumbImg(item, 600, 420, "H");
                            bll.UpdateField(model.Id, " ImgUrl='" + model.ImgUrl + "',ImageSmall='" + model.ImageSmall + "'");
                        }

                        pModel = new ZGD.Model.ProjectImg();
                        pModel.Title = title_list[idx];
                        pModel.Description = title_list[idx];
                        //缩略图生产
                        if (!string.IsNullOrWhiteSpace(item))
                        {
                            pModel.ImgUrl = ZGD.Common.Thumbnail.CreateThumbImg(item, 800, 600, "W");
                            pModel.ImageSmall = ZGD.Common.Thumbnail.CreateThumbImg(item, 600, 410, "W");
                        }
                        pModel.pID = ReId;
                        pModel.PubTime = DateTime.Now;
                        pBll.Add(pModel);
                        idx++;
                    }
                }

                //保存日志
                SaveLogs("[图册模块]添加图册：" + model.Title);
                Clear();
                JscriptMsg("图册发布成功");
            }
            else
            {
                JscriptMsg("发布过程中发生错误");
            }
        }
        #endregion

        void Clear()
        {
            hidImg.Value = "";
            hidTitle.Value = "";
            txtClick.Text = "0";
            txtDescription.Text = "";
            txtImgUrl.Text = "";
            txtKeyword.Text = "";
            txtTitle.Text = "";
            txtAuthor.Text = "";
            ddlClassId.SelectedIndex = 0;
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Clear();
        }

    }
}