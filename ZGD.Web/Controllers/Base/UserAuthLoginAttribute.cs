
using System.Web.Mvc;

namespace ZGD.Web.Controllers
{
    public class UserAuthLoginAttribute : ActionFilterAttribute
    {
        public bool IsLogin = true;
        /// <summary>
        /// 登录状态
        /// </summary>
        public UserAuthLoginAttribute()
        {
            IsLogin = true;
        }

        /// <summary>
        /// 登录状态
        /// </summary>
        /// <param name="islogin"></param>
        public UserAuthLoginAttribute(bool islogin)
        {
            IsLogin = islogin;
        }

        /// <summary>
        /// 判断登录状态
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //排除例外
            if (!IsLogin)
                return;
            string loginUrl = "/user/login";
            //上一次请求地址
            string refUrl = filterContext.HttpContext.Request.UrlReferrer != null ? filterContext.HttpContext.Request.UrlReferrer.ToString() : loginUrl;
            //控制器
            string controlName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToLower();
            //方法
            string actionName = filterContext.ActionDescriptor.ActionName.ToLower();
            //子方法
            bool isChildAction = filterContext.IsChildAction;
            //是否为异步请求
            bool isAjax = filterContext.HttpContext.Request.IsAjaxRequest();
            var controller = filterContext.Controller as UserBaseController;

            //未登录
            // 1在APP中 未登录
            // 2未在APP中 未登录
            if (!controller.IsLogin)
            {
                //string route = "/" + controlName + "/" + actionName;

                //异步处理
                if (isAjax)
                {
                    //这里可以添加一些过滤登录的异步操作如：公共上传图片
                    JsonResult jr = new JsonResult();
                    jr.Data = new AjaxMsgResult() { Success = false, Msg = "请先登录！", Source = "-110" };
                    filterContext.Result = jr;
                }
                else if (filterContext.IsChildAction)
                {
                    filterContext.Result = new ContentResult() { Content = "请先登录！" };
                }
                else
                {
                    string pq = null;
                    if (filterContext.HttpContext.Request.Url != null)
                    {
                        pq = filterContext.HttpContext.Request.Url.PathAndQuery;
                    }
                    filterContext.Result = new RedirectResult(loginUrl);
                }
            }
        }
    }
}