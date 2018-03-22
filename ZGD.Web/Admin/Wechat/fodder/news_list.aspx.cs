using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZGD.Web.Admin.wechat.fodder
{
    public partial class news_list : ZGD.BasePage.ManagePage
    {
        public string json = "";
        public int itemCount = 0;
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
                json = data();
            }
        }
        public void post()
        {
            string action = Request["action"];
            if (action == "list")
            {
                Context.Response.Write(data());
            }
            if (action == "del")
            {
                string id = Request["id"];
                int _id = 0;
                if (!int.TryParse(id, out _id)) { Context.Response.Write("no"); }
                if (n_wechat.fodder_Delete(_id) > 0) { Context.Response.Write("yes"); } else { Context.Response.Write("no"); }
            }
        }
        private string data()
        {
            DataSet nds = n_wechat.fodder_Query("new");
            if (nds.Tables.Count > 0)
            {
                DataTable ndt = nds.Tables[0];
                itemCount = ndt.Rows.Count;
                return n_wechat.ToJson(ndt);
            }
            return "";
        }
    }
}