using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;

namespace ZGD.Common
{
    public class LogHepler
    {
        private static LogHepler instance = new LogHepler();
        public static LogHepler Instance
        {
            get { return instance; }
        }

        public void WriteFile(string FileText, string FileName)
        {
            var DirectoryDir = "//Log//" + DateTime.Now.ToString("yyyyMMdd") + "//";
            var FileDir = FileName + DateTime.Now.Ticks + ".txt";
            var AllDir = System.Web.HttpContext.Current.Server.MapPath(DirectoryDir + FileDir);
            CreateDis(DirectoryDir);
            using (System.IO.FileStream fs = new System.IO.FileStream(AllDir, System.IO.FileMode.OpenOrCreate))
            {
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(fs))
                {
                    lock (sw)
                    {
                        sw.WriteLine(DateTime.Now.ToString());
                        sw.Write(FileText);
                    }
                }
            }
        }

        //public bool WriteLog() { }

        private void CreateDis(string dir)
        {
            Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(dir));
        }

        public string ReadFile(string path)
        {
            string returnValue = "";
            using (StreamReader sr = File.OpenText(path))
            {
                returnValue = sr.ReadToEnd();
            }

            return returnValue;
        }

        /// <summary>
        /// 日志记录
        /// </summary>
        /// <param name="url">错误跳转路径</param>
        /// <param name="isRedirect">是否跳转</param>
        public static void WriteLog(string content)
        {
            string fileName = "tmp" + DateTime.Now.ToString("yyyy-MM-dd");
            string path = HttpContext.Current.Server.MapPath("~/Log/Errorlog/");

            //将信息内容写入文件
            FileDom.WriteStream("【" + DateTime.Now + "】" + content + "\n\n", Encoding.Unicode, path, fileName + ".txt", true);
        }

        /// <summary>
        /// 错误日志记录
        /// </summary>
        /// <param name="url">错误跳转路径</param>
        /// <param name="isRedirect">是否跳转</param>
        public static void ErrorLog(Exception ex, string url = "", bool isRedirect = false)
        {
            if (ex != null)
            {
                string fileName = "Errolog" + DateTime.Now.ToString("yyyy-MM-dd");
                string path = HttpContext.Current.Server.MapPath("~/Log/Errorlog/");
                StringBuilder sb = new StringBuilder();

                sb.Append("*************************************************************************************\n");
                sb.Append("======================发生时间=======================\n" + DateTime.Now + "\n");
                sb.Append("======================客服端IP=======================\n" + StringHandler.GetClientIP() + "\n");
                sb.Append("======================客服端DNS======================\n" + HttpContext.Current.Request.UserHostName + "\n");
                sb.Append("======================客服端代理信息=================\n" + HttpContext.Current.Request.UserAgent + "\n");
                sb.Append("======================浏览器类型=====================\n" + HttpContext.Current.Request.Browser.Type + "\n");
                sb.Append("======================操作系统=======================\n" + HttpContext.Current.Request.Browser.Platform + "\n");
                sb.Append("======================异常地址=======================\n" + HttpContext.Current.Request.Url + "\n");
                sb.Append("======================异常来源=======================\n" + ex.Source + "\n");
                sb.Append("======================异常信息=======================\n" + ex.Message + "\n");
                sb.Append("======================详细信息=======================\n" + ex.ToString() + "\n");
                sb.Append("*************************************************************************************\n\n");

                //将信息内容写入文件
                FileDom.WriteStream(sb.ToString(), Encoding.Unicode, path, fileName + ".txt", true);
                if (isRedirect)
                    HttpContext.Current.Response.Redirect(url);
            }
        }
    }
}
