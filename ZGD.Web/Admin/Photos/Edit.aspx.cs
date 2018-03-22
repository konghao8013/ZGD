using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZGD.Common;

namespace ZGD.Web.Admin.Photos
{
    public partial class Edit : ZGD.BasePage.ManagePage
    {
        public int Id;
        public string title = "相册";
        public int pType = 0;
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
            ZGD.BLL.Photos bll = new ZGD.BLL.Photos();
            ZGD.Model.Photos model = bll.GetModel(_id);

            pType = model.Type;
            switch (model.Type)
            {
                case 1:
                    title = "工艺";
                    selType.Style["display"] = "block";
                    break;
                case 2:
                    title = "体系";
                    selType.Style["display"] = "none";
                    break;
                case 3:
                    title = "材料";
                    selType.Style["display"] = "none";
                    break;
            }

            ddlType.SelectedValue = model.pType.ToString();
            txtTitle.Text = model.PhotoName;
            txtDescription.Text = model.PhotoCon;
            txtImgUrl.Text = model.ImgUrl;
            if (!string.IsNullOrWhiteSpace(model.ImgUrl))
            {
                imgPanel.InnerHtml = "<img src=\"" + model.ImgUrl + "\" />";
            }
            else
            {
                imgPanel.Style["display"] = "none";
            }
            txtSort.Text = model.Sort.ToString();
            cblItem.Checked = model.IsTop == 1 ? true : false;

            //ZGD.BLL.ProjectImg pBll = new ZGD.BLL.ProjectImg();

            ////绑定户型图
            //DataSet ds = pBll.GetList(" pID=" + model.ID + " and Type=3");
            //string imgList = string.Empty, titleList = string.Empty;
            //if (ds != null && ds.Tables[0].Rows.Count > 0)
            //{
            //    foreach (DataRow dr in ds.Tables[0].Rows)
            //    {
            //        imgList += dr["ImgUrl"].ToString() + ",";
            //        titleList += dr["Title"].ToString() + ",";
            //    }
            //}
            //if (!string.IsNullOrEmpty(imgList))
            //{
            //    hidImg.Value = imgList;
            //    hidTitle.Value = titleList;
            //}

            //if (!string.IsNullOrEmpty(imgList))
            //    Page.RegisterClientScriptBlock("imgedit1", "<script>$(function(){setImg('','',1);});</script>");
        }
        #endregion

        #region 修改操作
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string strErr = "";

            ZGD.BLL.Photos bll = new ZGD.BLL.Photos();
            ZGD.Model.Photos model = bll.GetModel(this.Id);
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                strErr += "标题不能为空！\\n";
            }
            if (string.IsNullOrWhiteSpace(txtImgUrl.Text))
            {
                strErr += "请上传图片！\\n";
            }
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            model.pType = Convert.ToInt32(ddlType.SelectedValue);
            model.PhotoName = this.txtTitle.Text;
            model.PhotoCon = txtDescription.Text;
            model.ImgUrl = txtImgUrl.Text.Trim();
            model.Sort = string.IsNullOrEmpty(txtSort.Text) ? 0 : int.Parse(this.txtSort.Text);
            model.IsTop = cblItem.Checked ? 1 : 0;

            if (bll.Update(model))
            {
                //    //保存户型图、项目图
                //    ZGD.BLL.ProjectImg pBll = new ZGD.BLL.ProjectImg();
                //    pBll.Delete(model.ID, 3);
                //    ZGD.Model.ProjectImg pModel = null;

                //    //户型图
                //    string[] img_list = hidImg.Value.TrimEnd(',').Split(',');
                //    string[] title_list = hidTitle.Value.TrimEnd(',').Split(',');
                //    int idx = 0;
                //    foreach (string item in img_list)
                //    {
                //        if (!string.IsNullOrEmpty(item))
                //        {
                //            pModel = new ZGD.Model.ProjectImg();
                //            pModel.Title = title_list[idx];
                //            pModel.ImgUrl = item;
                //            //缩略图生产
                //            if (!string.IsNullOrWhiteSpace(item))
                //            {
                //                pModel.ImageSmall = ZGD.Common.Thumbnail.CreateThumbImg(item, 60, 40, "W");
                //            }
                //            pModel.DesignerId = 0;
                //            pModel.pID = this.Id;
                //            pModel.Type = 3;
                //            pModel.PubTime = DateTime.Now;
                //            pBll.Add(pModel);
                //            idx++;
                //        }
                //    }
                //保存日志
                SaveLogs("[相册模块]编辑相册：" + model.PhotoName);
                JscriptMsg("保存成功", "List.aspx?t=" + model.Type);
            }

        }
        #endregion


        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("List.aspx?t=" + pType);
        }

    }
}