using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Threading.Tasks;
using ZGD.Common;

namespace ZGD.Web.Controllers
{
    [Filter]
    [ValidateInput(false)]
    public class BaseController : Controller
    {
        /// <summary>
        /// 页面标题
        /// </summary>
        public string Title
        {
            get { return ViewBag.Title; }
            set { ViewBag.Title = value; }
        }
        /// <summary>
        /// 页面关键词
        /// </summary>
        public string Keywords
        {
            get { return ViewBag.Keywords; }
            set { ViewBag.Keywords = value; }
        }
        /// <summary>
        /// 页面描述
        /// </summary>
        public string Description
        {
            get { return ViewBag.Description; }
            set { ViewBag.Description = value; }
        }
    }

    public class FilterAttribute : ActionFilterAttribute
    {
        public bool IsFilter = true;
        /// <summary>
        /// 登录状态
        /// </summary>
        public FilterAttribute()
        {
            IsFilter = true;
        }

        /// <summary>
        /// 登录状态
        /// </summary>
        /// <param name="islogin"></param>
        public FilterAttribute(bool isFilter)
        {
            IsFilter = isFilter;
        }

        /// <summary>
        /// 判断登录状态
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (this.IsFilter)
            {
                //过滤参数
                var actionParameters = filterContext.ActionDescriptor.GetParameters();
                if (actionParameters != null && actionParameters.Count() > 0)
                {
                    foreach (var p in actionParameters)
                    {
                        if (p.ParameterType == typeof(string))
                        {
                            if (filterContext.ActionParameters[p.ParameterName] != null)
                            {
                                if (StringHandler.SqlFilter(filterContext.ActionParameters[p.ParameterName].ToString()))
                                {
                                    filterContext.Result = new RedirectResult("/Error/Error.html");
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
