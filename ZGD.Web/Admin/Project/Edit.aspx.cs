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
                //绑定设计师
                new ZGD.BLL.House().BindHouse(this.ddlHouse);
                //绑定设计师
                new ZGD.BLL.Designer().BindDesigner(this.ddlDes);
                //绑定类别
                ChannelTreeBind(11, "所有风格", 3, this.ddlFG);
                ChannelTreeBind(15, "所有户型", 4, this.ddlHX);
                ChannelTreeBind(19, "所有价格", 5, this.ddlJG);
                ChannelTreeBind(62, "所有面积", 8, this.ddlMJ);
                ChannelTreeBind(77, "所有功能间", 10, this.ddlGN);
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
            ddlDes.SelectedValue = model.DesignerId.ToString();
            ddlHouse.SelectedValue = model.HouseId.ToString();
            //txtDes.Text = model.DesignerName;
            ddlFG.SelectedValue = model.fgID.ToString();
            ddlHX.SelectedValue = model.hxID.ToString();
            ddlGN.SelectedValue = model.gnID.ToString();
            ddlMJ.SelectedValue = model.mjID.ToString();
            ddlJG.SelectedValue = model.jgID.ToString();
            txtPrice.Text = model.Price.ToString();
            txtArea.Text = model.Area.ToString();
            //txtTDFiles.Text = model.TDFiles;
            txtImgUrl.Text = model.ImgUrl;
            if (!string.IsNullOrWhiteSpace(model.ImageSmall))
            {
                imgPanel.InnerHtml = "<img src=\"" + model.ImageSmall + "\" />";
            }
            else
            {
                imgPanel.Style["display"] = "none";
            }
            txtRemark.Value = model.Remark;
            txtClick.Text = model.Click.ToString();
            cblItem.Items[0].Selected = model.IsTop == 1 ? true : false;
            cblItem.Items[1].Selected = model.IsLock == 1 ? true : false;

            ZGD.BLL.ProjectImg pBll = new ZGD.BLL.ProjectImg();

            //绑定户型图
            DataSet ds = pBll.GetList(" pID=" + model.Id + " and Type=1");
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
                strErr += "案例标题不能为空！\\n";
            }
            if (ddlDes.SelectedIndex == 0)
            {
                strErr += "请选择设计师！\\n";
            }
            if (ddlFG.SelectedIndex == 0)
            {
                strErr += "请选择案例风格！\\n";
            }
            if (ddlHX.SelectedIndex == 0)
            {
                strErr += "请选择案例户型！\\n";
            }
            if (ddlGN.SelectedIndex == 0)
            {
                strErr += "请选择案例功能间！\\n";
            }
            //if (ddlMJ.SelectedIndex == 0)
            //{
            //    strErr += "请选择面积范围！\\n";
            //}
            //if (string.IsNullOrWhiteSpace(txtArea.Text))
            //{
            //    strErr += "请输入具体面积！\\n";
            //}
            if (ddlJG.SelectedIndex == 0)
            {
                strErr += "请选择价格范围！\\n";
            }
            if (string.IsNullOrWhiteSpace(txtPrice.Text))
            {
                strErr += "请输入具体价格！\\n";
            }
            if (string.IsNullOrWhiteSpace(txtImgUrl.Text))
            {
                strErr += "请上传案例LOGO！\\n";
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
            model.DesignerId = Convert.ToInt32(ddlDes.SelectedValue);
            model.HouseId = ddlHouse.SelectedIndex != 0 ? Convert.ToInt32(ddlHouse.SelectedValue) : 0;
            model.hxID = Convert.ToInt32(ddlHX.SelectedValue);
            model.gnID = Convert.ToInt32(ddlGN.SelectedValue);
            model.fgID = Convert.ToInt32(ddlFG.SelectedValue);
            model.mjID = string.IsNullOrWhiteSpace(ddlMJ.SelectedValue) ? 0 : Convert.ToInt32(ddlMJ.SelectedValue);
            model.Area = string.IsNullOrWhiteSpace(txtArea.Text) ? 0 : Convert.ToDecimal(txtArea.Text);
            model.jgID = Convert.ToInt32(ddlJG.SelectedValue);
            model.Price = Convert.ToDecimal(txtPrice.Text);
            model.ImgUrl = txtImgUrl.Text.Trim();
            //缩略图生产
            if (!string.IsNullOrWhiteSpace(txtImgUrl.Text) && model.ImgUrl != txtImgUrl.Text.Trim())
            {
                model.ImageSmall = ZGD.Common.Thumbnail.CreateThumbImg(model.ImgUrl, 300, 300, "H");
            }
            model.Click = string.IsNullOrEmpty(txtClick.Text) ? 0 : int.Parse(this.txtClick.Text);
            model.Remark = txtRemark.Value;
            model.PubTime = DateTime.Now;
            model.IsTop = 0;
            model.IsLock = 0;
            if (cblItem.Items[0].Selected == true)
            {
                model.IsTop = 1;
            }
            if (cblItem.Items[1].Selected == true)
            {
                model.IsLock = 1;
            }

            if (bll.Update(model))
            {
                //保存户型图、项目图
                ZGD.BLL.ProjectImg pBll = new ZGD.BLL.ProjectImg();
                pBll.Delete(model.Id, 1);
                ZGD.Model.ProjectImg pModel = null;

                //户型图
                string[] img_list = hidImg.Value.TrimEnd(',').Split(',');
                string[] title_list = hidTitle.Value.TrimEnd(',').Split(',');
                int idx = 0;
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
                        pModel.DesignerId = model.DesignerId;
                        pModel.pID = this.Id;
                        pModel.Type = 1;
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