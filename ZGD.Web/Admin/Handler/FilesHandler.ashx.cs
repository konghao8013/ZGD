using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZGD.Web.Admin.Handler
{
    /// <summary>
    /// FilesHandler 的摘要说明
    /// </summary>
    public class FilesHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string retMsg = string.Empty;
            if (context.Request.Form["action"] != null)
            {
                string action = context.Request.Form["action"];
                string[] path = context.Request.Form["path"].Split(',');
                switch (action)
                {
                    //日志文件删除
                    case "LogDelete":
                        foreach (string str in path)
                        {
                            string tmpPath = "~/files" + str.Replace("|", "/");
                            ZGD.Common.FileDom.DeleteFile(tmpPath);
                        }
                        retMsg = "1";
                        break;
                }
                context.Response.Write(retMsg);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}