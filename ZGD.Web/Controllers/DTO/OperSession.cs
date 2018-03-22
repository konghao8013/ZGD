using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZGD.Common;

namespace ZGD.Web.Controllers.DTO
{
    /// <summary>
    /// 后台登录状态信息
    /// </summary>
    public class OperSession
    {
        /// <summary>
        /// 登录信息
        /// </summary>
        public static Model.User LoginUser
        {
            get
            {
                if (HttpContext.Current.Session[DTKeys.SESSION_USER_INFO] != null)
                {
                    return HttpContext.Current.Session[DTKeys.SESSION_USER_INFO] as Model.User;
                }
                return null;
            }
            set
            {
                HttpContext.Current.Session[DTKeys.SESSION_USER_INFO] = value;
            }
        }
    }
}