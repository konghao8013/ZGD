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
    public partial class Add : ZGD.BasePage.ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        #region 添加
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

            ZGD.Model.Designer model = new ZGD.Model.Designer();
            model.Name = txtName.Text;
            model.cName = txtcName.Text;
            model.Title = txtTitle.Value;
            model.Rongyu = txtRongyu.Text;
            model.School = txtSchool.Text;
            model.Star = Convert.ToInt32(ddlStar.SelectedValue);
            model.Sort = Convert.ToInt32(txtSort.Text);
            model.cyDate = string.IsNullOrWhiteSpace(txtCyDate.Text) ? 0 : Convert.ToInt32(txtCyDate.Text);
            model.GoodAtDes = txtGoodAtDes.Text;
            model.GoodAt = txtGoodAt.Text;
            model.Photo = txtImgUrl.Text.Trim();
            model.IsTop = cblItem.Checked ? 1 : 0;
            model.Clicks = string.IsNullOrWhiteSpace(txtClick.Text) ? 0 : Convert.ToInt32(txtClick.Text);

            ZGD.BLL.Designer bll = new ZGD.BLL.Designer();
            int ReId = bll.Add(model);
            if (ReId > 0)
            {
                ////保存设计师相册
                //ZGD.BLL.ProjectImg pBll = new ZGD.BLL.ProjectImg();
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
                //        pModel.DesignerId = ReId;
                //        pModel.pID = 0;
                //        pModel.Type = 2;
                //        pModel.PubTime = DateTime.Now;
                //        pBll.Add(pModel);
                //        idx++;
                //    }
                //}
                //保存日志
                SaveLogs("[设计师模块]添加设计师：" + model.Title);
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
            this.txtClick.Text = "0";
            this.txtName.Text = "";
            this.txtcName.Text = "";
            this.txtTitle.Value = "";
            this.txtCyDate.Text = "";
            this.txtGoodAtDes.Text = "";
            this.txtGoodAt.Text = "";
            txtSort.Text = "0";
            ddlStar.SelectedIndex = 0;
            txtSchool.Text = "";
            txtImgUrl.Text = "";
        }

    }
}