using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
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
            GlobalFilters.Filters.Add(new GlobalActionFilter());
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

    }
}
