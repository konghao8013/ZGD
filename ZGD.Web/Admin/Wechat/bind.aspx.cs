using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZGD.Common;
using System.Data;

namespace ZGD.Web.Admin.wechat
{
    public partial class bind : ZGD.BasePage.ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("sys_wechat_setting_account", DTEnums.ActionEnum.View.ToString()); //检查权限
                ShowInfo();
            }
        }

        private void ShowInfo()
        {
            BLL.wechat nwechat = new BLL.wechat();
            Model.wechat_bind model = nwechat.bind_Model();
            if (model != null)
            {
                wechat_name.Text = model.wechat_name;
                wechat_no.Text = model.wechat_no;
                wechat_logo.Text = model.wechat_logo;
                if (model.wechat_check == 1)
                    wechat_check.Checked = true;
                wechat_url.Text = model.Wechat_url;
                wechat_account.Text = model.wechat_account;
                wechat_pwd.Text = model.wechat_pwd;
                appid.Text = model.appid;
                appsecret.Text = model.appsecret;
                token.Text = model.token;
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("sys_wechat_setting_account", DTEnums.ActionEnum.Edit.ToString()); //检查权限
            BLL.wechat nwechat = new BLL.wechat();

            Model.wechat_bind model = new Model.wechat_bind();
            model.wechat_name = wechat_name.Text;
            model.wechat_no = wechat_no.Text;
            model.wechat_logo = wechat_logo.Text;

            model.Wechat_url = wechat_url.Text;
            model.wechat_account = wechat_account.Text;
            model.wechat_pwd = wechat_pwd.Text;
            model.appid = appid.Text;
            model.appsecret = appsecret.Text;
            model.token = token.Text;

            if (wechat_check.Checked == true)
                model.wechat_check = 1;
            else
                model.wechat_check = 0;
            if(nwechat.bind_Edit(model)>0)
                JscriptMsg("保存成功！", "bind.aspx", "Success");
            else
                JscriptMsg("保存失败！", "bind.aspx", "Error");
            
        }
    }
}