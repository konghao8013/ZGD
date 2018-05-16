using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZGD.Common;

namespace ZGD.Web.Admin.Project
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
                ChannelTreeBind(52, "请选择图册分类", 5, this.ddlClassId);
                //赋值
                ShowInfo(this.Id);
            }
        }

        #region 赋值操作
        private void ShowInfo(int _id)
        {
            ZGD.BLL.Channel channel = new ZGD.BLL.Channel();
            ZGD.BLL.Project bll = new ZGD.BLL.Project();
            ZGD.Model.Project model = bll.GetModel(_id);

            txtTitle.Text = model.Title;
            txtKeyword.Text = model.Keyword;
            txtDescription.Text = model.Description;
            txtAuthor.Text = model.Author;
            ddlClassId.SelectedValue = model.ClassId.ToString();
            txtPubTime.Text = model.PubTime.ToString("yyyy-MM-dd");

            txtImgUrl.Text = model.ImgUrl;
            //if (!string.IsNullOrWhiteSpace(model.ImageSmall))
            //{
            //    imgPanel.InnerHtml = "<img src=\"" + model.ImageSmall + "\" />";
            //}
            //else
            //{
            //    imgPanel.Style["display"] = "none";
            //}
            txtClick.Text = model.Click.ToString();
            cblItem.Items[0].Selected = model.IsTop == 1 ? true : false;
            //cblItem.Items[1].Selected = model.IsLock == 1 ? true : false;

            ZGD.BLL.ProjectImg pBll = new ZGD.BLL.ProjectImg();

            //绑定户型图
            DataSet ds = pBll.GetList(" pID=" + model.Id);
            string imgList = string.Empty, titleList = string.Empty;
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    imgList += dr["ImgUrl"].ToString() + ",";
                    titleList += dr["Title"].ToString() + ",";
                }
            }
            if (!string.IsNullOrEmpty(imgList))
            {
                hidImg.Value = imgList;
                hidTitle.Value = titleList;
            }

            if (!string.IsNullOrEmpty(imgList))
                Page.RegisterClientScriptBlock("imgedit1", "<script>$(function(){setImg('','',1);});</script>");
        }
        #endregion

        #region 修改操作
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

            ZGD.BLL.Project bll = new ZGD.BLL.Project();
            ZGD.Model.Project model = bll.GetModel(this.Id);

            model.Title = this.txtTitle.Text;
            model.Keyword = this.txtKeyword.Text;
            model.Description = this.txtDescription.Text;
            model.Author = this.txtAuthor.Text;
            model.ClassId = Convert.ToInt32(ddlClassId.SelectedValue);
            model.PubTime = string.IsNullOrWhiteSpace(txtPubTime.Text) ? DateTime.Now : Convert.ToDateTime(txtPubTime.Text);
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

            if (bll.Update(model))
            {
                //保存户型图、项目图
                ZGD.BLL.ProjectImg pBll = new ZGD.BLL.ProjectImg();
                pBll.Delete(model.Id);
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
                        if (idx == 0 && item != model.ImgUrl)
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

                        pModel.pID = this.Id;
                        pModel.PubTime = DateTime.Now;
                        pBll.Add(pModel);
                        idx++;
                    }
                }
            }

            //保存日志
            SaveLogs("[案例模块]编辑案例：" + model.Title);
            JscriptMsg("保存成功", "List.aspx");
        }
        #endregion


        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("List.aspx");
        }

    }
}