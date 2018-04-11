using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZGD.Web.Admin.Channel
{
    public partial class Add : ZGD.BasePage.ManagePage
    {
        private ZGD.BLL.Channel bll = new ZGD.BLL.Channel();
        private ZGD.Model.Channel model = new ZGD.Model.Channel();
        public int kindId; //栏目种类
        public int pId;    //栏目父ID
        public string pTitle = "顶级版块";

        protected void Page_Load(object sender, EventArgs e)
        {
            //取得栏目传参
            if (int.TryParse(Request.Params["kindId"], out kindId))
            {
                if (int.TryParse(Request.Params["pId"], out pId))
                {
                    pTitle = bll.GetChannelTitle(pId);
                }
                else
                {
                    pId = 0;
                }
                lblPid.Text = pId.ToString();
                lblPname.Text = pTitle;
            }
            else
            {
                JscriptMsg("出现错误！您要增加版块的种类不明确或参数不正确。");
            }
        }

        //添加
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int classId;
            int parentId = int.Parse(this.lblPid.Text.Trim());          //上一级目录
            int classLayer = 1;                                         //栏目深度
            string classList = "";

            model.Title = this.txtTitle.Text.Trim();
            model.ParentId = parentId;
            model.SortId = int.Parse(this.txtSortId.Text.Trim());
            model.KindId = this.kindId;
            model.IsDelete = cbIsDelete.Checked ? 1 : 0;

            //添加栏目
            classId = bll.Add(model);
            //修改栏目的下属栏目ID列表
            if (parentId > 0)
            {
                DataSet ds = bll.GetChannelListByClassId(parentId);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    classList = dr["ClassList"].ToString().Trim() + classId + ",";
                    classLayer = Convert.ToInt32(dr["ClassLayer"]) + 1;
                }
            }
            else
            {
                classList = "," + classId + ",";
                classLayer = 1;
            }
            model.Id = classId;
            model.ClassList = classList;
            model.ClassLayer = classLayer;
            bll.Update(model);
            //保存日志
            SaveLogs("[栏目版块]添加版块：" + model.Title);
            JscriptMsg("保存成功", "List.aspx?kindId=" + this.kindId + "&pId=" + pId);
        }
    }
}