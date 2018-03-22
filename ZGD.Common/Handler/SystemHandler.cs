using System;
using System.Collections.Generic;
using System.Web;
using System.Text;

namespace ZGD.Common
{
    public class SystemHandler
    {

        /// <summary>
        /// 验证短信验证码
        /// </summary>
        /// <returns></returns>
        public static bool CheckSmsCode(string smsCode)
        {
            if (HttpContext.Current.Session[DTKeys.SESSION_SMS_CODE] != null)
            {
                string code = HttpContext.Current.Session[DTKeys.SESSION_SMS_CODE].ToString();
                code = code.ToLower();
                if (code.Equals(smsCode.Trim().ToLower()))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 验证图片验证码
        /// </summary>
        /// <returns></returns>
        public static bool CheckImgCode(string smsCode)
        {
            if (HttpContext.Current.Session[DTKeys.SESSION_CODE] != null)
            {
                string code = HttpContext.Current.Session[DTKeys.SESSION_CODE].ToString();
                code = code.ToLower();
                if (code.Equals(smsCode.Trim().ToLower()))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 获取投诉类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetComplainType(int type)
        {
            switch (type)
            {
                case 1:
                    return "设计投诉";
                case 2:
                    return "施工投诉";
                default:
                    return "其他投诉";
            }
        }
        /// <summary>
        /// 验证验证码正确性
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static bool CheckCode(string code)
        {
            bool bl = false;
            if (!string.IsNullOrEmpty(code))
            {
                if (HttpContext.Current.Session["CheckCode"] != null)
                {
                    if (code.Trim().ToLower().Equals(HttpContext.Current.Session["CheckCode"].ToString().Trim().ToLower()))
                    {
                        bl = true;
                    }
                }
            }
            return bl;
        }

        /// <summary>
        /// 清除登录状态
        /// </summary>
        public static void RemoveWebState(string domain)
        {
            ZGD.Common.CookieHelper.RemoteCookieValue("uid", domain);
            ZGD.Common.CookieHelper.RemoteCookieValue("username", domain);
        }

        /// <summary>
        /// 获取会员ID
        /// </summary>
        /// <returns></returns>
        public static int GetMemberID()
        {
            int uID = 0;
            string uid = ZGD.Common.CookieHelper.GetCookieValue("uid");
            if (!string.IsNullOrEmpty(uid))
            {
                uID = Convert.ToInt32(uid);
            }
            return uID;
        }

        /// <summary>
        /// 获取会员账号
        /// </summary>
        /// <returns></returns>
        public static string GetMemberName()
        {
            return ZGD.Common.CookieHelper.GetCookieValue("username");
        }

        /// <summary>
        /// 创建密码密文
        /// MD5加密
        /// </summary>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        public static string CreatePwdString(string pwd)
        {
            return StringHandler.MD5(pwd);
        }

        /// <summary>
        /// 创建密码密文
        /// </summary>
        /// <param name="pwd">密码</param>
        /// <param name="key">标识</param>
        /// <returns></returns>
        public static string CreatePwdString(string pwd, string key)
        {
            return StringHandler.MD5(StringHandler.MD5(StringHandler.MD5(pwd) + key));
        }

        /// <summary>
        /// 广告设置
        /// </summary>
        /// <param name="adType">广告类型</param>
        /// <param name="pathName">文件路径(相对于网站的根目录)如"File/AD/201006/xxx.gif"</param>
        /// <param name="adDescription">描述</param>
        /// <returns></returns>
        public static string GetAdHtml(string adType, string pathName, string adDescription, string width, string height, string link, bool isBlank)
        {
            string adStr = string.Empty;
            //设置时间版本（防止缓存）
            string nowString = DateTimeHelper.FormatDate(DateTime.Now, "yyyyMMddhhmmss");
            string blank = isBlank == true ? "target=\"_blank\"" : "";
            pathName = XMLDom.ReadConfig("~/Config/SiteBase.config", "Domain") + pathName;

            switch (adType)
            {
                //img
                case "1":
                    adStr = "<a href=\"" + link + "\" " + blank + "><img src=\"" + pathName + "?v=" + nowString + "\" alt=\"" + adDescription + "\" width=\"" + width + "\" height=\"" + height + "\"/></a>";
                    break;
                //flash
                case "2":
                    adStr = "<object classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=5,0,0,0\" width=\"" + width + "\" height=\"" + height + "\"><param name=\"movie\" value=\"" + pathName + "?v=" + nowString + "\" /><param name=\"quality\" value=\"high\" /><param name=\"wmode\" value=\"transparent\"><embed src=\"" + pathName + "?v=" + nowString + "\" quality=\"high\" pluginspage=\"http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash\" type=\"application/x-shockwave-flash\" width=\"" + width + "\" height=\"" + height + "\" wmode=\"transparent\"></embed></object>";
                    break;
            }
            return StringHandler.EnCode(adStr);
        }

        /// <summary>
        /// 获取活动状态
        /// </summary>
        /// <returns></returns>
        public static string GetActivityStatus(int state)
        {
            string planState = "已结束";
            switch (state)
            {
                case 0:
                    planState = "未开始";
                    break;
                case 1:
                    planState = "进行中";
                    break;
                case -1:
                    planState = "已结束";
                    break;
            }
            return planState;
        }

        /// <summary>
        /// 创建编号
        /// </summary>
        /// <param name="company">公司名缩写（2位）</param>
        /// <param name="max">最大订单号</param>
        /// <returns></returns>
        public static string CreateCode(string company, string max)
        {
            string maxCode = string.Empty;
            max = max.Trim();
            //为空则为第一各编号
            if (string.IsNullOrEmpty(max))
                maxCode = "001";
            else
            {
                int maxNum = Convert.ToInt32(max.Substring(5, 3));
                maxNum++;
                if (maxNum.ToString().Length == 1)
                {
                    maxCode = "00" + maxNum.ToString();
                }
                else if (maxNum.ToString().Length == 2)
                {
                    maxCode = "0" + maxNum.ToString();
                }
                else
                {
                    maxCode = maxNum.ToString();
                }
            }
            string date_code = DateTime.Now.Year.ToString();
            return company + date_code + maxCode;
        }

        /// <summary>
        /// 获取星值
        /// </summary>
        /// <param name="star"></param>
        /// <returns></returns>
        public static string GetStarValue(string star)
        {
            string showStar = string.Empty;
            switch (star)
            {
                case "0":
                    showStar = "☆☆☆☆☆";
                    break;
                case "1":
                    showStar = "★☆☆☆☆";
                    break;
                case "2":
                    showStar = "★★☆☆☆";
                    break;
                case "3":
                    showStar = "★★★☆☆";
                    break;
                case "4":
                    showStar = "★★★★☆";
                    break;
                case "5":
                    showStar = "★★★★★";
                    break;
                default:
                    showStar = "★★★★★";
                    break;
            }
            return showStar;
        }

        /// <summary>
        /// 判断活动等级
        /// </summary>
        /// <param name="star"></param>
        /// <returns></returns>
        public static int GetActLevel(int actNum)
        {
            int level = 0;
            if (actNum > 50 && actNum <= 150)
            {
                level = 1;
            }
            else if (actNum > 150 && actNum <= 250)
            {
                level = 2;
            }
            else if (actNum > 250 && actNum <= 450)
            {
                level = 3;
            }
            else if (actNum > 450 && actNum <= 650)
            {
                level = 4;
            }
            else if (actNum > 650)
            {
                level = 5;
            }
            return level;
        }
    }
}
