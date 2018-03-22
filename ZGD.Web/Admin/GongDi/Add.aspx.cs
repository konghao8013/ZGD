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
    public partial class Add : ZGD.BasePage.ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //绑定小区
                new ZGD.BLL.House().BindHouse(this.ddlHouse);
                //绑定设计师
                new ZGD.BLL.Designer().BindDesigner(this.ddlDes);
                //绑定类别
                ChannelTreeBind(11, "所有风格", 3, this.ddlFG);
                ChannelTreeBind(15, "所有户型", 4, this.ddlHX);
                ChannelTreeBind(78, "所有进度", 11, this.ddlStatus);
                ChannelTreeBind(78, "请选择图集所属进度", 11, this.ddlJD_Img);
                ChannelTreeBind(76, "所有区域", 9, this.ddlAreaId);

                if (!string.IsNullOrEmpty(Request.QueryString["assID"]))
                {
                    ddlHouse.SelectedValue = Request.QueryString["assID"];
                }
            }
        }

        #region 添加
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string strErr = "";
            //if (ddlHouse.SelectedIndex == 0)
            //{
            //    strErr += "请选择楼盘！\\n";
            //}
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
                strErr += "工地地址不能为空！\\n";
            }
            //if (this.txtTags.Text.Trim().Length == 0)
            //{
            //    strErr += "标签不能为空！\\n";
            //}
            if (this.txtAuthor.Text.Trim().Length == 0)
            {
                strErr += "发布人不能为空！\\n";
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
            model.Keyword = this.txtKeyword.Text;
            model.Address = this.txtAddress.Text;
            model.Description = txtDesc.Value;
            model.Author = this.txtAuthor.Text;
            model.UserName = this.txtUserName.Text;
            model.ZxType = 2;
            model.IsImage = 0;
            model.AssId = ddlHouse.SelectedIndex == 0 ? 0 : Convert.ToInt32(ddlHouse.SelectedValue);
            model.DesignerId = Convert.ToInt32(ddlDes.SelectedValue);
            model.hxID = Convert.ToInt32(ddlHX.SelectedValue);
            model.fgID = Convert.ToInt32(ddlFG.SelectedValue);
            model.AreaId = Convert.ToInt32(ddlAreaId.SelectedValue);
            model.ZxKind = Convert.ToInt32(ddlZxKind.SelectedValue);
            model.gdStatus = Convert.ToInt32(ddlStatus.SelectedValue);
            model.Area = string.IsNullOrWhiteSpace(txtArea.Text) ? 0 : Convert.ToInt32(txtArea.Text);
            model.Price = string.IsNullOrWhiteSpace(txtPrice.Text) ? 0 : Convert.ToDecimal(txtPrice.Text);
            model.ImgUrl = txtImgUrl.Text.Trim();
            model.Click = int.Parse(this.txtClick.Text);
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

            ZGD.BLL.NewsInfo bll = new ZGD.BLL.NewsInfo();
            int ReId = bll.Add(model);
            if (ReId > 0)
            {
                //保存图
                ZGD.BLL.ProjectImg pBll = new ZGD.BLL.ProjectImg();
                ZGD.Model.ProjectImg pModel = null;

                //图集数据
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
                        pModel.pID = ReId;
                        pModel.Type = 5;
                        pModel.PubTime = DateTime.Now;
                        pBll.Add(pModel);
                        idx++;
                    }
                }

                //保存日志
                SaveLogs("[工地模块]添加工地：" + model.Title);
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
            hidImg.Value = "";
            hidStatus.Value = "";
            hidTitle.Value = "";

            ddlHouse.SelectedIndex = 0;
            ddlDes.SelectedIndex = 0;
            ddlFG.SelectedIndex = 0;
            ddlHX.SelectedIndex = 0;
            ddlZxKind.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;

            this.txtArea.Text = "0";
            this.txtPrice.Text = "0";
            this.txtUserName.Text = "";
            this.txtTitle.Text = "";
            this.txtKeyword.Text = "";
            this.txtAuthor.Text = "";
            this.txtImgUrl.Text = "";
            this.txtDesc.Value = "";
            //ddlClassId.SelectedIndex = 0;
            //this.txtTags.Text = "";
            //ddlZxType.SelectedIndex = 0;
        }
    }
}