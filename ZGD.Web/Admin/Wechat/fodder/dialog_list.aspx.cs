using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZGD.Web.Admin.wechat.fodder
{
    public partial class dialog_list : ZGD.BasePage.ManagePage
    {
        public string json = "";
        private BLL.wechat n_wechat = new BLL.wechat();
        protected void Page_Load(object sender, EventArgs e)
        {
            getData();
        }

        private void getData()
        {
            DataSet nds = n_wechat.fodder_Query();
            if (nds.Tables.Count > 0)
            {
                DataTable ndt = nds.Tables[0];
                if (ndt.Rows.Count > 0)
                {
                    json = n_wechat.ToJson(ndt);
                }
            }
        }
    }
}