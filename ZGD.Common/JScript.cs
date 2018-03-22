//=============================================================================
//
// Project:客户端JS处理
// Author:优纳科技 LJ
// Data:2010-5-12 编写时间
// Updated:2010-5-12 修改时间
// Remark:客户端JS处理
//
//=============================================================================
using System;
using System.Web;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Web.Security;
using System.Web.UI;
using System.Runtime.Serialization.Json;
using System.IO;

namespace ZGD.Common
{
    /// <summary>
    /// JS处理公共类
    /// </summary>
    public class JScript
    {
        public static void PageReload(System.Web.UI.Page page)
        {
            page.RegisterClientScriptBlock("reloadPage", "<script type=\"text/javascript\">window.location.reload();</script>");
        }

        public static void PageBack(System.Web.UI.Page page)
        {
            page.RegisterClientScriptBlock("reloadPage", "<script type=\"text/javascript\">window.history.back();</script>");
        }

        /// <summary>
        /// 提示信息
        /// 默认脚本标识 “alertMsg”
        /// </summary>
        /// <param name="msg"></param>
        public static void AlertMsg(System.Web.UI.Page page, string msg, bool isTop)
        {
            if (!string.IsNullOrEmpty(msg))
            {
                if (isTop)
                    page.RegisterClientScriptBlock("alertMsg", "<script type=\"text/javascript\">alert('" + msg + "');</script>");
                else
                    page.RegisterStartupScript("alertMsg", "<script type=\"text/javascript\">alert('" + msg + "');</script>");
            }
        }
        /// <summary>
        /// 提示信息
        /// 默认脚本标识 “alertMsg”
        /// </summary>
        /// <param name="msg"></param>
        public static void AlertMsg(System.Web.UI.Page page, string msg, bool isTop, String URL)
        {
            if (!string.IsNullOrEmpty(msg))
            {
                if (isTop)
                    page.RegisterClientScriptBlock("alertMsg", "<script type=\"text/javascript\">alert('" + msg + "');window.location.href='" + URL + "';</script>");
                else
                    page.RegisterStartupScript("alertMsg", "<script type=\"text/javascript\">alert('" + msg + "');window.location.href='" + URL + "';</script>");
            }
        }
        /// <summary>
        /// 提示信息
        /// 自定义脚本标识
        /// </summary>
        /// <param name="msg"></param>
        public static void AlertMsg(System.Web.UI.Page page, string key, string msg, bool isTop)
        {
            if (!string.IsNullOrEmpty(msg))
            {
                if (isTop)
                    page.RegisterClientScriptBlock(key, "<script type=\"text/javascript\">alert('" + msg + "');</script>");
                else
                    page.RegisterStartupScript(key, "<script type=\"text/javascript\">alert('" + msg + "');</script>");
            }
        }

        /// <summary>
        /// 提示信息
        /// 非提示框，向页面居中位置植入DIV提示层
        /// back：是否返回跳转之前页面
        /// </summary>
        /// <param name="msg"></param>
        public static void AlertDiv(System.Web.UI.Page page, string key, string msg, bool isTop)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("");
            if (!string.IsNullOrEmpty(msg))
            {
                sb.Append("");
                if (isTop)
                    page.RegisterClientScriptBlock(key, "<script type=\"text/javascript\">" + sb.ToString() + "</script>");
                else
                    page.RegisterStartupScript(key, "<script type=\"text/javascript\">" + sb.ToString() + "</script>");
            }
        }

        /// <summary>
        /// 向页面植入脚本
        /// </summary>
        /// <param name="msg"></param>
        public static void CreatePageScript(System.Web.UI.Page page, string key, string script, bool isTop)
        {
            if (!string.IsNullOrEmpty(key))
            {
                if (isTop)
                    page.RegisterClientScriptBlock(key, "<script type=\"text/javascript\">" + script + "</script>");
                else
                    page.RegisterStartupScript(key, "<script type=\"text/javascript\">" + script + "</script>");
            }
        }

        /// <summary>
        /// 编码 适用于中文 服务器端 客户端编码一致
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string JsEncode(string content)
        {
            return Microsoft.JScript.GlobalObject.escape(content);
        }

        /// <summary>
        /// 解码 适用于中文 服务器端 客户端编码一致
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string JsDecode(string content)
        {
            return Microsoft.JScript.GlobalObject.unescape(content);
        }

        /// <summary>
        /// Datatable转换json 
        /// 未加对象名
        /// </summary>
        /// <param name="dt">Datatable</param>
        /// <returns></returns>
        public static string DataTableToJson(System.Data.DataTable dt)
        {
            StringBuilder Json = new StringBuilder();
            if (dt.Rows.Count > 0)
            {
                Json.Append("[");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Json.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        Json.Append("\"" + dt.Columns[j].ColumnName.ToString().Trim() + "\":\"" + JsonCharFilter(dt.Rows[i][j].ToString().Trim()) + "\"");
                        if (j < dt.Columns.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
                Json.Append("]");
            }
            return Json.ToString();
        }

        /// <summary>
        /// Datatable转换json
        /// </summary>
        /// <param name="jsonName">Json数据的根元素名称</param>
        /// <param name="dt">Datatable</param>
        /// <returns></returns>
        public static string DataTableToJson(string jsonName, System.Data.DataTable dt)
        {
            StringBuilder Json = new StringBuilder();
            if (dt.Rows.Count > 0)
            {
                Json.Append("{\"" + jsonName + "\":[");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Json.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        Json.Append("\"" + dt.Columns[j].ColumnName.ToString().Trim() + "\":\"" + JsonCharFilter(dt.Rows[i][j].ToString().Trim()) + "\"");
                        if (j < dt.Columns.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
                Json.Append("]}");
            }
            return Json.ToString();
        }

        /// <summary>
        /// JSON序列化    
        /// </summary>
        /// <typeparam name="T">要序列化的数据类型</typeparam>
        /// <param name="t">序列化数据</param>
        /// <returns></returns>
        public static string JsonSerializer<T>(T t)
        {
            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                MemoryStream ms = new MemoryStream();
                ser.WriteObject(ms, t);
                string jsonString = Encoding.UTF8.GetString(ms.ToArray());
                ms.Close();
                //替换Json的Date字符串        
                string p = @"\\/Date\((\d+)\+\d+\)\\/";
                System.Text.RegularExpressions.MatchEvaluator matchEvaluator = new System.Text.RegularExpressions.MatchEvaluator(RegexHandler.ConvertJsonDateToDateString);
                System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(p);
                jsonString = reg.Replace(jsonString, matchEvaluator);
                return jsonString;
            }
            catch
            {
                return "";
            }

        }

        /// <summary>
        ///  JSON反序列化  
        /// </summary>
        /// <typeparam name="T">反序列化转换类型</typeparam>
        /// <param name="jsonString">反序列化字符串</param>
        /// <returns></returns>
        public static T JsonDeserialize<T>(string jsonString)
        {
            try
            {
                //将"yyyy-MM-dd HH:mm:ss"格式的字符串转为"\/Date(1294499956278+0800)\/"格式       
                string p = @"\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2}";
                System.Text.RegularExpressions.MatchEvaluator matchEvaluator = new System.Text.RegularExpressions.MatchEvaluator(RegexHandler.ConvertDateStringToJsonDate);
                System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(p);
                jsonString = reg.Replace(jsonString, matchEvaluator);
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
                T obj = (T)ser.ReadObject(ms);
                return obj;
            }
            catch
            {
                return default(T);
            }
        }

        /// <summary>
        /// Json特符字符过滤，参见http://www.json.org/
        /// </summary>
        /// <param name="sourceStr">要过滤的源字符串</param>
        /// <returns>返回过滤的字符串</returns>
        public static string JsonCharFilter(string sourceStr)
        {
            sourceStr = sourceStr.Replace("\r\n", ";");
            sourceStr = sourceStr.Replace("\r", ";");
            sourceStr = sourceStr.Replace("\n", ";");
            sourceStr = sourceStr.Replace("\n", ";");
            sourceStr = sourceStr.Replace("\\", "");
            sourceStr = sourceStr.Replace("\b", "");
            sourceStr = sourceStr.Replace("\t", "");
            sourceStr = sourceStr.Replace("\f", "");
            sourceStr = sourceStr.Replace("\"", "“");
            return sourceStr;
        }

        /// <summary>
        /// Json特符字符过滤，参见http://www.json.org/
        /// </summary>
        /// <param name="sourceStr">要过滤的源字符串</param>
        /// <returns>返回过滤的字符串</returns>
        public static string JsonCharFilter2(string sourceStr)
        {
            sourceStr = sourceStr.Replace("\\", "\\\\");
            sourceStr = sourceStr.Replace("\b", "\\\b");
            sourceStr = sourceStr.Replace("\t", "\\\t");
            sourceStr = sourceStr.Replace("\n", "\\\n");
            sourceStr = sourceStr.Replace("\n", "\\\n");
            sourceStr = sourceStr.Replace("\f", "\\\f");
            sourceStr = sourceStr.Replace("\r", "\\\r");
            return sourceStr;
        }
    }
}
