using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZGD.Web.Admin.wechat.fodder
{
    public partial class dialog_news : ZGD.BasePage.ManagePage
    {
        public string time = DateTime.Now.ToString("yyyyMMddhhmmssffff");
        private BLL.wechat n_wechat = new BLL.wechat();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.ServerVariables["REQUEST_METHOD"] == "POST")
            {
                post();
                Context.Response.End();
            }
            else
            {
                loadData();
            }
        }
        public string json = "";
        private void loadData()
        {
            string id=Request["id"];
            int _id = 0;
            if (!int.TryParse(id, out _id)) { JscriptMsg("参数错误！", "back", "Error"); return; }
            if (_id == 0) { JscriptMsg("参数错误！", "back", "Error"); return; }
            var model = n_wechat.fodder_model(_id);
            json = model.fodder_xml;
        }
        public void post()
        {
            var json = Request["json"];
            string action = Request["action"];
            string id = Request["id"];

            json = json.Replace("\n", "\\n");
            json = json.Replace("\r", "\\r");
            json = json.Replace("<", "&lt;");
            json = json.Replace(">", "&gt;");

            if (action == "add")
            {
                Model.wechat_fodder fodder_model = new Model.wechat_fodder();
                fodder_model.fodder_type = "news";
                fodder_model.fodder_xml = json;
                int row = n_wechat.fodder_Add(fodder_model);
                if (row > 0)
                { Context.Response.Write("yes"); }
                else
                { Context.Response.Write("no"); }
            }
            if (action == "edit")
            {
                int _id = 0;
                if (!int.TryParse(id, out _id)) { Context.Response.Write("no"); return; }

                int row = n_wechat.fodder_Update(_id, json);
                if (row > 0)
                { Context.Response.Write("yes"); }
                else
                { Context.Response.Write("no"); }
            }
        }
    }
}