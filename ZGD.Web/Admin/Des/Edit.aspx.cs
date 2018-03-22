using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZGD.Common;

namespace ZGD.Web.Admin.Des
{
    public partial class Edit : ZGD.BasePage.ManagePage
    {
        public int Id;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!int.TryParse(Request.Params["id"] as string, out this.Id))
            {
                JscriptMsg("出现错误您要修改的信息不存在或参数不正确。");
                return;
            }
            if (!Page.IsPostBack)
            {
                ShowInfo();
            }
        }

        #region 赋值操作
        private void ShowInfo()
        {
            ZGD.BLL.Designer bll = new ZGD.BLL.Designer();
            ZGD.Model.Designer model = bll.GetModel(this.Id);

            txtName.Text = model.Name;
            txtcName.Text = model.cName;
            txtCyDate.Text = model.cyDate.ToString();
            txtTitle.Value = model.Title;
            txtImgUrl.Text = model.Photo;
            txtRongyu.Text = model.Rongyu;
            txtSchool.Text = model.School;
            txtGoodAt.Text = model.GoodAt;
            txtSort.Text = model.Sort.ToString();
            txtClick.Text = model.Clicks.ToString();
            ddlStar.SelectedValue = model.Star.ToString();
            txtGoodAtDes.Text = model.GoodAtDes;
            cblItem.Checked = model.IsTop == 1 ? true : false;

            if (!string.IsNullOrWhiteSpace(model.Photo))
            {
                imgPanel.InnerHtml = "<img src=\"" + model.Photo + "\" />";
            }
            else
            {
                imgPanel.Style["display"] = "none";
            }

            ////绑定相册
            //ZGD.BLL.ProjectImg pBll = new ZGD.BLL.ProjectImg();
            //DataSet ds = pBll.GetList(" DesignerId=" + model.ID + " and Type=2");
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
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                strErr += "设计师名称为空！\\n";
            }
            if (string.IsNullOrWhiteSpace(txtcName.Text))
            {
                strErr += "设计师职称为空！\\n";
            }
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }

            ZGD.BLL.Designer bll = new ZGD.BLL.Designer();
            ZGD.Model.Designer model = bll.GetModel(this.Id);
            model.Name = txtName.Text;
            model.cName = txtcName.Text;
            model.Title = txtTitle.Value;
            model.Rongyu = txtRongyu.Text;
            model.School = txtSchool.Text;
            model.GoodAtDes = txtGoodAtDes.Text;
            model.Star = Convert.ToInt32(ddlStar.SelectedValue);
            model.Sort = Convert.ToInt32(txtSort.Text);
            model.cyDate = string.IsNullOrWhiteSpace(txtCyDate.Text) ? 0 : Convert.ToInt32(txtCyDate.Text);
            model.GoodAt = txtGoodAt.Text;
            model.Photo = txtImgUrl.Text.Trim();
            model.IsTop = cblItem.Checked ? 1 : 0;
            model.Clicks = string.IsNullOrWhiteSpace(txtClick.Text) ? 0 : Convert.ToInt32(txtClick.Text);

            if (bll.Update(model))
            {
                ////保存相册
                //ZGD.BLL.ProjectImg pBll = new ZGD.BLL.ProjectImg();
                //pBll.DeleteByDesignerId(model.ID);
                //ZGD.Model.ProjectImg pModel = null;

                ////户型图
                //string[] img_list = hidImg.Value.TrimEnd(',').Split(',');
                //string[] title_list = hidTitle.Value.TrimEnd(',').Split(',');
                //int idx = 0;
                //foreach (string item in img_list)
                //{
                //    if (!string.IsNullOrEmpty(item))
                //    {
                //        pModel = new ZGD.Model.ProjectImg();
                //        pModel.Title = title_list[idx];
                //        pModel.ImgUrl = item;
                //        //缩略图生产
                //        if (!string.IsNullOrWhiteSpace(item))
                //        {
                //            pModel.ImageSmall = ZGD.Common.Thumbnail.CreateThumbImg(item, 300, 300, "H");
                //        }
                //        pModel.DesignerId = this.Id;
                //        pModel.pID = 0;
                //        pModel.Type = 2;
                //        pModel.PubTime = DateTime.Now;
                //        pBll.Add(pModel);
                //        idx++;
                //    }
                //}

                //保存日志
                SaveLogs("[设计师模块]编辑设计师：" + model.Title);
                JscriptMsg("保存成功", "List.aspx");
            }
            else
            {
                JscriptMsg("发布过程中发生错误");
            }
        }
        #endregion

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("List.aspx");
        }

    }
}