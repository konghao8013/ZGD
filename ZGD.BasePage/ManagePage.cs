using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using System.Configuration;
using ZGD.Common;

namespace ZGD.BasePage
{
    public class ManagePage : System.Web.UI.Page
    {
        protected internal ZGD.Model.SiteConfig siteConfig;
        public ManagePage()
        {
            this.Load += new EventHandler(ManagePage_Load);
            siteConfig = new BLL.siteconfig().loadConfig();
        }

        void ManagePage_Load(object sender, EventArgs e)
        {
            if (Session[DTKeys.SESSION_ADMIN_INFO] == null)
            {
                Response.Write("<script>alert('对不起,您没有登录！');parent.location.href='/admin/login.aspx'</script>");
                Response.End();
            }
        }

        /// <summary>
        /// 取得管理员信息
        /// </summary>
        public Model.manager GetAdminInfo()
        {
            if (Session[DTKeys.SESSION_ADMIN_INFO] != null)
            {
                Model.manager model = Session[DTKeys.SESSION_ADMIN_INFO] as Model.manager;
                if (model != null)
                {
                    return model;
                }
            }
            return null;
        }

        /// <summary>
        /// 检查管理员权限
        /// </summary>
        /// <param name="nav_name">菜单名称</param>
        /// <param name="action_type">操作类型</param>
        public void ChkAdminLevel(string nav_name, string action_type)
        {
            Model.manager model = GetAdminInfo();
            BLL.manager_role bll = new BLL.manager_role();
            bool result = bll.Exists(model.role_id, nav_name, action_type);

            if (!result)
            {
                string msgbox = "parent.jsdialog(\"错误提示\", \"您没有管理该页面的权限，请勿非法进入！\", \"back\", \"Error\")";
                Response.Write("<script type=\"text/javascript\">" + msgbox + "</script>");
                Response.End();
            }
        }

        /// <summary>
        /// 写入管理日志
        /// </summary>
        /// <param name="action_type"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public bool AddAdminLog(string action_type, string remark)
        {
            if (siteConfig.logstatus > 0)
            {
                Model.manager model = GetAdminInfo();
                int newId = new BLL.manager_log().Add(model.id, model.user_name, action_type, remark);
                if (newId > 0)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 日志写入方法
        /// </summary>
        /// <param name="str"></param>
        protected void SaveLogs(string str)
        {
            if (siteConfig.logstatus == 0)
            {
                return;
            }
            ZGD.BLL.SystemLog bll = new ZGD.BLL.SystemLog();
            ZGD.Model.SystemLog model = new ZGD.Model.SystemLog();
            if (Session["AdminName"] != null)
            {
                model.UserName = Session["AdminName"].ToString();
            }
            if (HttpContext.Current.Request.UserHostAddress != null)
            {
                model.IPAddress = HttpContext.Current.Request.UserHostAddress;
            }
            model.Title = str;
            model.AddTime = DateTime.Now;
            bll.Add(model);
        }

        #region JS提示============================================
        /// <summary>
        /// 遮罩提示窗口
        /// </summary>
        /// <param name="msgbox">提示文字</param>
        /// <param name="url">返回地址</param>
        protected void JscriptMsg(string msgbox, string url = "")
        {
            string msbox = "";
            msbox += "<script type=\"text/javascript\">\n";
            if (!string.IsNullOrWhiteSpace(url))
                msbox += "alert('" + msgbox + "');window.location.href='" + url + "'";
            else
                msbox += "alert('" + msgbox + "');";
            msbox += "</script>\n";
            ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsMsg", msbox);
        }

        /// <summary>
        /// 添加编辑删除提示
        /// </summary>
        /// <param name="msgtitle">提示文字</param>
        /// <param name="url">返回地址</param>
        /// <param name="msgcss">CSS样式</param>
        protected void JscriptMsg(string msgtitle, string url, string msgcss)
        {
            string msbox = "";
            msbox += "<script type=\"text/javascript\">\n";
            msbox += "parent.jsprint(\"" + msgtitle + "\", \"" + url + "\", \"" + msgcss + "\")";
            msbox += "</script>\n";
            ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msbox);
        }
        /// <summary>
        /// 带回传函数的添加编辑删除提示
        /// </summary>
        /// <param name="msgtitle">提示文字</param>
        /// <param name="url">返回地址</param>
        /// <param name="msgcss">CSS样式</param>
        /// <param name="callback">JS回调函数</param>
        protected void JscriptMsg(string msgtitle, string url, string msgcss, string callback)
        {
            string msbox = "";
            msbox += "<script type=\"text/javascript\">\n";
            if (callback != "")
            {
                msbox += "parent.jsprint(\"" + msgtitle + "\", \"" + url + "\", \"" + msgcss + "\", \"" + callback + "\")";
            }
            else
                msbox += "parent.jsprint(\"" + msgtitle + "\", \"" + url + "\", \"" + msgcss + "\")";
            msbox += "</script>\n";
            ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msbox);
        }
        protected string WriteJscriptMsg(string msgtitle, string url, string msgcss, string callback)
        {
            string msbox = "<script>parent.jsprint(\"" + msgtitle + "\", \"" + url + "\", \"" + msgcss + "\", " + callback + ")</script>";
            //Context.Response.Write(msbox);
            return msbox;
        }
        #endregion

        /// <summary>
        /// 绑定类别DropDownList控制
        /// </summary>
        /// <param name="parentId">父类ID</param>
        /// /// <param name="firstItemTxt">第一项显示的文字</param>
        /// <param name="kindId">大类ID</param>
        /// <param name="ddl">要绑定的DropDownList控件</param>
        protected void ChannelTreeBind(int parentId, string firstItemTxt, int kindId, DropDownList ddl)
        {
            ZGD.BLL.Channel cbll = new ZGD.BLL.Channel();
            DataTable dt = cbll.BindList(parentId, kindId);

            ddl.Items.Clear();
            ddl.Items.Add(new ListItem(firstItemTxt, ""));
            foreach (DataRow dr in dt.Rows)
            {
                string Id = dr["Id"].ToString();
                int ClassLayer = int.Parse(dr["ClassLayer"].ToString());
                string Title = dr["Title"].ToString().Trim();

                if (ClassLayer == 1)
                {
                    ddl.Items.Add(new ListItem(Title, Id));
                }
                else
                {
                    Title = "├ " + Title;
                    Title = Utils.StringOfChar(ClassLayer - 1, "　") + Title;
                    ddl.Items.Add(new ListItem(Title, Id));
                }
            }
        }


        /// <summary>
        /// 绑定类别DropDownList控制 (value为标题)
        /// </summary>
        /// <param name="parentId">父类ID</param>
        /// /// <param name="firstItemTxt">第一项显示的文字</param>
        /// <param name="kindId">大类ID</param>
        /// <param name="ddl">要绑定的DropDownList控件</param>
        protected void ChannelTreeBind2(int parentId, string firstItemTxt, int kindId, DropDownList ddl)
        {
            ZGD.BLL.Channel cbll = new ZGD.BLL.Channel();
            DataTable dt = cbll.BindList(parentId, kindId);

            ddl.Items.Clear();
            ddl.Items.Add(new ListItem(firstItemTxt, ""));
            foreach (DataRow dr in dt.Rows)
            {
                string Id = dr["Id"].ToString();
                int ClassLayer = int.Parse(dr["ClassLayer"].ToString());
                string Title = dr["Title"].ToString().Trim();

                if (ClassLayer == 1)
                {
                    ddl.Items.Add(new ListItem(Title, Title));
                }
                else
                {
                    Title = "├ " + Title;
                    Title = Utils.StringOfChar(ClassLayer - 1, "　") + Title;
                    ddl.Items.Add(new ListItem(Title, Title));
                }
            }
        }

        #region

        //==================================以下为文件操作函数===================================
        /// <summary>
        /// 删除单个文件
        /// </summary>
        /// <param name="_filepath">文件相对路径</param>
        protected void DeleteFile(string _filepath)
        {
            if (string.IsNullOrEmpty(_filepath))
            {
                return;
            }
            string fullpath = Utils.GetMapPath(_filepath);
            if (File.Exists(fullpath))
            {
                File.Delete(fullpath);
            }
        }
        #endregion
    }
}