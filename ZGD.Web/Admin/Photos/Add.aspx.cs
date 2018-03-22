using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZGD.Common;
using System.IO;

namespace ZGD.Web.Admin.Photos
{
    public partial class Add : ZGD.BasePage.ManagePage
    {
        public string title = "相册";
        public int pType = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["t"]))
            {
                pType = Convert.ToInt32(Request.QueryString["t"]);
            }
            switch (pType)
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
        }

        #region 添加
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int t = 0;
            if (!string.IsNullOrEmpty(Request.QueryString["t"]))
            {
                t = Convert.ToInt32(Request.QueryString["t"]);
            }
            string strErr = "";
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                strErr += title + "标题不能为空！\\n";
            }
            if (string.IsNullOrWhiteSpace(txtImgUrl.Text))
            {
                strErr += "请上传" + title + "封面！\\n";
            }
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }

            ZGD.Model.Photos model = new ZGD.Model.Photos();

            model.pType = Convert.ToInt32(ddlType.SelectedValue);
            model.PhotoName = this.txtTitle.Text;
            model.PhotoCon = txtDescription.Text;
            model.ImgUrl = txtImgUrl.Text.Trim();
            model.Sort = string.IsNullOrEmpty(txtSort.Text) ? 0 : int.Parse(this.txtSort.Text);
            model.PubTime = DateTime.Now;
            model.IsTop = cblItem.Checked ? 1 : 0;
            model.Type = t;

            ZGD.BLL.Photos bll = new ZGD.BLL.Photos();
            int ReId = bll.Add(model);
            if (ReId > 0)
            {
                ////保存户型图、项目图
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
                //            pModel.ImageSmall = ZGD.Common.Thumbnail.CreateThumbImg(item, 60, 40, "W");
                //        }
                //        pModel.DesignerId = 0;
                //        pModel.pID = ReId;
                //        pModel.Type = 3;
                //        pModel.PubTime = DateTime.Now;
                //        pBll.Add(pModel);
                //        idx++;
                //    }
                //}

                //保存日志
                SaveLogs("[" + title + "模块]添加" + title + "：" + model.PhotoName);
                Clear();
                JscriptMsg("" + title + "发布成功");
            }
            else
            {
                JscriptMsg("发布过程中发生错误");
            }
        }
        #endregion

        void Clear()
        {
            txtDescription.Text = "";
            txtImgUrl.Text = "";
            txtTitle.Text = "";
            txtSort.Text = "0";
            //hidImg.Value = "";
            //hidTitle.Value = "";
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Clear();
        }

    }
}