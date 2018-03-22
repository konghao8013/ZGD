/*
* Project：时间操作类
* Author：LJ
* Data：2011-11-23
* Updated：
* Remark：对时间各种读取操作类
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace ZGD.Common
{
    public class DateTimeHelper
    {
        /// <summary>
        /// 验证最小时间
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static bool CheckMinDate(object code)
        {
            bool bl = true;
            if (code == null)
                return true;
            if (!string.IsNullOrEmpty(code.ToString()))
            {
                if (Convert.ToDateTime(code).Equals(DateTime.MinValue))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return bl;
        }

        /// <summary>
        /// 获取当前时间 间隔 
        /// type -> 1：秒  2：分  3：小时  4：天
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int GetTimeSpan(DateTime date, int type)
        {
            int count = 0;
            TimeSpan ts = date - DateTime.Now;
            switch (type)
            {
                //秒
                case 1:
                    count = Convert.ToInt32(ts.TotalSeconds);
                    break;
                //分
                case 2:
                    count = Convert.ToInt32(ts.TotalMinutes);
                    break;
                //小时
                case 3:
                    count = Convert.ToInt32(ts.TotalHours);
                    break;
                //天
                case 4:
                    count = Convert.ToInt32(ts.TotalDays);
                    break;
            }
            return count;
        }

        /// <summary>
        /// 返回当前时间
        /// </summary>
        /// <param name="formart">
        /// d 返回
        /// m 返回
        /// y 返回
        /// H 返回
        /// M 返回
        /// S 返回
        /// all 返回
        /// 默认返回 yyyy-MM-dd
        /// </param>
        /// <returns></returns>
        public static string GetTime(string formart = "", string datetime = "")
        {
            DateTime dt = new DateTime();
            if (!DateTime.TryParse(datetime, out dt))
                dt = DateTime.Now;

            switch (formart)
            {
                case "d":
                    return dt.Day.ToString();
                case "m":
                    return dt.Month.ToString();
                case "y":
                    return dt.Year.ToString();
                case "H":
                    return dt.Hour.ToString();
                case "M":
                    return dt.Minute.ToString();
                case "S":
                    return dt.Millisecond.ToString();
                case "all":
                    return dt.ToString();
                default:
                    return dt.ToString("yyyy-MM-dd");
            }
        }

        /// <summary>
        /// 对时间进行操作
        /// </summary>
        /// <param name="formart">
        /// d 操作 天
        /// m 操作 月
        /// y 操作 年
        /// H 操作 小时
        /// M 操作 分钟
        /// S 操作 秒
        /// </param>
        /// <param name="num">数值 正负数</param>
        /// <param name="datetime">需操作的时间 不输入、出错 为当前时间</param>
        /// <returns>操作后时间</returns>
        public static DateTime GetTime(string formart = "", int num = 0, string datetime = "")
        {
            DateTime dt = new DateTime();
            if (!DateTime.TryParse(datetime, out dt))
            {
                dt = DateTime.Now;
            }
            if (num != 0)
            {
                switch (formart)
                {
                    case "d":
                        return dt.AddDays(num);
                    case "m":
                        return dt.AddMonths(num);
                    case "y":
                        return dt.AddYears(num);
                    case "H":
                        return dt.AddHours(num);
                    case "M":
                        return dt.AddMinutes(num);
                    case "S":
                        return dt.AddMilliseconds(num);
                    default:
                        break;
                }
            }
            return dt;
        }

        /// <summary>
        /// 得到对时间操作后的字符串
        /// </summary>
        /// <param name="formart">
        /// d 操作 天
        /// m 操作 月
        /// y 操作 年
        /// H 操作 小时
        /// M 操作 分钟
        /// S 操作 秒
        /// </param>
        /// <param name="num">数值 正负数</param>
        /// <param name="returnFormart">
        /// d 返回
        /// m 返回
        /// y 返回
        /// H 返回
        /// M 返回
        /// S 返回
        /// all 返回
        /// 默认返回 yyyy-MM-dd
        /// </param>
        /// <param name="datetime">需操作的时间 不输入、出错 为当前时间</param>
        /// <returns>操作后时间</returns>
        public static string GetTime(string formart = "", int num = 0, string returnFormart = "" ,string datetime = "")
        {
            DateTime dt = GetTime(formart, num, datetime);
            return GetTime(returnFormart, dt.ToString());
        }

        /// <summary>
        /// 时间格式转换(如有其他格式请加case处理)
        ///format参数格式： yyyy格式MM格式dd格式 HH:mm:ss  如（yyyy年MM月dd日）
        /// </summary>
        /// <param name="obj">时间</param>
        /// <param name="fType">类型</param>
        /// <returns></returns>
        public static string FormatDate(object obj, string format)
        {
            if (obj != null)
            {
                try
                {
                    DateTime date = Convert.ToDateTime(obj);
                    return date.ToString(format, DateTimeFormatInfo.InvariantInfo);
                }
                catch { return ""; }
            }
            else
                return "";
        }

        /// <summary>
        /// 根据当前日期 获取 本日、本周、本月、本年
        /// type：1 本日  2 本周  3 本月  4 下一个月  5 本年
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<string> GetCurDate(string type)
        {
            string beginDate = "", endDate = "";
            List<string> date = new List<string>();
            switch (type)
            {
                case "1":
                    beginDate = DateTime.Now.ToString("d") + " 00:00:00";
                    endDate = DateTime.Now.ToString("d") + " 23:59:59";
                    break;
                case "2":
                    switch (DateTime.Now.DayOfWeek)
                    {
                        /*星期一*/
                        case DayOfWeek.Monday: beginDate = DateTime.Now.ToString("d") + " 00:00:00";
                            endDate = DateTime.Now.AddDays(6).ToString("d") + " 23:59:59";
                            break;
                        /*星期二*/
                        case DayOfWeek.Tuesday:
                            beginDate = DateTime.Now.AddDays(-1).ToString("d") + " 00:00:00";
                            endDate = DateTime.Now.AddDays(5).ToString("d") + " 23:59:59";
                            break;
                        /*星期三*/
                        case DayOfWeek.Wednesday:
                            beginDate = DateTime.Now.AddDays(-2).ToString("d") + " 00:00:00";
                            endDate = DateTime.Now.AddDays(4).ToString("d") + " 23:59:59";
                            break;
                        /*星期四*/
                        case DayOfWeek.Thursday:
                            beginDate = DateTime.Now.AddDays(-3).ToString("d") + " 00:00:00";
                            endDate = DateTime.Now.AddDays(3).ToString("d") + " 23:59:59";
                            break;
                        /*星期五*/
                        case DayOfWeek.Friday:
                            beginDate = DateTime.Now.AddDays(-4).ToString("d") + " 00:00:00";
                            endDate = DateTime.Now.AddDays(2).ToString("d") + " 23:59:59";
                            break;
                        /*星期六*/
                        case DayOfWeek.Saturday:
                            beginDate = DateTime.Now.AddDays(-5).ToString("d") + " 00:00:00";
                            endDate = DateTime.Now.AddDays(1).ToString("d") + " 23:59:59";
                            break;
                        /*星期日*/
                        case DayOfWeek.Sunday:
                            beginDate = DateTime.Now.AddDays(-6).ToString("d") + " 00:00:00";
                            endDate = DateTime.Now.ToString("d") + " 23:59:59";
                            break;
                    }
                    break;
                case "3":
                    beginDate = DateTime.Now.ToString("yyyy-MM") + "-01 00:00:00";
                    endDate = DateTime.Parse(DateTime.Now.AddMonths(1).ToString("yyyy-MM") + "-01 00:00:00").AddDays(-1).ToString("d") + " 23:59:59";
                    break;
                case "4":
                    beginDate = DateTime.Now.AddMonths(1).ToString("yyyy-MM") + "-01 00:00:00";
                    endDate = DateTime.Parse(DateTime.Now.AddMonths(2).ToString("yyyy-MM") + "-01 00:00:00").AddDays(-1).ToString("d") + " 23:59:59";
                    break;
                case "5":
                    beginDate = DateTime.Now.ToString("yyyy") + "-01-01 00:00:00";
                    endDate = DateTime.Now.ToString("yyyy") + "-12-31 23:59:59";
                    break;
            }
            date.Add(beginDate);
            date.Add(endDate);
            return date;
        }

        /// <summary>
        /// 获取当前星期几
        /// </summary>
        /// <param name="obj">时间</param>
        /// <returns></returns>
        public static string GetWeek(object obj)
        {
            string week = string.Empty;
            DateTime date = Convert.ToDateTime(obj);
            switch (date.DayOfWeek.ToString("D"))
            {
                case "0":
                    week = "星期日";
                    break;
                case "1":
                    week = "星期一";
                    break;
                case "2":
                    week = "星期二";
                    break;
                case "3":
                    week = "星期三";
                    break;
                case "4":
                    week = "星期四";
                    break;
                case "5":
                    week = "星期五";
                    break;
                case "6":
                    week = "星期六";
                    break;
            }
            return week;
        }

        /// <summary>
        /// 获取指定日期所处的一周 日期时间(0：周一日期   1：周末日期)
        /// </summary>
        /// <returns></returns>
        public static List<string> GetWeekDate(DateTime date)
        {
            if (date == null)
                date = DateTime.Now;
            List<string> weekDate = new List<string>();
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    weekDate.Add(date.ToShortDateString() + " 00:00:00");
                    weekDate.Add(date.AddDays(6).ToShortDateString() + " 23:59:59");
                    break;
                case DayOfWeek.Tuesday:
                    weekDate.Add(date.AddDays(-1).ToShortDateString() + " 00:00:00");
                    weekDate.Add(date.AddDays(5).ToShortDateString() + " 23:59:59");
                    break;
                case DayOfWeek.Wednesday:
                    weekDate.Add(date.AddDays(-2).ToShortDateString() + " 00:00:00");
                    weekDate.Add(date.AddDays(4).ToShortDateString() + " 23:59:59");
                    break;
                case DayOfWeek.Thursday:
                    weekDate.Add(date.AddDays(-3).ToShortDateString() + " 00:00:00");
                    weekDate.Add(date.AddDays(3).ToShortDateString() + " 23:59:59");
                    break;
                case DayOfWeek.Friday:
                    weekDate.Add(date.AddDays(-4).ToShortDateString() + " 00:00:00");
                    weekDate.Add(date.AddDays(2).ToShortDateString() + " 23:59:59");
                    break;
                case DayOfWeek.Saturday:
                    weekDate.Add(date.AddDays(-5).ToShortDateString() + " 00:00:00");
                    weekDate.Add(date.AddDays(1).ToShortDateString() + " 23:59:59");
                    break;
                case DayOfWeek.Sunday:
                    weekDate.Add(date.AddDays(-6).ToShortDateString() + " 00:00:00");
                    weekDate.Add(date.ToShortDateString() + " 23:59:59");
                    break;
            }
            return weekDate;
        }

        /// <summary>
        /// 时间戳转为C#格式时间
        /// </summary>
        /// <param name="timeStamp">Unix时间戳格式</param>
        /// <returns>C#格式时间</returns>
        public static DateTime UnixIntToDateTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }

        /// <summary>
        /// DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time"> DateTime时间格式</param>
        /// <returns>Unix时间戳格式</returns>
        public static int DateTimeToUnixInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }

        /// <summary>
        /// 将时间换成中文
        /// 以当前时间为准(距离当前时间)
        /// </summary>
        /// <param name="datetime">时间</param>
        /// <returns></returns>
        public static string DateToChineseString(object datetime)
        {
            DateTime curDate = Convert.ToDateTime(datetime);
            TimeSpan ts = DateTime.Now - curDate;
            //    System.Web.HttpContext.Current.Response.Write(ts.TotalDays);
            if ((int)ts.TotalDays >= 365)
            {
                return (int)ts.TotalDays / 365 + "年前";
            }
            if ((int)ts.TotalDays >= 30 && ts.TotalDays <= 365)
            {
                return (int)ts.TotalDays / 30 + "月前";
            }
            if ((int)ts.TotalDays == 1)
            {
                return "昨天";
            }
            if ((int)ts.TotalDays == 2)
            {
                return "前天";
            }
            if ((int)ts.TotalDays >= 3 && ts.TotalDays <= 30)
            {
                return (int)ts.TotalDays + "天前";
            }
            if ((int)ts.TotalDays == 0)
            {
                if ((int)ts.TotalHours != 0)
                {
                    return (int)ts.TotalHours + "小时前";
                }
                else
                {
                    if ((int)ts.TotalMinutes == 0)
                    {
                        return "1分钟前";
                    }
                    else
                    {
                        return (int)ts.TotalMinutes + "分钟前";
                    }
                }
            }
            return curDate.ToString("yyyy年MM月dd日 HH:mm");
        }
    }
}
