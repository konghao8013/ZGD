using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.SessionState;
using System.IO;
using ZGD.Common;

namespace ZGD.Web.Tools
{
    /// <summary>
    /// AJAX单文件上传页
    /// </summary>
    public class SingleUpload : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            //检查是否登录后上传操作
            if (context.Session[DTKeys.SESSION_ADMIN_INFO] == null)
            {
                context.Response.Write("{\"msg\": 0, \"msbox\": \"请登录后再进行上传文件！\"}");
                return;
            }
            string _refilepath = context.Request.QueryString["ReFilePath"]; //取得返回的对象名称
            string _upfilepath = context.Request.QueryString["UpFilePath"]; //取得上传的对象名称
            int _iswater = 0; //默认打水印
            if (!string.IsNullOrWhiteSpace(context.Request.QueryString["IsWater"]))
            {
                _iswater = Convert.ToInt32(context.Request.QueryString["IsWater"]);
            }
            HttpPostedFile _upfile = context.Request.Files[_upfilepath];
            string _delfile = context.Request.Params[_refilepath];

            if (_upfile == null)
            {
                context.Response.Write("{\"msg\": 0, \"msbox\": \"请选择要上传文件！\"}");
                return;
            }
            UpLoad upFiles = new UpLoad();
            string msg = upFiles.fileSaveAs(_upfile, false, _iswater > 0 ? true : false);
            //删除已存在的旧文件
            if (!string.IsNullOrEmpty(_delfile))
            {
                string _filename = Utils.GetMapPath(_delfile);
                if (File.Exists(_filename))
                {
                    File.Delete(_filename);
                }
            }
            //返回成功信息
            context.Response.Write(msg);
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