using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.SessionState;
using ZGD.Common;

namespace ZGD.Web.Modules
{
    public class GlobalAuthenticationModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            // 在模块初始化时订阅AcquireRequestState事件
            //context.AcquireRequestState += (sender, e) =>
            //{
            //    // 启用会话状态
            //    HttpContext currentContext = HttpContext.Current;
            //    currentContext.SetSessionStateBehavior(SessionStateBehavior.Default);
            //};


            context.AuthenticateRequest += AuthenticateRequestHandler;
        }
        public void Dispose()
        {

        }

        private void AuthenticateRequestHandler(Object source, EventArgs e)
        {
            HttpApplication application = (HttpApplication)source;
            HttpContext context = application.Context;

            // 在此处执行身份验证和授权逻辑
            //if (!IsUserAuthenticated(context) || !IsUserAuthorized(context))
            //{
            //    // 如果用户未经过身份验证或未经授权，可以重定向到登录页或其他自定义页面
            //    context.Response.Redirect("~/admin/Login.aspx");
            //}
            ///admin/login.aspx
            if (context.Session == null)
            {
                return;
            }
            var url = context.Request.Url.AbsolutePath;
            if (url.StartsWith("admin/login.aspx")
                || !context.Request.Url.AbsolutePath.StartsWith("/admin", StringComparison.CurrentCultureIgnoreCase)
                )
            {
                //1.登录页面忽略验证
                //2.非管理页面忽略验证
                return;
            }
            if (!IsLogin(context) || !IsAuthorized(context))
            {
                // 如果用户未经过身份验证或未经授权，可以重定向到登录页或其他自定义页面
                context.Response.Redirect("~/admin/Login.aspx");
            }
        }
        //
        private bool IsLogin(HttpContext context)
        {
            return context.Session[DTKeys.SESSION_ADMIN_INFO] != null;
        }

        // 自定义授权逻辑
        private bool IsAuthorized(HttpContext context)
        {
            var user = context.Session[DTKeys.SESSION_ADMIN_INFO] as Model.manager;
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
                var url = context.Request.Url.AbsolutePath;

                string pattern = "/admin/";

                Regex regex = new Regex(Regex.Escape(pattern));
                string replaced = regex.Replace(url, " ", 1);

                var roles = bll.GetRoleValues(user.role_id, replaced);

            }
            return false;
        }

    }
}