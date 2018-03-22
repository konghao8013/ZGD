using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZGD.Web.Admin.wechat.fodder
{
    public partial class dialog_txt : ZGD.BasePage.ManagePage
    {
        private BLL.wechat n_wechat = new BLL.wechat();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.ServerVariables["REQUEST_METHOD"] == "POST")
            {
                post();
                Context.Response.End();
            }
        }
        public void post()
        {
            string action = Request["action"];
            string id = Request["id"];
            string txt = Request["txt"];

            txt = txt.Replace("\n", "\\n");
            txt = txt.Replace("\r", "\\r");
            txt = txt.Replace("<", "&lt;");
            txt = txt.Replace(">", "&gt;");


            if (action == "add")
            {
                Model.wechat_fodder fodder_model = new Model.wechat_fodder();
                fodder_model.fodder_type = "txt";
                fodder_model.fodder_xml = txt;
                int row=n_wechat.fodder_Add(fodder_model);
                if (row > 0)
                { Context.Response.Write("yes"); }
                else
                { Context.Response.Write("no"); }
            }
            if (action == "edit")
            {
                int _id=0;
                if (!int.TryParse(id, out _id)) { Context.Response.Write("no"); return; }

                int row = n_wechat.fodder_Update(_id, txt);
                if (row > 0)
                { Context.Response.Write("yes"); }
                else
                { Context.Response.Write("no"); }
            }
        }
    }
}