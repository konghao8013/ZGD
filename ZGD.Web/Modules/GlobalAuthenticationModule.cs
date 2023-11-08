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
    
        }
        //
      

    }
}