using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Web.SessionState;
using ZGD.Common;

namespace ZGD.BasePage
{
    public class BasePage : System.Web.UI.Page
    {
        public virtual void Page_Error(EventArgs e)
        {
            Exception ex = Server.GetLastError();
            LogHepler.ErrorLog(ex, "~/Logout.aspx?logoutmsg=-1", false);
        }
    }
}
