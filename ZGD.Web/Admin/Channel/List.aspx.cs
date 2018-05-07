using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZGD.Web.Admin.Channel
{
    public partial class List : ZGD.BasePage.ManagePage
    {
        public int kindId; //版块种类
        public int pId; //父类种类
        public string typeName = string.Empty;
        ZGD.BLL.Channel bll = new ZGD.BLL.Channel();
        protected void Page_Load(object sender, EventArgs e)
        {
            //取得版块传参
            if (int.TryParse(Request.Params["kindId"], out kindId) && int.TryParse(Request.Params["pId"], out pId))
            {
                if (!Page.IsPostBack)
                {
                    typeName = TypeName(Request.Params["pId"]);
                    //数据绑定
                    BindData();
                }
            }
            else
            {
                JscriptMsg("出现错误啦！您要管理的类别种类不明确或参数不正确。");
            }
        }

        //数据绑定
        private void BindData()
        {
            DataTable dt = bll.GetList(0, kindId);
            this.rptClassList.DataSource = dt;
            this.rptClassList.DataBind();
        }

        //删除操作
        protected void rptClassList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            HiddenField txtClassId = (HiddenField)e.Item.FindControl("txtClassId");
            int id = Convert.ToInt32(txtClassId.Value);
            ZGD.Model.Channel model = bll.GetModelById(id);

            switch (e.CommandName.ToLower())
            {
                case "btndel":
                    //保存日志
                    SaveLogs("[版块类别]删除类别：" + model.Title);
                    //删除记录
                    bll.Delete(id);
                    //重新绑定数据
                    BindData();
                    JscriptMsg("批量删除成功。");
                    break;
                case "ibtndelete":
                    if (model.IsDelete == 1)
                        bll.UpdateField(id, "IsDelete=0");
                    else
                        bll.UpdateField(id, "IsDelete=1");
                    break;
                case "ibtntop":
                    if (model.IsTop == 1)
                        bll.UpdateField(id, "IsTop=0");
                    else
                        bll.UpdateField(id, "IsTop=1");
                    break;
            }
            BindData();
        }
        //美化列表
        protected void rptClassList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Literal LitFirst = (Literal)e.Item.FindControl("LitFirst");
                HiddenField txtClassId = (HiddenField)e.Item.FindControl("txtClassId");
                HiddenField txtClassLayer = (HiddenField)e.Item.FindControl("txtClassLayer");
                string LitStyle = "{1}{2}";
                string LitImg1 = "<span class=\"folder-open\"></span>";
                string LitImg2 = "<span class=\"folder-line\"></span>";

                int id = Convert.ToInt32(txtClassId.Value);
                ZGD.Model.Channel model = bll.GetModelById(id);
                ImageButton isTop = (ImageButton)e.Item.FindControl("ibtnTop");
                if (model.ParentId != 21)
                {
                    isTop.Visible = false;
                }

                int classLayer = Convert.ToInt32(txtClassLayer.Value);
                if (classLayer == 1)
                {
                    LitFirst.Text = LitImg1;
                }
                else
                {
                    LitFirst.Text = string.Format(LitStyle, classLayer * 18, LitImg2, LitImg1);
                }
            }
        }
    }
}