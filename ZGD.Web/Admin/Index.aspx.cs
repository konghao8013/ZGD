using ZGD.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZGD.Web.Admin
{
    public partial class Index : ZGD.BasePage.ManagePage
    {
        protected Model.manager admin_info;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session[DTKeys.SESSION_ADMIN_INFO] == null)
                {
                    Response.Write("<script>alert('对不起,您没有登录！');parent.location.href='Login.aspx'</script>");
                    return;
                }
                else
                {
                    admin_info = GetAdminInfo();
                }
            }
        }

        //安全退出
        protected void lbtnExit_Click(object sender, EventArgs e)
        {
            Session[DTKeys.SESSION_ADMIN_INFO] = null;
            Response.Redirect("Login.aspx");
        }
    }
}