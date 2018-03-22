using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZGD.Web.Admin.wechat.reply
{
    public partial class _default : ZGD.BasePage.ManagePage
    {
        private BLL.wechat n_wechat = new BLL.wechat();
        public string json = "";
        public string type = "";
        public string time = "";
        public string fodder_id = "0";
        private string reply_type = "default";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.ServerVariables["REQUEST_METHOD"] == "POST")
            {
                post();
                Context.Response.End();
            }
            else
            {
                getData();
            }
        }

        private void post()
        {
            string id = Request["id"];
            int _fodder_id = 0;
            if (!int.TryParse(id, out _fodder_id)) { Context.Response.Write("no"); return; }
            if (_fodder_id == 0) { Context.Response.Write("no"); return; }
            if (!n_wechat.fodder_Exists(_fodder_id)) { Context.Response.Write("no"); return; }
            var model = n_wechat.reply_model(reply_type);
            if (model == null) { model = new Model.wechat_reply(); model.id = 0; }
            int rows = 0;
            if (n_wechat.reply_Exists(model.id))
            {
                rows = n_wechat.reply_Update(model.id, _fodder_id);
            }
            else
            {
                var nm = new Model.wechat_reply();
                nm.reply_type = reply_type;
                nm.reply_fodder_id = _fodder_id.ToString();
                nm.reply_key = "";
                rows = n_wechat.reply_Add(nm);
            }
            if (rows > 0)
            {
                Context.Response.Write("yes");
            }
            else
            {
                Context.Response.Write("no");
            }
        }
        public void getData()
        {
            DataSet nds = n_wechat.reply_Query(reply_type);
            if (nds.Tables.Count > 0)
            {
                DataTable ndt = nds.Tables[0];
                if (ndt.Rows.Count > 0)
                {
                    fodder_id = ndt.Rows[0]["reply_fodder_id"].ToString();
                    int _fodder_id = 0;
                    if (!int.TryParse(fodder_id, out _fodder_id)) { return; }
                    if (_fodder_id == 0) { return; }
                    var model = n_wechat.fodder_model(_fodder_id);
                    if (model != null)
                    {
                        if (model.fodder_xml != "")
                        {
                            fodder_id = model.id.ToString();
                            time = model.addtime.ToString("MM月dd日");
                            type = model.fodder_type;
                            json = model.fodder_xml;
                        }
                    }
                    else
                    {
                        fodder_id = "0";
                    }
                }
            }

        }
    }
}