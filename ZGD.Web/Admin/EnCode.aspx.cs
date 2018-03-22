using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZGD.Web.Admin
{
    public partial class EnCode : ZGD.BasePage.ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string msg = HttpUtility.UrlEncode(TextBox1.Text);
            Label1.Text = msg;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string msg2 = HttpUtility.UrlDecode(TextBox2.Text);
            Label2.Text = msg2;
        }
    }
}