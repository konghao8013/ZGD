using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZGD.Web.Admin.wechat.reply
{
    public partial class dialog_key : ZGD.BasePage.ManagePage
    {
        private BLL.wechat n_wechat = new BLL.wechat();
        private string reply_type = "key";
        public Model.wechat_reply reply =new Model.wechat_reply();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.ServerVariables["REQUEST_METHOD"] == "POST")
            {
                post();
                Context.Response.End();
            }
            else
            {
                data();
            }
        }
        public void post()
        {
            string action = Request["action"];
            if (action == "f")
            {
                string id = Request["id"];
                int _id = 0;
                if (!int.TryParse(id, out _id)) { Context.Response.Write("no"); }

                if (!n_wechat.fodder_Exists(_id)) {  Context.Response.Write("no"); }

                DataSet nds = n_wechat.fodder_Query(_id);
                if (nds.Tables.Count > 0)
                {
                    DataTable ndt=nds.Tables[0];
                    if (ndt.Rows.Count == 1)
                    {
                        Context.Response.Write(n_wechat.ToJson(ndt));
                        return;
                    }
                }
                Context.Response.Write("no");
            }
            if (action == "add")
            {
                string key = Request["key"]; if (key.Trim() == "") { Context.Response.Write("no"); return; }
                string fodder_id = Request["fodder_id"];

                int _id = 0;
                if (!int.TryParse(fodder_id, out _id)) { Context.Response.Write("no"); return; }

                if (!n_wechat.fodder_Exists(_id)) { Context.Response.Write("no"); return; }

                if (n_wechat.reply_Exists(reply_type,key)) { Context.Response.Write("repter"); return; }

                Model.wechat_reply model = new Model.wechat_reply();
                model.reply_type = reply_type;
                model.reply_key = key;
                model.reply_fodder_id = fodder_id;
                if (n_wechat.reply_Add(model) > 0)
                {
                    Context.Response.Write("yes"); return;
                }
                else
                {
                    Context.Response.Write("no"); return;
                }
            }
            if (action == "edit")
            {
                string key = Request["key"]; if (key.Trim() == "") { Context.Response.Write("no"); return; }
                string fodder_id = Request["fodder_id"];

                string id = Request["id"];

                int _id = 0;
                if (!int.TryParse(id, out _id)) { Context.Response.Write("no"); return; }
                if (!n_wechat.reply_Exists(_id)) { Context.Response.Write("no"); return; }

                int _fodder_id = 0;
                if (!int.TryParse(fodder_id, out _fodder_id)) { Context.Response.Write("no"); return; }

                if (!n_wechat.fodder_Exists(_fodder_id)) { Context.Response.Write("no"); return; }

                if (n_wechat.reply_Exists(_id,reply_type, key)) { Context.Response.Write("repter"); return; }



                if (n_wechat.reply_Update(_id,_fodder_id,key) > 0)
                {
                    Context.Response.Write("yes"); return;
                }
                else
                {
                    Context.Response.Write("no"); return;
                }
            }
        }
        private void data()
        {
            string id = Request["id"];
            int _id = 0;
            if (!int.TryParse(id, out _id)) {  return; }
            if (!n_wechat.reply_Exists(_id)) {  return; }
            reply = n_wechat.reply_model(_id);
        }
    }
}