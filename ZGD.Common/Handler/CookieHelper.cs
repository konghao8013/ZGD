using System;
using System.Text;
using System.Web;

namespace ZGD.Common
{
    public class CookieHelper
    {
        ///<summary>
        ///創建cookie值
        ///</summary>
        ///<param name="cookieName">cookie名稱</param>
        ///<param name="cookieValue">cookie值</param>
        ///<param name="cookieTime">cookie有效時間</param>
        public static void CreateCookieValue(string cookieName, string cookieValue, DateTime cookieTime)
        {
            HttpCookie cookie = new HttpCookie(cookieName);
            cookie.Value = cookieValue;
            //DateTime dtNow = DateTime.Now ;
            //TimeSpan tsMinute = cookieTime;
            cookie.Expires = cookieTime;
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        ///<summary>
        ///創建cookie值
        ///</summary>
        ///<param name="cookieName">cookie名稱</param>
        ///<param name="cookieValue">cookie值</param>
        ///<param name="cookieTime">cookie有效時間</param>
        public static void CreateCookieValue(string cookieName, string cookieValue, DateTime cookieTime, string domain)
        {
            HttpCookie cookie = new HttpCookie(cookieName);
            cookie.Value = cookieValue;
            cookie.Domain = domain;
            //DateTime dtNow = DateTime.Now ;
            //TimeSpan tsMinute = cookieTime;
            cookie.Expires = cookieTime;
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        ///<summary>
        ///創建cookie值
        ///</summary>
        ///<param name="cookieName">cookie名稱</param>    
        ///<param name="cookieValue">cookie值</param>
        ///<param name="subCookieName">子信息cookie名稱</param>
        ///<param name="subCookieValue">子信息cookie值</param>
        ///<param name="cookieTime">cookie有效時間</param>
        public static void CreateCookieValue(string cookieName, string cookieValue, string subCookieName, string subCookieValue, DateTime cookieTime, string domain)
        {
            HttpCookie cookie = new HttpCookie(cookieName);
            cookie.Value = cookieValue;
            cookie[subCookieName] = subCookieValue;
            cookie.Expires = cookieTime;
            cookie.Domain = domain;
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
        ///<summary>
        ///取得cookie的值
        ///</summary>
        ///<param name="cookieName">cookie名稱</param>
        ///<returns></returns>
        public static string GetCookieValue(string cookieName)
        {
            string cookieValue = "";
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
            if (null == cookie)
            {
                cookieValue = "";
            }
            else
            {
                cookieValue = cookie.Value;
            }
            return cookieValue;
        }
        ///<summary>
        ///取得cookie的值
        ///</summary>
        ///<param name="cookieName">cookie名稱</param>
        ///<param name="subCookieName">cookie子信息值</param>
        ///<returns></returns>
        public static string GetCookieValue(string cookieName, string subCookieName)
        {
            string cookieValue = "";
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
            if (null == cookie)
            {
                cookieValue = "";
            }
            else
            {
                cookieValue = cookie.Value;
                cookieValue = cookieValue.Split('&')[1].ToString().Split('=')[1];
            }
            return cookieValue;
        }

        ///<summary>
        ///刪除某個固定的cookie值[此方法一是在原有的cookie上再創建同樣的cookie值，但是時間是過期的時間]
        ///</summary>
        ///<param name="cookieName"></param>
        public static void RemoteCookieValue(string cookieName)
        {
            string dt = "1900-01-01 12:00:00";
            CreateCookieValue(cookieName, "", Convert.ToDateTime(dt));
        }

        ///<summary>
        ///刪除某個固定的cookie值[此方法一是在原有的cookie上再創建同樣的cookie值，但是時間是過期的時間]
        ///</summary>
        ///<param name="cookieName"></param>
        public static void RemoteCookieValue(string cookieName, string domain)
        {
            string dt = "1900-01-01 12:00:00";
            CreateCookieValue(cookieName, "", Convert.ToDateTime(dt), domain);
        }
    }
}
