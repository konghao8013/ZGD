using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using ZGD.Common;
using ZGD.Model;

namespace ZGD.Web.Controllers
{
    public class UserController : UserBaseController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int page = 1, int t = 1)
        {
            int userId = this.CurrentUser.ID;
            string pager = string.Empty, url = "&t=" + t;

            ZGD.BLL.Question qa = new BLL.Question();
            ZGD.BLL.Answer a_bll = new BLL.Answer();
            ViewBag.aCount = a_bll.GetRecordCount(" UserId=" + userId);
            ViewBag.cnCount = a_bll.GetRecordCount(" UserId=" + userId + " and IsAccept=1");
            if (t == 1)
            {
                ViewBag.MyQA = qa.GetList(page, 10, " q.UserId=" + userId, " q.IsTop desc, q.ID desc", url, "/user", out pager);
            }
            else
            {
                ViewBag.MyQA = a_bll.GetList(page, 10, " a.UserId=" + userId, " a.ID desc", url, "/user", out pager);
            }
            ViewBag.Pager = pager;
            ViewBag.Page = page;
            ViewBag.Type = t;
            return View(DTO.OperSession.LoginUser);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [UserAuthLogin(false)]
        [HttpGet]
        public ActionResult Reg()
        {
            if (CheckLogin())
            {
                return Redirect("/user");
            }
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [UserAuthLogin(false)]
        [HttpGet]
        public ActionResult Login()
        {
            if (CheckLogin())
            {
                return Redirect("/user");
            }
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            CookieHelper.RemoteCookieValue(DTKeys.COOKIE_USER_NAME_REMEMBER);
            return Redirect("/");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [UserAuthLogin(false)]
        [HttpGet]
        public ActionResult Find()
        {
            if (CheckLogin())
            {
                return Redirect("/user/editpwd");
            }
            return View();
        }

        /// <summary>
        /// 登录异步方法
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [UserAuthLogin(false)]
        [HttpPost]
        public JsonResult Login(Model.RequestUser request)
        {
            return Json(LoginMethod(request));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        AjaxMsgResult LoginMethod(Model.RequestUser request)
        {
            if (request == null)
            {
                return new AjaxMsgResult { Success = false, Msg = "请求参数为空！" };
            }
            request.IsImgCode = true;
            request.IsSmsCode = false;

//#if DEBUG
//            request.Phone = "13452355249";
//            request.Password = "111111";
//#endif

            if (string.IsNullOrWhiteSpace(request.Phone))
            {
                return new AjaxMsgResult { Success = false, Msg = "请输入用户名！" };
            }
            if (!RegexHandler.IsMobile(request.Phone))
            {
                return new AjaxMsgResult { Success = false, Msg = "错误的手机号！" };
            }
            if (string.IsNullOrWhiteSpace(request.Password))
            {
                return new AjaxMsgResult { Success = false, Msg = "请输入登录密码！" };
            }

            //图片验证码验证
            if (request.IsImgCode)
            {
                if (string.IsNullOrEmpty(request.ImgCode))
                {
                    return new AjaxMsgResult { Success = false, Msg = "图片验证码不能为空！" };
                }
                if (!SystemHandler.CheckImgCode(request.ImgCode))
                {
                    return new AjaxMsgResult { Success = false, Msg = "图片验证码错误！" };
                }
            }

            //短信验证码验证
            if (request.IsSmsCode)
            {
                if (string.IsNullOrEmpty(request.SmsCode))
                {
                    return new AjaxMsgResult { Success = false, Msg = "短信验证码不能为空！" };
                }
                if (!SystemHandler.CheckSmsCode(request.SmsCode))
                {
                    return new AjaxMsgResult { Success = false, Msg = "短信验证码错误！" };
                }
            }

            BLL.User user = new BLL.User();
            string pwd_md5 = StringHandler.MD5(request.Password);
            var data = user.GetList(1, " Phone='" + request.Phone + "' and Password='" + pwd_md5 + "'", "");

            if (data != null && data.Tables[0] != null && data.Tables[0].Rows.Count > 0)
            {
                //赋值会话
                int userId = Convert.ToInt32(data.Tables[0].Rows[0]["ID"]);
                var model = user.GetModel(userId);
                DTO.OperSession.LoginUser = model;
                if (request.IsRem)
                {
                    string cookieData = DESHelper.DesEncrypt(request.Phone + "|" + request.Password, DTKeys.SSOKey);
                    CookieHelper.CreateCookieValue(DTKeys.COOKIE_USER_NAME_REMEMBER, cookieData, DateTime.Now.AddDays(30));
                }
                return new AjaxMsgResult { Success = true, Msg = "登录成功！", Source = "/user" };
            }
            return new AjaxMsgResult { Success = false, Msg = "账号或密码错误，登录失败！", Source = "/" };
        }

        /// <summary>
        /// 验证是否登录
        /// </summary>
        /// <returns></returns>
        bool CheckLogin()
        {
            if (DTO.OperSession.LoginUser != null)
            {
                return true;
            }
            else
            {
                //记住密码处理
                string cookieData = CookieHelper.GetCookieValue(DTKeys.COOKIE_USER_NAME_REMEMBER);
                if (string.IsNullOrWhiteSpace(cookieData))
                {
                    return false;
                }
                string loginData = DESHelper.DesDecrypt(cookieData, DTKeys.SSOKey);
                if (string.IsNullOrWhiteSpace(loginData))
                {
                    return false;
                }

                string[] loginInfo = loginData.Split('|');
                var result = LoginMethod(new Model.RequestUser
                {
                    IsImgCode = false,
                    IsSmsCode = false,
                    Phone = loginInfo[0],
                    Password = loginInfo[1]
                });
                if (result.Success)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        [ActionName("editpwd")]
        [HttpPost]
        public JsonResult UserEditPwd(string oPwd, string nPwd, string cPwd)
        {
            if (string.IsNullOrWhiteSpace(oPwd))
            {
                return Json(new AjaxMsgResult { Success = false, Msg = "输入您的原密码！" });
            }
            if (string.IsNullOrWhiteSpace(nPwd))
            {
                return Json(new AjaxMsgResult { Success = false, Msg = "输入您的新密码！" });
            }
            if (nPwd.Length < 6 || nPwd.Length > 20)
            {
                return Json(new AjaxMsgResult { Success = false, Msg = "密码为6-20位字母或数字！" });
            }

            //
            string md5_oPwd = StringHandler.MD5(oPwd);
            BLL.User user = new BLL.User();
            var data = user.GetList(1, " ID='" + CurrentUser.ID + "' and Password='" + md5_oPwd + "'", "");
            if (data == null || data.Tables[0] == null || data.Tables[0].Rows.Count == 0)
            {
                return Json(new AjaxMsgResult { Success = false, Msg = "原密码输入错误！" });
            }

            user.UpdateField(CurrentUser.ID, " Password=" + StringHandler.MD5(nPwd));
            return Json(new AjaxMsgResult { Success = true, Msg = "密码修改成功！", Source = "/user/login" });
            //return Json(new AjaxMsgResult { Success = false, Msg = "提交失败，请联系客服" + ConstHelper.ServiceTel + "！" });
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [UserAuthLogin(false)]
        [ActionName("reg")]
        [HttpPost]
        public JsonResult RegUser(RequestUser regInfo)
        {
            if (regInfo == null)
            {
                return Json(new AjaxMsgResult { Success = false, Msg = "参数异常！" });
            }
            regInfo.IsImgCode = true;
            regInfo.IsSmsCode = true;
            regInfo.IsAutoLogin = true;

            BLL.User user = new BLL.User();

            #region 参数验证
            if (regInfo == null)
            {
                return Json(new AjaxMsgResult { Success = false, Msg = "参数异常！" });
            }
            if (string.IsNullOrEmpty(regInfo.Phone))
            {
                return Json(new AjaxMsgResult { Success = false, Msg = "注册手机不能为空！" });
            }
            if (string.IsNullOrEmpty(regInfo.Nickname))
            {
                return Json(new AjaxMsgResult { Success = false, Msg = "请输入您的昵称！" });
            }
            if (string.IsNullOrEmpty(regInfo.Password))
            {
                return Json(new AjaxMsgResult { Success = false, Msg = "密码不能为空！" });
            }
            if (regInfo.Password.Length < 6 || regInfo.Password.Length > 20)
            {
                return Json(new AjaxMsgResult { Success = false, Msg = "密码为6-20位字母或数字！" });
            }

            //手机验证
            if (RegexHandler.IsMobile(regInfo.Phone))
            {
                if (user.ExistsByPhone(regInfo.Phone))
                {
                    return Json(new AjaxMsgResult { Success = false, Msg = "该号码已被注册！" });
                }
            }
            else
            {
                return Json(new AjaxMsgResult { Success = false, Msg = "请输入正确的手机号码！" });
            }

            //短信验证码验证
            if (regInfo.IsSmsCode)
            {
                if (string.IsNullOrEmpty(regInfo.SmsCode))
                {
                    return Json(new AjaxMsgResult { Success = false, Msg = "短信验证码不能为空！" });
                }
                if (!SystemHandler.CheckSmsCode(regInfo.SmsCode))
                {
                    return Json(new AjaxMsgResult { Success = false, Msg = "短信验证码错误！" });
                }
            }

            //图片验证码验证
            if (regInfo.IsImgCode)
            {
                if (string.IsNullOrWhiteSpace(regInfo.ImgCode))
                {
                    return Json(new AjaxMsgResult { Success = false, Msg = "请输入图片验证码！" });
                }
                if (!SystemHandler.CheckImgCode(regInfo.ImgCode))
                {
                    return Json(new AjaxMsgResult { Success = false, Msg = "图片验证码错误！" });
                }
            }

            //验证昵称是否为手机
            if (RegexHandler.IsMobile(regInfo.Nickname))
            {
                regInfo.Nickname = regInfo.Nickname.ToPhone();
            }

            //昵称唯一性
            if (user.ExistsByNick(regInfo.Nickname))
            {
                return Json(new AjaxMsgResult { Success = false, Msg = "该昵称已被注册！" });
            }

            //获取城市
            string ip = StringHandler.GetClientIP();
            #endregion

            var model = new Model.User
            {
                Nickname = regInfo.Nickname,
                Password = StringHandler.MD5(regInfo.Password),
                Phone = regInfo.Phone,
                IsLock = 0,
                RegIP = ip,
                PubTime = DateTime.Now
            };
            var result = user.Add(model);
            if (result > 0)
            {
                if (regInfo.IsAutoLogin)
                {
                    model.ID = result;
                    DTO.OperSession.LoginUser = model;
                    string cookieData = DESHelper.DesEncrypt(regInfo.Phone + "|" + regInfo.Password, DTKeys.SSOKey);
                    CookieHelper.CreateCookieValue(DTKeys.COOKIE_USER_NAME_REMEMBER, cookieData, DateTime.Now.AddDays(30));
                }
                return Json(new AjaxMsgResult { Success = true, Msg = "注册成功！", Source = "/user" });
            }
            return Json(new AjaxMsgResult { Success = false, Msg = "注册失败，请联系客服！" });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [ActionName("find")]
        [UserAuthLogin(false)]
        [HttpPost]
        public JsonResult FindPwd(RequestUser request)
        {
            if (request == null)
            {
                return Json(new AjaxMsgResult { Success = false, Msg = "参数异常！" });
            }

            request.IsSmsCode = true;
            request.IsImgCode = true;

            if (string.IsNullOrWhiteSpace(request.Phone))
            {
                return Json(new AjaxMsgResult { Success = false, Msg = "请输入手机号码！" });
            }
            //验证手机号码是否正确
            if (!RegexHandler.IsMobile(request.Phone))
            {
                return Json(new AjaxMsgResult { Success = false, Msg = "手机号码不正确！" });
            }

            //短信验证码验证
            if (request.IsSmsCode)
            {
                if (string.IsNullOrWhiteSpace(request.SmsCode))
                {
                    return Json(new AjaxMsgResult { Success = false, Msg = "请输入短信验证码！" });
                }
                if (!SystemHandler.CheckSmsCode(request.SmsCode))
                {
                    return Json(new AjaxMsgResult { Success = false, Msg = "短信验证码错误！" });
                }
            }

            //图片验证码验证
            if (request.IsImgCode)
            {
                if (string.IsNullOrWhiteSpace(request.ImgCode))
                {
                    return Json(new AjaxMsgResult { Success = false, Msg = "请输入图片验证码！" });
                }
                if (!SystemHandler.CheckImgCode(request.ImgCode))
                {
                    return Json(new AjaxMsgResult { Success = false, Msg = "图片验证码错误！" });
                }
            }

            //
            BLL.User user = new BLL.User();
            if (!user.ExistsByPhone(request.Phone))
            {
                return Json(new AjaxMsgResult { Success = false, Msg = "该手机号不存在！" });
            }

            Random rand = new Random();
            int newpwd = rand.Next(100000, 999999);
            string sendMsg = "密码已重置：" + newpwd.ToString() + "，登录后请自行修改密码";

            var result = SMSHelper.SendSms(request.Phone, sendMsg);
            if (result.Success)
            {
                user.UpdateFieldByPhone(request.Phone, " Password=" + StringHandler.MD5(newpwd.ToString()));
                return Json(new AjaxMsgResult { Success = true, Msg = "成功找回，新密码已发送至您的手机，请注意查收！", Source = "/user/login" });
            }
            return Json(new AjaxMsgResult { Success = false, Msg = "提交失败，请联系客服！" });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public JsonResult repairjoin(Model.Repair model)
        {
            if (string.IsNullOrWhiteSpace(model.uName))
            {
                return Json(new AjaxMsgResult() { Success = false, Msg = "请输入您的姓名！" });
            }
            if (string.IsNullOrWhiteSpace(model.Phone))
            {
                return Json(new AjaxMsgResult() { Success = false, Msg = "请输入您的联系手机！" });
            }
            if (!RegexHandler.IsMobile(model.Phone))
            {
                return Json(new AjaxMsgResult() { Success = false, Msg = "请输入正确的手机号！" });
            }
            if (string.IsNullOrWhiteSpace(model.Address))
            {
                return Json(new AjaxMsgResult() { Success = false, Msg = "请输入楼盘地址！" });
            }
            if (string.IsNullOrWhiteSpace(model.WorkDate))
            {
                return Json(new AjaxMsgResult() { Success = false, Msg = "请输入维修时间！" });
            }
            if (string.IsNullOrWhiteSpace(model.Remark))
            {
                return Json(new AjaxMsgResult() { Success = false, Msg = "请输入维修说明！" });
            }
            if (string.IsNullOrWhiteSpace(model.vCode))
            {
                return Json(new AjaxMsgResult() { Success = false, Msg = "验证码不能为空！" });
            }
            if (model.vCode.ToLower() != Session[DTKeys.SESSION_CODE].ToString().ToLower())
            {
                return Json(new AjaxMsgResult() { Success = false, Msg = "验证码错误！" });
            }
            BLL.Repair rBll = new BLL.Repair();
            int join_id = rBll.Add(model);
            if (join_id > 0)
            {
                return Json(new AjaxMsgResult() { Success = true, Msg = "提交成功！" });
            }
            return Json(new AjaxMsgResult() { Success = false, Msg = "提交失败！" });
        }
    }
}
