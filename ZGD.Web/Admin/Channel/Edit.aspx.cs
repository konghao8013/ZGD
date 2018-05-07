using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZGD.Web.Admin.Channel
{
    public partial class Edit : ZGD.BasePage.ManagePage
    {
        private ZGD.BLL.Channel bll = new ZGD.BLL.Channel();
        private ZGD.Model.Channel model = new ZGD.Model.Channel();
        public int kindId; //种类
        public int pId;    //pId
        public int classId;    //ID
        public string typeName = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            //取得栏目传参
            if (int.TryParse(Request.Params["kindId"], out kindId) && int.TryParse(Request.Params["classId"], out classId)
                && int.TryParse(Request.Params["pId"], out pId))
            {
                model = bll.GetModelById(classId);
                typeName = TypeName(Request.Params["pId"]);

                if (!Page.IsPostBack)
                {
                    //数据绑定
                    ShowInfo();
                }
            }
            else
            {
                JscriptMsg("出现错误！您要修改类别的种类不明确或参数不正确。");
            }
        }

        //绑定数据
        private void ShowInfo()
        {
            this.lblPid.Text = model.ParentId.ToString();
            if (model.ParentId > 0)
            {
                this.lblPname.Text = bll.GetChannelTitle(model.ParentId.ToString());
            }
            else
            {
                this.lblPname.Text = "顶级类别";
            }
            this.txtTitle.Text = model.Title;
            this.txtSortId.Text = model.SortId.ToString();
            cbIsDelete.Checked = model.IsDelete == 1 ? true : false;
            if (Request.Params["pId"] == "21")
            {
                this.txtImgUrl.Text = model.ImgUrl;
                tr_img_panel.Style["display"] = "block";
                imgPanel.InnerHtml = "<img src=\"" + model.ImgUrl + "\" />";
            }
            else
            {
                tr_img_panel.Style["display"] = "none";
            }
        }

        //保存修改
        protected void btnSave_Click(object sender, EventArgs e)
        {
            model.Title = txtTitle.Text.Trim();
            model.SortId = int.Parse(txtSortId.Text.Trim());
            model.IsDelete = cbIsDelete.Checked ? 1 : 0;
            model.ImgUrl = txtImgUrl.Text.Trim();
            //修改栏目
            bll.Update(model);
            //保存日志
            SaveLogs("[栏目类别]修改类别：" + model.Title);
            JscriptMsg("保存成功", "List.aspx?kindId=" + this.kindId + "&pId=" + pId);
        }
    }
}