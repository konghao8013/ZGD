using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZGD.Common;

namespace ZGD.Web.Admin.GongDi
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
                //绑定设计师
                new ZGD.BLL.House().BindHouse(this.ddlHouse);
                //绑定设计师
                new ZGD.BLL.Designer().BindDesigner(this.ddlDes);
                //绑定类别
                ChannelTreeBind(11, "所有风格", 3, this.ddlFG);
                ChannelTreeBind(15, "所有户型", 4, this.ddlHX);
                ChannelTreeBind(78, "所有进度", 11, this.ddlStatus);
                ChannelTreeBind(78, "请选择图集所属进度", 11, this.ddlJD_Img);
                ChannelTreeBind(76, "所有区域", 9, this.ddlAreaId);
                //赋值
                ShowInfo(this.Id);
            }
        }

        #region 赋值操作
        private void ShowInfo(int _id)
        {
            ZGD.BLL.NewsInfo bll = new ZGD.BLL.NewsInfo();
            ZGD.Model.NewsInfo model = bll.GetModel(_id);

            txtTitle.Text = model.Title;
            txtKeyword.Text = model.Keyword;
            txtAddress.Text = model.Address;
            txtDesc.Value = model.Description;
            txtAuthor.Text = model.Author;
            txtUserName.Text = model.UserName;
            txtArea.Text = model.Area.ToString();
            txtPrice.Text = model.Price.ToString();
            ddlHouse.SelectedValue = model.AssId.ToString();
            ddlDes.SelectedValue = model.DesignerId.ToString();
            ddlFG.SelectedValue = model.fgID.ToString();
            ddlHX.SelectedValue = model.hxID.ToString();
            ddlAreaId.SelectedValue = model.AreaId.ToString();
            ddlZxKind.SelectedValue = model.ZxKind.ToString();
            ddlStatus.SelectedValue = model.gdStatus.ToString();
            //txtTags.Text = model.Tags;
            //ddlClassId.SelectedValue = model.ClassId.ToString();
            //ddlZxType.SelectedValue = model.ZxType.ToString();
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
            txtClick.Text = model.Click.ToString();
            if (model.IsLock == 1)
            {
                cblItem.Items[0].Selected = true;
            }
            if (model.IsTop == 1)
            {
                cblItem.Items[1].Selected = true;
            }

            ZGD.BLL.ProjectImg pBll = new ZGD.BLL.ProjectImg();
            //绑定户型图
            DataSet ds = pBll.GetList(" pID=" + model.Id + " and Type=5");
            string imgList = string.Empty, statusList = string.Empty, titleList = string.Empty;
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    imgList += dr["ImgUrl"].ToString() + ",";
                    statusList += dr["DesignerId"].ToString() + ",";
                    titleList += dr["Title"].ToString() + ",";
                }
            }
            if (!string.IsNullOrEmpty(imgList))
            {
                hidImg.Value = imgList;
                hidStatus.Value = statusList;
                hidTitle.Value = titleList;
            }

            if (!string.IsNullOrEmpty(imgList))
                Page.RegisterClientScriptBlock("imgedit1", "<script>$(function(){bindImg();});</script>");
        }
        #endregion

        #region 修改操作
        protected void btnSave_Click(object sender, EventArgs e)
        {
            ZGD.BLL.NewsInfo bll = new ZGD.BLL.NewsInfo();
            ZGD.Model.NewsInfo model = bll.GetModel(this.Id);

            int nType_int = 1;
            if (!string.IsNullOrEmpty(Request.QueryString["ntype"]))
            {
                nType_int = Convert.ToInt32(Request.QueryString["ntype"]);
            }
            model.Title = txtTitle.Text.Trim();
            model.Keyword = txtKeyword.Text.Trim();
            model.Address = txtAddress.Text;
            model.Description = txtDesc.Value;
            model.Author = txtAuthor.Text.Trim();
            model.UserName = this.txtUserName.Text;
            model.AssId = ddlHouse.SelectedIndex == 0 ? 0 : Convert.ToInt32(ddlHouse.SelectedValue);
            model.DesignerId = Convert.ToInt32(ddlDes.SelectedValue);
            model.hxID = Convert.ToInt32(ddlHX.SelectedValue);
            model.fgID = Convert.ToInt32(ddlFG.SelectedValue);
            model.ZxKind = Convert.ToInt32(ddlZxKind.SelectedValue);
            model.gdStatus = Convert.ToInt32(ddlStatus.SelectedValue);
            model.AreaId = Convert.ToInt32(ddlAreaId.SelectedValue);
            model.Area = string.IsNullOrWhiteSpace(txtArea.Text) ? 0 : Convert.ToInt32(txtArea.Text);
            model.Price = string.IsNullOrWhiteSpace(txtPrice.Text) ? 0 : Convert.ToDecimal(txtPrice.Text);
            model.ZxType = 2;
            model.ImgUrl = txtImgUrl.Text.Trim();
            model.Click = int.Parse(txtClick.Text.Trim());

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
                //保存户型图、项目图
                ZGD.BLL.ProjectImg pBll = new ZGD.BLL.ProjectImg();
                pBll.Delete(model.Id, 5);
                ZGD.Model.ProjectImg pModel = null;

                //户型图
                string[] img_list = hidImg.Value.TrimEnd(',').Split(',');
                string[] status_list = hidStatus.Value.TrimEnd(',').Split(',');
                string[] title_list = hidTitle.Value.TrimEnd(',').Split(',');
                int idx = 0, img_s = 0;
                foreach (string item in img_list)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        pModel = new ZGD.Model.ProjectImg();
                        pModel.Title = title_list[idx];
                        pModel.ImgUrl = item;
                        //缩略图生产
                        if (!string.IsNullOrWhiteSpace(item))
                        {
                            pModel.ImageSmall = ZGD.Common.Thumbnail.CreateThumbImg(item, 300, 300, "H");
                        }
                        int.TryParse(status_list[idx], out img_s);
                        pModel.DesignerId = img_s;
                        pModel.pID = this.Id;
                        pModel.Type = 5;
                        pModel.PubTime = DateTime.Now;
                        pBll.Add(pModel);
                        idx++;
                    }
                }
            }

            //保存日志
            SaveLogs("[工地模块]编辑工地：" + model.Title);
            JscriptMsg("保存成功", "List.aspx");
        }
        #endregion

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("List.aspx");
        }

    }
}