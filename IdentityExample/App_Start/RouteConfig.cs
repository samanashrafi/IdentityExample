using System.Web.Mvc;
using System.Web.Routing;

namespace IdentityExample
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                //defaults: new[] { "IdentityExample.Areas.Admin.Controllers" }
            );


            //routes.MapRoute(
            //    "Default", // Route name
            //    "{controller}/{action}/{id}", // URL with parameters
            //    new { area = "Admin", controller = "FreeContent", action = "Index", id = UrlParameter.Optional }, // Parameter defaults
            //    new[] { "IdentityExample.Areas.Admin.Controllers" }
            //).DataTokens.Add("area", "Admin");
        }
    }
}