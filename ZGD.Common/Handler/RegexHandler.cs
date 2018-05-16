//=============================================================================
//
// Project:正则表达式类
// Author: LJ
// Data:2010-4-28 编写时间
// Updated:2010-4-28 修改时间
// Remark:正则--公共方法
//
//=============================================================================
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections;

namespace ZGD.Common
{    
    public class RegexHandler
    {
        public static SortedList<string, string> RegexItem = new SortedList<string, string>();
        public static void InitRegexItem()
        {
            RegexItem.Add("intege", "^-?[1-9]\\d*$");//整数 
            RegexItem.Add("intege1","^[1-9]\\d*$");//正整数
            RegexItem.Add("intege2","^-[1-9]\\d*$");//负整数
            RegexItem.Add("num","^([+-]?)\\d*\\.?\\d+$");//数字
            RegexItem.Add("num1","^[1-9]\\d*|0$");//正数（正整数 + 0）
            RegexItem.Add("num2","^-[1-9]\\d*|0$");//负数（负整数 + 0）
            RegexItem.Add("decmal","^([+-]?)\\d*\\.\\d+$");//浮点数
            RegexItem.Add("decmal1","^[1-9]\\d*.\\d*|0.\\d*[1-9]\\d*$");//正浮点数
            RegexItem.Add("decmal2","^-([1-9]\\d*.\\d*|0.\\d*[1-9]\\d*)$");//负浮点数
            RegexItem.Add("decmal3","^-?([1-9]\\d*.\\d*|0.\\d*[1-9]\\d*|0?.0+|0)$");//浮点数
            RegexItem.Add("decmal4","^[1-9]\\d*.\\d*|0.\\d*[1-9]\\d*|0?.0+|0$");//非负浮点数（正浮点数 + 0）
            RegexItem.Add("decmal5","^(-([1-9]\\d*.\\d*|0.\\d*[1-9]\\d*))|0?.0+|0$");//非正浮点数（负浮点数 + 0）
            RegexItem.Add("email","^\\w+((-\\w+)|(\\.\\w+))*\\@[A-Za-z0-9]+((\\.|-)[A-Za-z0-9]+)*\\.[A-Za-z0-9]+$"); //邮件 
            RegexItem.Add("color","^[a-fA-F0-9]{6}$");//颜色
            RegexItem.Add("url","^http[s]?:\\/\\/([\\w-]+\\.)+[\\w-]+([\\w-./?%&=]*)?$");//url 
            RegexItem.Add("chinese","^[\\u4E00-\\u9FA5\\uF900-\\uFA2D]+$"); //仅中文 
            RegexItem.Add("ascii","^[\\x00-\\xFF]+$"); //仅ACSII字符
            RegexItem.Add("zipcode","^\\d{6}$"); //邮编
            RegexItem.Add("mobile","^(1)[0-9]{9}$"); //手机
            RegexItem.Add("ip4","^(25[0-5]|2[0-4]\\d|[0-1]\\d{2}|[1-9]?\\d)\\.(25[0-5]|2[0-4]\\d|[0-1]\\d{2}|[1-9]?\\d)\\.(25[0-5]|2[0-4]\\d|[0-1]\\d{2}|[1-9]?\\d)\\.(25[0-5]|2[0-4]\\d|[0-1]\\d{2}|[1-9]?\\d)$"); //ip地址
            RegexItem.Add("picture","(.*)\\.(jpg|bmp|gif|ico|pcx|jpeg|tif|png|raw|tga)$"); //图片
            RegexItem.Add("rar","(.*)\\.(rar|zip|7zip|tgz)$"); //压缩文件 
            RegexItem.Add("date","^\\d{4}(\\-|\\/|\\.)\\d{1,2}\\1\\d{1,2}$"); //日期 
            RegexItem.Add("qq","^[1-9]*[1-9][0-9]*$"); //QQ号码 
            RegexItem.Add("tel","^(([0\\+]\\d{2,3}-)?(0\\d{2,3})-)?(\\d{7,8})(-(\\d{3,}))?$");//电话号码的函数(包括验证国内区号,国际区号,分机号) 
            RegexItem.Add("username","^\\w+$"); //用来用户注册。匹配由数字、26个英文字母或者下划线组成的字符串 
            RegexItem.Add("letter","^[A-Za-z]+$");//字母 
            RegexItem.Add("letter_u","^[A-Z]+$");//大写字母 
            RegexItem.Add("letter_l","^[a-z]+$");//小写字母 
            RegexItem.Add("idcard","^[1-9]([0-9]{14}|[0-9]{17})$");//身份证 
            RegexItem.Add("ps_username","^[\\u4E00-\\u9FA5\\uF900-\\uFA2D_\\w]+$");//中文、字母、数字
        }


        private static Regex RegexBr = new Regex(@"(\r\n)", RegexOptions.IgnoreCase);


        public static string StrReplace(string matchStr, string replaceStr, string regexStr)
        {
           return  Regex.Replace(matchStr, regexStr, replaceStr);
        }
        /// <summary>
        /// 验证字符串
        /// </summary>
        /// <param name="matchStr">需验证文本</param>
        /// <param name="regexStr">正则表达式</param>
        /// <returns></returns>
        public static bool RegexStr(string matchStr, string regexStr)
        {
            Match m = Regex.Match(matchStr, regexStr);
            return m.Success;
        }


        /// <summary>
        /// 检测是否是正确的Url
        /// </summary>
        /// <param name="strUrl">要验证的Url</param>
        /// <returns>判断结果</returns>
        public static bool IsURL(string strUrl)
        {
            return Regex.IsMatch(strUrl, @"^(http|https)\://([a-zA-Z0-9\.\-]+(\:[a-zA-Z0-9\.&%\$\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|localhost|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{1,10}))(\:[0-9]+)*(/($|[a-zA-Z0-9\.\,\?\'\\\+&%\$#\=~_\-]+))*$");
        }

        /// <summary>
        /// 检测是否符合email格式
        /// </summary>
        /// <param name="strEmail">要判断的email字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsValidEmail(string strEmail)
        {
            return Regex.IsMatch(strEmail, @"^[\w\.]+([-]\w+)*@[A-Za-z0-9-_]+[\.][A-Za-z0-9-_]");
        }

        /// <summary>
        /// 检测是否有Sql危险字符
        /// </summary>
        /// <param name="str">要判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsSafeSqlString(string str)
        {
            return !Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
        }

        /// <summary>
        /// 检测是否有危险的可能用于链接的字符串
        /// </summary>
        /// <param name="str">要判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsSafeUserInfoString(string str)
        {
            return !Regex.IsMatch(str, @"^\s*$|^c:\\con\\con$|[%,\*" + "\"" + @"\s\t\<\>\&]|游客|^Guest");
        }

        /// <summary>
        /// 清除给定字符串中的回车及换行符
        /// </summary>
        /// <param name="str">要清除的字符串</param>
        /// <returns>清除后返回的字符串</returns>
        public static string ClearBR(string str)
        {
            Match m = null;

            for (m = RegexBr.Match(str); m.Success; m = m.NextMatch())
            {
                str = str.Replace(m.Groups[0].ToString(), "");
            }
            return str;
        }
        
        /// <summary>
        /// 返回匹配指定标签内的html
        /// </summary>
        /// <param name="matchStr">匹配文本</param>
        /// <param name="htmlTitle">html标签</param>
        /// <param name="replaceStr">替换文本</param>
        /// <returns></returns>
        public string Gettitle(string matchStr, string htmlTitle, string replaceStr)
        {
            Regex rg = new Regex("<" + htmlTitle + ">.*</" + htmlTitle + ">");
            return rg.Replace(matchStr, replaceStr);
        }

        /// <summary>
        /// 是否数字字符串
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsNumber(string inputData)
        {
            Match m = Regex.Match(inputData, @"^?\d+$");
            return m.Success;
        }

        /// <summary>
        /// 移除Html标记
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string RemoveHtml(string content)
        {
            return Regex.Replace(content, @"<[^>]*>", string.Empty, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 从HTML中获取文本,保留br,p,img
        /// </summary>
        /// <param name="HTML"></param>
        /// <returns></returns>
        public static string GetTextFromHTML(string html)
        {
            System.Text.RegularExpressions.Regex regEx = new System.Text.RegularExpressions.Regex(@"</?(?!br|/?p|img)[^>]*>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            return regEx.Replace(html, "");
        }

        /// <summary>
        /// 过滤HTML中的不安全标签
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string RemoveUnsafeHtml(string content)
        {
            content = Regex.Replace(content, @"(\<|\s+)o([a-z]+\s?=)", "$1$2", RegexOptions.IgnoreCase);
            content = Regex.Replace(content, @"(script|frame|form|meta|behavior|style)([\s|:|>])+", "$1.$2", RegexOptions.IgnoreCase);
            return content;
        }

        /// <summary>    
        /// 将Json序列化的时间由/Date(1294499956278+0800)转为字符串    
        /// </summary>    
        public static string ConvertJsonDateToDateString(Match m)
        {
            string result = string.Empty;
            DateTime dt = new DateTime(1970, 1, 1);
            dt = dt.AddMilliseconds(long.Parse(m.Groups[1].Value));
            dt = dt.ToLocalTime();
            result = dt.ToString("yyyy-MM-dd HH:mm:ss");
            return result;
        }

        /// <summary>    
        /// 将时间字符串转为Json时间  
        ///  </summary>   
        public static string ConvertDateStringToJsonDate(Match m)
        {
            string result = string.Empty;
            DateTime dt = DateTime.Parse(m.Groups[0].Value);
            dt = dt.ToUniversalTime();
            TimeSpan ts = dt - DateTime.Parse("1970-01-01");
            result = string.Format("\\/Date({0}+0800)\\/", ts.TotalMilliseconds);
            return result;
        }
        
        /// <summary>
        /// 是否手机号码
        /// </summary>
        /// <param name="input">要判断的手机号码</param>
        /// <returns></returns>
        public static bool IsMobile(string input)
        {
            if (!Regex.IsMatch(input, @"^[1][1-9]\d{9}$", RegexOptions.IgnoreCase))
                return false;
            if (input.Length == 11 && (input.StartsWith("13") || input.StartsWith("14") || input.StartsWith("15") || input.StartsWith("17") || input.StartsWith("18")))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 是否为邮箱
        /// </summary>
        /// <param name="input">要判断的邮件</param>
        /// <returns></returns>
        public static bool IsEmail(string input)
        {
            if (!Regex.IsMatch(input, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", RegexOptions.IgnoreCase))
                return false;
            return true;
        }

        /// <summary>  
        /// 获取文章中图片地址
        /// </summary>  
        /// <param name="html">文章内容</param>  
        /// <returns></returns>  
        public static List<string> GetImgList(string html)
        {
            List<string> imgs = new List<string>();
            Regex r = new Regex(@"<IMG[^>]+src=\s*(?:'(?<src>[^']+)'|""(?<src>[^""]+)""|(?<src>[^>\s]+))\s*[^>]*>", RegexOptions.IgnoreCase);
            MatchCollection mc = r.Matches(html);

            foreach (Match m in mc)
            {
                imgs.Add(m.Groups["src"].Value.ToLower());
            }
            return imgs;
        }
    }
}
