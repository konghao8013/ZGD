using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZGD.Web.Admin.Contents
{
    public partial class content : ZGD.BasePage.ManagePage
    {
        public string xmlPath = "~/XmlConfig/About.xml";
        public string typeName = "集团介绍";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //赋值
                ShowInfo();
            }
        }

        #region 赋值操作
        private void ShowInfo()
        {
            string type = cbType.SelectedValue;
            typeName = cbType.SelectedItem.Text;
            string content = ZGD.Common.XMLDom.ReadXml(xmlPath, "SiteCon/" + type);
            kEditor.Value = ZGD.Common.StringHandler.DeCode(content);
        }
        #endregion

        #region 内容管理
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string type = cbType.SelectedValue;
            typeName = cbType.SelectedItem.Text;
            string content = ZGD.Common.StringHandler.EnCode(Request["kEditor"]);
            ZGD.Common.XMLDom.EditXmlElement(xmlPath, "SiteCon/" + type, content);

            //保存日志
            SaveLogs("[内容管理]内容设置：" + content);
            JscriptMsg("内容设置成功");
        }
        #endregion

        protected void btnretset_Click(object sender, EventArgs e)
        {
            kEditor.Value = "";
        }

        protected void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowInfo();
        }

    }
}