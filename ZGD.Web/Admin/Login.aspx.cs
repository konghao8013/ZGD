using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using ZGD.Common;

namespace ZGD.Web.Admin
{
    public partial class Login : System.Web.UI.Page
    {
        ZGD.BLL.Admin bll = new BLL.Admin();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Session[DTKeys.SESSION_CODE] != null && Session[DTKeys.SESSION_CODE].ToString() != "")
            {
                //判断登录
                if (Session["AdminLoginSun"] != null && Convert.ToInt32(Session["AdminLoginSun"]) > 5)
                {
                    this.btnSubmit.Enabled = false;
                    this.txtCode.Text = "";
                    this.txtUserName.Enabled = false;
                    this.txtPassword.Enabled = false;
                    this.msgtip.InnerText = "对不起，你错误登录超过5次，系统登录锁定!";
                    return;
                }

                string userName = txtUserName.Text.Trim();
                userName = StringHandler.InputText(userName, userName.Length);
                string userPwd = txtPassword.Text.Trim();
                userPwd = StringHandler.InputText(userPwd, userPwd.Length);
                string vCode = txtCode.Text.Trim();
#if DEBUG
                userName = string.IsNullOrWhiteSpace(userName) ? "admin" : userName;
                userPwd = string.IsNullOrWhiteSpace(userPwd) ? "111111" : userPwd;
                vCode = string.IsNullOrWhiteSpace(vCode) ? Session[DTKeys.SESSION_CODE].ToString() : vCode;
#endif
                
                if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(userPwd))
                {
                    msgtip.InnerText = "请输入您要登录用户名或密码";
                }
                else
                {
                    if (vCode.ToLower() != Session[DTKeys.SESSION_CODE].ToString().ToLower())
                    {
                        this.msgtip.InnerText = "您输入的验证码不正确，请重新输入！";
                        this.txtCode.Text = "";
                        Session[DTKeys.SESSION_CODE] = null;
                        return;
                    }
                    else
                    {
                        Session[DTKeys.SESSION_CODE] = null;
                    }

                    BLL.manager bll = new BLL.manager();
                    Model.manager model = bll.GetModel(userName, userPwd, true);
                    if (model == null)
                    {
                        if (Session["AdminLoginSun"] == null)
                        {
                            Session["AdminLoginSun"] = 1;
                        }
                        else
                        {
                            Session["AdminLoginSun"] = Convert.ToInt32(Session["AdminLoginSun"]) + 1;
                        }
                        msgtip.InnerText = "您输入的用户名或密码不正确";
                        return;
                    }
                    Session[DTKeys.SESSION_ADMIN_INFO] = model;
                    Session["AdminNo"] = model.id;
                    Session["AdminName"] = model.user_name;
                    //设置超时时间
                    Session.Timeout = 120;
                    Session["AdminLoginSun"] = null;
                    //写入登录日志
                    Model.SiteConfig siteConfig = new BLL.siteconfig().loadConfig();
                    if (siteConfig.logstatus > 0)
                    {
                        new BLL.manager_log().Add(model.id, model.user_name, DTEnums.ActionEnum.Login.ToString(), "用户登录");
                    }

                    Response.Redirect("Index.aspx");
                }
            }
            else
            {
                this.msgtip.InnerText = "请输入验证码！";
            }
        }

        protected void logincancel_Click(object sender, ImageClickEventArgs e)
        {
            this.txtUserName.Text = "";
            this.txtPassword.Text = "";
        }

    }
}