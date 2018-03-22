//=============================================================================
//
// Project:字符串操作类
// Author: LJ
// Data:2010-4-28 编写时间
// Updated:2010-4-28 修改时间
// Remark:字符串操作--公共方法
//
//=============================================================================
using System;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Security;
using System.Security.Cryptography;
using System.Globalization;
using System.Configuration;
using System.Collections.Generic;
using System.Net;
using System.IO;

namespace ZGD.Common
{
    public class StringHandler
    {
        /// <summary>
        ///SQL注入过滤
        /// </summary>
        /// <param name="text">要过滤的字符串</param>
        /// <returns>如果参数存在不安全字符，则返回true</returns>
        public static bool SqlFilter(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return false;

            string[] keys = { "exec", "insert", "select", "delete", "update", "drop", "master", "truncate", "declare" };
            foreach (string i in keys)
            {
                if ((text.ToLower().IndexOf(i + " ") > -1) || (text.ToLower().IndexOf(" " + i) > -1))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary> 
        /// 转换人民币大小金额 
        /// </summary> 
        /// <param name="num">金额</param> 
        /// <returns>返回大写形式</returns> 
        public static string CmycurD(decimal num)
        {
            string str1 = "零壹贰叁肆伍陆柒捌玖";            //0-9所对应的汉字 
            string str2 = "万仟佰拾亿仟佰拾万仟佰拾元角分"; //数字位所对应的汉字 
            string str3 = "";    //从原num值中取出的值 
            string str4 = "";    //数字的字符串形式 
            string str5 = "";  //人民币大写金额形式 
            int i;    //循环变量 
            int j;    //num的值乘以100的字符串长度 
            string ch1 = "";    //数字的汉语读法 
            string ch2 = "";    //数字位的汉字读法 
            int nzero = 0;  //用来计算连续的零值是几个 
            int temp;            //从原num值中取出的值 

            num = Math.Round(Math.Abs(num), 2);    //将num取绝对值并四舍五入取2位小数 
            str4 = ((long)(num * 100)).ToString();        //将num乘100并转换成字符串形式 
            j = str4.Length;      //找出最高位 
            if (j > 15) { return "溢出"; }
            str2 = str2.Substring(15 - j);   //取出对应位数的str2的值。如：200.55,j为5所以str2=佰拾元角分 

            //循环取出每一位需要转换的值 
            for (i = 0; i < j; i++)
            {
                str3 = str4.Substring(i, 1);          //取出需转换的某一位的值 
                temp = Convert.ToInt32(str3);      //转换为数字 
                if (i != (j - 3) && i != (j - 7) && i != (j - 11) && i != (j - 15))
                {
                    //当所取位数不为元、万、亿、万亿上的数字时 
                    if (str3 == "0")
                    {
                        ch1 = "";
                        ch2 = "";
                        nzero = nzero + 1;
                    }
                    else
                    {
                        if (str3 != "0" && nzero != 0)
                        {
                            ch1 = "零" + str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            ch1 = str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                    }
                }
                else
                {
                    //该位是万亿，亿，万，元位等关键位 
                    if (str3 != "0" && nzero != 0)
                    {
                        ch1 = "零" + str1.Substring(temp * 1, 1);
                        ch2 = str2.Substring(i, 1);
                        nzero = 0;
                    }
                    else
                    {
                        if (str3 != "0" && nzero == 0)
                        {
                            ch1 = str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            if (str3 == "0" && nzero >= 3)
                            {
                                ch1 = "";
                                ch2 = "";
                                nzero = nzero + 1;
                            }
                            else
                            {
                                if (j >= 11)
                                {
                                    ch1 = "";
                                    nzero = nzero + 1;
                                }
                                else
                                {
                                    ch1 = "";
                                    ch2 = str2.Substring(i, 1);
                                    nzero = nzero + 1;
                                }
                            }
                        }
                    }
                }
                if (i == (j - 11) || i == (j - 3))
                {
                    //如果该位是亿位或元位，则必须写上 
                    ch2 = str2.Substring(i, 1);
                }
                str5 = str5 + ch1 + ch2;

                if (i == j - 1 && str3 == "0")
                {
                    //最后一位（分）为0时，加上“整” 
                    str5 = str5 + '整';
                }
            }
            if (num == 0)
            {
                str5 = "零元整";
            }
            return str5;
        }

        /// <summary>
        /// 获取banner类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetBannerType(string type)
        {
            switch (type)
            {
                case "1":
                    return "首页轮播图";
                case "2":
                    return "页面广告";
                case "3":
                    return "";
            }
            return "未知";
        }

        /**/
        /// <summary> 
        /// 一个重载，将字符串先转换成数字在调用CmycurD(decimal num) 
        /// </summary> 
        /// <param name="num">用户输入的金额，字符串形式未转成decimal</param> 
        /// <returns></returns> 
        public static string CmycurD(string numstr)
        {
            try
            {
                decimal num = Convert.ToDecimal(numstr);
                return CmycurD(num);
            }
            catch
            {
                return "非数字形式！";
            }
        }

        /// <summary>
        /// 构造提交表单HTML数据
        /// 立即提交
        /// </summary>
        /// <param name="fromID">表单ID</param>
        /// <param name="sParaTemp">请求参数数组</param>
        /// <param name="gateway">网关地址</param>
        /// <param name="charset">编码格式 gbk utf-8</param>
        /// <param name="strMethod">提交方式。两个值可选：post、get</param>
        /// <returns>提交表单HTML文本</returns>
        public static string BuildFormHtml(string fromID, Dictionary<string, string> sParaTemp, string gateway, string strMethod)
        {
            StringBuilder sbHtml = new StringBuilder();

            sbHtml.Append("<form id='" + fromID + "' name='" + fromID + "' action='" + gateway + "' method='" + strMethod.ToLower().Trim() + "'>");

            foreach (KeyValuePair<string, string> temp in sParaTemp)
            {
                sbHtml.Append("<input type='hidden' name='" + temp.Key + "' value='" + temp.Value + "'/>");
            }

            //submit按钮控件请不要含有name属性
            sbHtml.Append("<input type='submit' value='submit' style='display:none;'></form>");

            sbHtml.Append("<script>document.forms['" + fromID + "'].submit();</script>");

            return sbHtml.ToString();
        }

        /// <summary>
        /// 构造提交表单HTML数据
        /// </summary>
        /// <param name="fromID">表单ID</param>
        /// <param name="sParaTemp">请求参数数组</param>
        /// <param name="gateway">网关地址</param>
        /// <param name="charset">编码格式 gbk utf-8</param>
        /// <param name="strMethod">提交方式。两个值可选：post、get</param>
        /// <param name="btnName">提交按钮名称</param>
        /// <param name="classStr">提交按钮样式</param>
        /// <returns>提交表单HTML文本</returns>
        public static string BuildFormHtml(string fromID, Dictionary<string, string> sParaTemp, string gateway, string strMethod, string btnName, string classStr)
        {
            StringBuilder sbHtml = new StringBuilder();

            sbHtml.Append("<form id='" + fromID + "' name='" + fromID + "' action='" + gateway + "' method='" + strMethod.ToLower().Trim() + "'>");

            foreach (KeyValuePair<string, string> temp in sParaTemp)
            {
                sbHtml.Append("<input type='hidden' name='" + temp.Key + "' value='" + temp.Value + "'/>");
            }

            //submit按钮控件请不要含有name属性
            sbHtml.Append("<input type='submit' value='" + btnName + "' class=\"" + classStr + "\"></form>");

            return sbHtml.ToString();
        }

        ///   <summary>   
        ///   去除HTML标记   
        ///   </summary>   
        ///   <param   name="NoHTML">包括HTML的源码   </param>   
        ///   <returns>已经去除后的文字</returns>   
        public static string NoHTML(string Htmlstring)
        {
            if (!string.IsNullOrEmpty(Htmlstring))
            {
                //删除脚本   
                Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
                //删除HTML   
                Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);

                Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "   ", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"\[backcolor=rgb(.*?)\]", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"\[color=rgb(.*?)\]", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"\[(.*?)\]", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"\[|\]", "", RegexOptions.IgnoreCase);

                Htmlstring.Replace("<", "");
                Htmlstring.Replace(">", "");
                Htmlstring.Replace("\r\n", "");
                Htmlstring = Htmlstring.Replace("'", "‘");
            }
            return Htmlstring;
        }

        /// <summary>
        /// 获取session数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetSession(string key)
        {
            if (HttpContext.Current.Session[key] != null)
                return HttpContext.Current.Session[key].ToString();
            else
                return "";
        }

        /// <summary>
        /// 获取request数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetRequest(string key)
        {
            if (HttpContext.Current.Request[key] != null)
                return HttpContext.Current.Request[key].ToString();
            else
                return "";
        }

        /// <summary>
        /// 删除字符串尾部的回车/换行/空格
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RTrim(string str)
        {
            for (int i = str.Length; i >= 0; i--)
            {
                if (str[i].Equals(" ") || str[i].Equals("\r") || str[i].Equals("\n"))
                {
                    str.Remove(i, 1);
                }
            }
            return str;
        }

        /// <summary>
        /// 删除字符串尾部的空格
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string DelTrim(string str)
        {
            return str.Trim();
        }

        /// <summary>
        /// 判断字符空
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static bool CheckStringNull(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                return true;
            else
                return false;
        }

        /// <summary>
        /// 编码
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string EnCode(string content)
        {
            return HttpUtility.HtmlEncode(content);
        }

        /// <summary>
        /// 解码
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string DeCode(string content)
        {
            return HttpUtility.HtmlDecode(content);
        }

        /// <summary>
        /// URL编码
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string UrlEnCode(string url)
        {
            return HttpUtility.UrlEncode(url);
        }

        /// <summary>
        /// URL解码
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string UrlDeCode(string url)
        {
            return HttpUtility.UrlDecode(url);
        }

        /// <summary>
        /// MD5函数
        /// </summary>
        /// <param name="str">原始字符串</param>
        /// <returns>MD5结果</returns>
        public static string MD5(string str)
        {
            byte[] b = Encoding.UTF8.GetBytes(str);
            b = new MD5CryptoServiceProvider().ComputeHash(b);
            string ret = "";
            for (int i = 0; i < b.Length; i++)
                ret += b[i].ToString("x").PadLeft(2, '0');

            return ret;
        }

        /// <summary>
        /// SHA256函数
        /// </summary>
        /// /// <param name="str">原始字符串</param>
        /// <returns>SHA256结果</returns>
        public static string SHA256(string str)
        {
            byte[] SHA256Data = Encoding.UTF8.GetBytes(str);
            SHA256Managed Sha256 = new SHA256Managed();
            byte[] Result = Sha256.ComputeHash(SHA256Data);
            return Convert.ToBase64String(Result);  //返回长度为44字节的字符串
        }

        /// <summary>
        /// 生成一个随机数唯一标识
        /// </summary>
        /// <param name="maxLengh">最小长度</param>
        /// <param name="maxLengh">最大长度</param>
        /// <returns></returns>
        public static string CreateRandNumStr(int minLenght, int maxLengh)
        {
            Random rd = new Random(Guid.NewGuid().GetHashCode());
            return rd.Next(minLenght, maxLengh).ToString();
        }

        /// <summary>
        /// 生成一个随机数唯一标识
        /// </summary>
        /// <param name="maxLengh">最小长度</param>
        /// <param name="maxLengh">最大长度</param>
        /// <returns></returns>
        public static int CreateRandNum(int minLenght, int maxLengh)
        {
            Random rd = new Random(Guid.NewGuid().GetHashCode());
            return rd.Next(minLenght, maxLengh);
        }

        /// <summary>
        /// 返回字符串真实长度, 1个汉字长度为2
        /// </summary>
        /// <returns>字符长度</returns>
        public static int GetStringLength(string str)
        {
            return Encoding.Default.GetBytes(str).Length;
        }

        /// <summary>
        /// 获取相关配置
        /// </summary>
        /// <returns></returns>
        public static string GetConfigInfo(string key)
        {
            return ConfigurationManager.AppSettings[key].ToString();
        }

        /// <summary>
        /// 获取客户端网络IP
        /// </summary>
        /// <returns></returns>
        public static string GetClientIP()
        {
            string clientIP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            if (!String.IsNullOrEmpty(HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]))
            {
                clientIP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            }
            return clientIP;
        }

        /// <summary>
        /// 截取字符为数组 用于模版解析 字符拆分
        /// </summary>
        /// <param name="srcString">检测字符</param>
        /// <param name="s">拆分字符</param>
        /// <returns></returns>
        public static string[] SplitString(string srcString, char s)
        {
            if (!string.IsNullOrEmpty(srcString))
                return srcString.Split(s);
            else
                return null;
        }

        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="str">检测字符</param>
        /// <param name="length">指定长度</param>
        /// <param name="defaultStr">省略代替字符串</param>
        /// <returns></returns>
        public static string SubStr(string srcString, int length, string replaceString)
        {
            string myResult = srcString;
            if (!string.IsNullOrWhiteSpace(srcString) && length >= 0)
            {
                byte[] bsSrcString = Encoding.Default.GetBytes(srcString);

                //当字符串长度大于起始位置
                if (bsSrcString.Length > 0)
                {
                    int p_EndIndex = bsSrcString.Length;

                    //当要截取的长度在字符串的有效长度范围内
                    if (bsSrcString.Length > length)
                    {
                        p_EndIndex = length + 0;
                    }
                    else
                    {   //当不在有效范围内时,只取到字符串的结尾

                        length = bsSrcString.Length;
                        replaceString = "";
                    }

                    int nRealLength = length;
                    int[] anResultFlag = new int[length];
                    byte[] bsResult = null;

                    int nFlag = 0;
                    for (int i = 0; i < p_EndIndex; i++)
                    {
                        if (bsSrcString[i] > 127)
                        {
                            nFlag++;
                            if (nFlag == 3)
                                nFlag = 1;
                        }
                        else
                            nFlag = 0;

                        anResultFlag[i] = nFlag;
                    }

                    if ((bsSrcString[p_EndIndex - 1] > 127) && (anResultFlag[length - 1] == 1))
                        nRealLength = length + 1;

                    bsResult = new byte[nRealLength];

                    Array.Copy(bsSrcString, 0, bsResult, 0, nRealLength);

                    myResult = Encoding.Default.GetString(bsResult);
                    myResult = myResult + replaceString;
                }
            }

            return myResult;
        }

        /// <summary>
        /// 字符串清理
        /// </summary>
        /// <param name="inputString">检测字符</param>
        /// <param name="maxLength">检测长度</param>
        /// <returns></returns>
        public static string InputText(string inputString, int maxLength)
        {
            StringBuilder retVal = new StringBuilder();
            // 检查是否为空
            if (!string.IsNullOrEmpty(inputString))
            {
                inputString = inputString.Trim();

                //检查长度
                if (inputString.Length > maxLength)
                    inputString = inputString.Substring(0, maxLength);

                //替换危险字符
                for (int i = 0; i < inputString.Length; i++)
                {
                    switch (inputString[i])
                    {
                        case '"':
                            retVal.Append("&quot;");
                            break;
                        case '<':
                            retVal.Append("&lt;");
                            break;
                        case '>':
                            retVal.Append("&gt;");
                            break;
                        default:
                            retVal.Append(inputString[i]);
                            break;
                    }
                }
                retVal.Replace("'", "");// 替换单引号
            }
            return retVal.ToString();
        }

        /// <summary>
        /// 数组转换字符
        /// </summary>
        /// <param name="arry"></param>
        /// <returns></returns>
        public static string ArryToString(string[] arry)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            for (int i = 0; i < arry.Length; i++)
            {
                sb.Append("\"" + arry[i] + "\"");
                if (i < arry.Length - 1)
                {
                    sb.Append(",");
                }
            }
            sb.Append("]");
            return sb.ToString();
        }

        /// <summary>
        /// 文件大小转换
        /// </summary>
        /// <param name="fileSize">fileSize long型</param>
        /// <returns>returns string</returns>
        public static string FormatFileSize(long fileSize)
        {
            double num = Convert.ToDouble(fileSize);
            while (num >= 1024.0)
            {
                if (num < 1048576.0)
                {
                    return string.Format("{0:N2} KB", num / 1024.0);
                }
                if ((((uint)fileSize) + ((uint)fileSize)) >= 0)
                {
                    if (((uint)fileSize & 0) != 0)
                    {
                    }
                    if (num < 1073741824.0)
                    {
                        return string.Format("{0:N2} MB", num / 1048576.0);
                    }
                    return string.Format("{0:N2} GB", num / 1073741824.0);
                }
            }
            return string.Format("{0:N0} B", num);
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="input">加密字符</param>
        /// <returns></returns>
        public static string CreateMd5Hash(string input)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();

            // Convert the input string to a byte array and compute the hash.   
            byte[] buf = System.Text.Encoding.UTF8.GetBytes(input); //utf-8数据 
            //byte[] buf = System.Text.Encoding.Default.GetBytes(input);//gb2312 吧，应该！看服务器语言定  

            byte[] data = md5Hasher.ComputeHash(buf);
            // Create a new Stringbuilder to collect the bytes     
            // and create a string.     
            StringBuilder sBuilder = new StringBuilder();
            // Loop through each byte of the hashed data      
            // and format each one as a hexadecimal string.     
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        /// <summary>
        /// 获取各个类型图片地址
        /// </summary>
        /// <param name="imgurl">原图片地址</param>
        /// <param name="t">类别，如,small,medium</param>
        /// <returns></returns>
        public static string GetImage(string imgurl, string t)
        {
            if (string.IsNullOrEmpty(imgurl)) return string.Empty;

            string fn = Path.GetFileName(imgurl.ToString());
            string dir = Path.GetDirectoryName(imgurl.ToString());
            dir = dir.Replace(@"\", "/");
            return dir + "/" + t + "/" + fn;
        }

        public static string Left(string strSrc, int iCount)
        {
            if (strSrc == null || strSrc.Length <= iCount)
                return strSrc;
            return strSrc.Substring(0, iCount);
        }

        public static string Right(string strSrc, int iCount)
        {
            if (strSrc == null || strSrc.Length <= iCount)
                return strSrc;
            return strSrc.Substring(strSrc.Length - iCount);
        }

        public static string Replace(string str, string reStr, string valStr)
        {
            if (!string.IsNullOrWhiteSpace(str))
            {
                return str.Replace(reStr, valStr);
            }
            return "";
        }

        /// <summary>
        /// 获取指定时间的 周几
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetDayOfWeek(DateTime dt)
        {
            string week = "未知";
            switch (DateTime.Today.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    week = "星期一";
                    break;
                case DayOfWeek.Tuesday:
                    week = "星期二";
                    break;
                case DayOfWeek.Wednesday:
                    week = "星期三";
                    break;
                case DayOfWeek.Thursday:
                    week = "星期四";
                    break;
                case DayOfWeek.Friday:
                    week = "星期五";
                    break;
                case DayOfWeek.Saturday:
                    week = "星期六";
                    break;
                case DayOfWeek.Sunday:
                    week = "星期日";
                    break;
            }
            return week;
        }
    }
}
