/**
 * KindEditor ASP.NET
 *
 * 本ASP.NET程序是演示程序，建议不要直接在实际项目中使用。
 * 如果您确定直接使用本程序，使用之前请仔细确认相关安全设置。
 *
 */
using System;
using System.Collections;
using System.Web;
using System.IO;
using System.Globalization;
using ZGD.Common;
using System.Linq;

namespace ZGD.Web.Tools
{
    /// <summary>
    /// editer_upfiles 的摘要说明
    /// </summary>
    public class editer_upfiles : IHttpHandler
    {
        private HttpContext context;
        public void ProcessRequest(HttpContext context)
        {
            //String aspxUrl = context.Request.Path.Substring(0, context.Request.Path.LastIndexOf("/") + 1);
            ZGD.Model.SiteConfig webset = new BLL.siteconfig().loadConfig();
            //文件保存目录路径
            String savePath = "~/" + webset.filepath + "/attached/";

            //文件保存目录URL
            String saveUrl = "/" + webset.filepath + "/attached/";

            //定义允许上传的文件扩展名
            Hashtable extTable = new Hashtable();
            extTable.Add("image", webset.fileextension.ToLower());
            extTable.Add("flash", "swf,flv");
            extTable.Add("media", "swf,flv,mp3,wav,wma,wmv,mid,avi,mpg,mp4,asf,rm,rmvb");
            extTable.Add("file", "doc,docx,xls,xlsx,ppt,txt,zip,rar,gz,bz2");

            //最大文件大小
            int maxSize = webset.attachsize;
            this.context = context;

            HttpPostedFile imgFile = context.Request.Files["imgFile"];
            if (imgFile == null)
            {
                showError("请选择文件。");
            }

            String dirPath = context.Server.MapPath(savePath);
            if (!Directory.Exists(dirPath))
            {
                showError("上传目录不存在。");
            }

            String dirName = context.Request.QueryString["dir"];
            if (String.IsNullOrEmpty(dirName))
            {
                dirName = "image";
            }
            if (!extTable.ContainsKey(dirName))
            {
                showError("目录名不正确。");
            }

            String fileName = imgFile.FileName;
            String fileExt = Path.GetExtension(fileName).ToLower();

            if (imgFile.InputStream == null || imgFile.InputStream.Length > maxSize)
            {
                showError("上传文件大小超过限制。");
            }

            if (String.IsNullOrEmpty(fileExt) || Array.IndexOf(((String)extTable[dirName]).Split(','), fileExt.Substring(1).ToLower()) == -1)
            {
                showError("上传文件扩展名是不允许的扩展名。\n只允许" + ((String)extTable[dirName]) + "格式。");
            }

            //创建文件夹
            dirPath += dirName + "/";
            saveUrl += dirName + "/";
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            String ymd = DateTime.Now.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo);
            dirPath += ymd + "/";
            saveUrl += ymd + "/";
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            String newFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo) + fileExt;
            String filePath = dirPath + newFileName;
            imgFile.SaveAs(filePath);

            //压缩图片
            string fileUrl = saveUrl + newFileName;
            if (extTable["image"].ToString().Split(',').Contains(fileExt))
            {
                fileUrl = ZGD.Common.Thumbnail.CreateThumbImg(fileUrl, 800, 600, "W");
            }


            Hashtable hash = new Hashtable();
            hash["error"] = 0;
            //hash["url"] = "http://" + ZGD.Common.DTKeys.Web + fileUrl;
            hash["url"] = fileUrl;
            context.Response.AddHeader("Content-Type", "application/json; charset=UTF-8");
            context.Response.Write(hash.ToJson());
            context.Response.End();
        }

        private void showError(string message)
        {
            Hashtable hash = new Hashtable();
            hash["error"] = 1;
            hash["message"] = message;
            context.Response.AddHeader("Content-Type", "application/json; charset=UTF-8");
            context.Response.Write(hash.ToJson());
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}