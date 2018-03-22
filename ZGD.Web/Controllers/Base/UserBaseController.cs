using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace ZGD.Web.Controllers
{
    [UserAuthLogin]
    [ValidateInput(false)]
    public class UserBaseController : BaseController
    {
        /// <summary>
        /// 是否登录
        /// </summary>
        public bool IsLogin
        {
            get
            {
                if (DTO.OperSession.LoginUser != null)
                    return true;
                return false;
            }
        }

        /// <summary>
        /// 取得当前用户信息
        /// </summary>
        public Model.User CurrentUser
        {
            get
            {
                return DTO.OperSession.LoginUser;
            }
        }
    }
}
