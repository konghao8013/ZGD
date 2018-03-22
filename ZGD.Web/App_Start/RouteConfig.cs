using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ZGD.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.aspx/{*pathInfo}");

            routes.MapRoute("ShortRoute",
                "{controller}/{id}",
                new { action = "details" },
                new { id = @"\d+" },
                new string[] { "ZGD.Web.Controllers" }
            );

            routes.MapRoute(
               "index.html", // Route name
               "index.html", // URL with parameters
               new
               {
                   controller = "Home",
                   action = "Index",
                   id = UrlParameter.Optional
               }, // Parameter defaults
               new string[] { "ZGD.Web.Controllers" }
            );
            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new
                {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional,
                    child = UrlParameter.Optional
                }, // Parameter defaults
                new string[] { "ZGD.Web.Controllers" }
            );
        }
    }
}
