using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using ZGD.Common;

namespace ZGD.Web
{
    public class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            //GlobalFilters.Filters.Add(new GlobalActionFilter());
            // 在应用程序启动时运行的代码
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }


        //protected void Application_BeginRequest(Object sender, EventArgs e)
        //{
        //    //验证参数非法
        //    NameValueCollection Params = Request.Params;
        //    string[] paramKeys = Request.Params.AllKeys;
        //    foreach (string item in paramKeys)
        //    {
        //        if (Params[item] != null && !string.IsNullOrWhiteSpace(Params[item]))
        //        {
        //            //检测
        //            if (StringHandler.SqlFilter(Params[item].ToString()))
        //            {
        //                LogHepler.WriteLog("非法请求（" + item + "）：\n  IP：" + StringHandler.GetClientIP() + "\n 地址：" + Request.Url.ToString() + "\n 异常参数：" + Params[item].ToString() + "\n");
        //                throw new Exception("非法请求");
        //            }
        //        }
        //    }
        //}
        protected void Application_AcquireRequestState(object send, EventArgs e)
        {
            if (Session == null)
            {
                return;
            }
            var url = Request.Url.AbsolutePath;
            var ignorePage = new List<string> {
            "/admin/login.aspx",
            "/admin/index.aspx",
            "/admin/center.aspx",
            "/admin/manager/manager_pwd.aspx"
            };
            if (ignorePage.Any(a => url.StartsWith(a, StringComparison.OrdinalIgnoreCase))
                || !Request.Url.AbsolutePath.StartsWith("/admin", StringComparison.CurrentCultureIgnoreCase)
                )
            {
                //1.登录页面忽略验证
                //2.非管理页面忽略验证
                return;
            }
            if (!IsLogin() || !IsAuthorized())
            {
                // 如果用户未经过身份验证或未经授权，可以重定向到登录页或其他自定义页面
                Response.Redirect("~/Errors/403.html");
            }
        }
        /// <summary>
        /// 接收请求
        /// </summary>
        protected void Application_BeginRequest()
        {
            var current = HttpContext.Current;
            //拦截请求
            string[] segments = Request.Url.Segments;
            if (segments.Length > 1 && segments[1].ToLower() == "testone")
            {
                //需要自己指定输出内容和类型
                Response.ContentType = "text/html;charset=utf-8";
                Response.Write("请求拦截处理");
                Response.End(); // 此处结束响应，就不会走路由系统
            }
        }

        void Application_End(object sender, EventArgs e)
        {
            //  在应用程序关闭时运行的代码

        }

        void Application_Error(object sender, EventArgs e)
        {
            // 在出现未处理的错误时运行的代码

        }

        void Session_Start(object sender, EventArgs e)
        {
            // 在新会话启动时运行的代码

        }

        void Session_End(object sender, EventArgs e)
        {
            // 在会话结束时运行的代码。 
            // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
            // InProc 时，才会引发 Session_End 事件。如果会话模式设置为 StateServer 
            // 或 SQLServer，则不会引发该事件。

        }


        private bool IsLogin()
        {
            return Session[DTKeys.SESSION_ADMIN_INFO] != null;
        }

        // 自定义授权逻辑
        private bool IsAuthorized()
        {
            var user = Session[DTKeys.SESSION_ADMIN_INFO] as Model.manager;
            if (user != null)
            {
                //锁定账户验证不通过
                if (user.is_lock == 1)
                {
                    return false;
                }
                //超级管理员，直接验证通过
                if (user.role_type <= 1)
                {
                    return true;
                }
                var bll = new BLL.manager_role();
                var url = Request.Url.AbsolutePath;

                string pattern = "/admin/";

                Regex regex = new Regex(Regex.Escape(pattern));
                string replaced = regex.Replace(url, "", 1);

                var roles = bll.GetRoleValues(user.role_id, replaced);
                return roles.Any(a => a.action_type == "Show");
            }
            return false;
        }

    }
}
