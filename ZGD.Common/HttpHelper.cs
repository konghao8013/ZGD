using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace ZGD.Common
{

    public class HttpHelper
    {
        /// <summary> 
        /// URL拼写完整性检查 
        /// </summary> 
        /// <param name="url">待检查的URL</param> 
        private static void UrlCheck(ref string url)
        {
            if (!url.StartsWith("http://") && !url.StartsWith("https://"))
                url = "http://" + url;
        }

        /// <summary> 
        /// GET请求 
        /// </summary> 
        /// <param name="url">请求地址</param> 
        /// <param name="param">参数</param> 
        /// <param name="onComplete">完成后执行的操作(可选参数，通过此方法可以获取到HTTP状态码)</param> 
        /// <returns>请求返回结果</returns> 
        public static string Get(string url, string param, Encoding encoding, Action<HttpStatusCode, string> onComplete = null)
        {
            UrlCheck(ref url);

            if (!string.IsNullOrEmpty(param))
                if (!param.StartsWith("?"))
                    param = "?" + param;

            var request = WebRequest.Create(url + param) as HttpWebRequest;
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";

            return HttpRequest(request, encoding, onComplete);
        }

        /// <summary> 
        /// 请求的主体部分（由此完成请求并返回请求结果） 
        /// </summary> 
        /// <param name="request">请求的对象</param> 
        /// <param name="onComplete">完成后执行的操作(可选参数，通过此方法可以获取到HTTP状态码)</param> 
        /// <returns>请求返回结果</returns> 
        private static string HttpRequest(HttpWebRequest request, Encoding encoding, Action<HttpStatusCode, string> onComplete = null)
        {
            HttpWebResponse response = null;

            try
            {
                response = request.GetResponse() as HttpWebResponse;
            }
            catch (WebException ex)
            {
                response = ex.Response as HttpWebResponse;
            }

            if (response == null)
            {
                if (onComplete != null)
                    onComplete(HttpStatusCode.NotFound, "请求远程返回为空");
                return null;
            }

            string result = string.Empty;
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), encoding))
            {
                result = reader.ReadToEnd();
            }

            if (onComplete != null)
                onComplete(response.StatusCode, result);

            return result;

        }

        /// <summary>
        /// 获取POST返回来的数据(默认UTF8编码格式)
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <returns></returns>
        public string PostInput(System.Web.HttpRequestBase request)
        {
            try
            {
                return PostInput(request, Encoding.UTF8);
            }
            catch (Exception ex)
            { throw ex; }
        }

        /// <summary>
        /// 获取POST返回来的数据
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <param name="encoding">编码格式</param>
        /// <returns></returns>
        public string PostInput(System.Web.HttpRequestBase request, Encoding encoding)
        {
            StringBuilder builder = new StringBuilder();
            try
            {
                using (System.IO.Stream s = request.InputStream)
                {
                    int count = 0;
                    byte[] buffer = new byte[1024];
                    while ((count = s.Read(buffer, 0, 1024)) > 0)
                    {
                        builder.Append(encoding.GetString(buffer, 0, count));
                    }
                    s.Flush();
                    s.Close();
                    s.Dispose();
                }
                return builder.ToString();
            }
            catch (Exception ex)
            { throw ex; }
        }

        /// <summary>
        /// POST返回数据(默认UTF8编码格式)
        /// </summary>
        /// <param name="response">response对象</param>
        /// <param name="content">内容</param>
        /// <returns></returns>
        public bool PostOutput(System.Web.HttpResponseBase response, string content)
        {
            try
            {
                return PostOutput(response, content, Encoding.UTF8);
            }
            catch (Exception ex)
            { throw ex; }
        }

        /// <summary>
        /// POST返回数据
        /// </summary>
        /// <param name="response">response对象</param>
        /// <param name="content">内容</param>
        /// <param name="encoding">编码格式</param>
        /// <returns></returns>
        public bool PostOutput(System.Web.HttpResponseBase response, string content, Encoding encoding)
        {
            StringBuilder builder = new StringBuilder();
            try
            {
                using (System.IO.Stream s = response.OutputStream)
                {
                    byte[] bys = encoding.GetBytes(content);
                    s.Write(bys, 0, bys.Length);
                    s.Flush();
                    s.Close();
                    s.Dispose();
                }
                return true;
            }
            catch (Exception ex)
            { throw ex; }
        }
    }
}
