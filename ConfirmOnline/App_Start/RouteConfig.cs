using Microsoft.AspNet.FriendlyUrls;
using System.Web.Routing;

namespace ConfirmOnline
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            var settings = new FriendlyUrlSettings();
            settings.AutoRedirectMode = RedirectMode.Permanent;
            //settings.AutoRedirectMode = RedirectMode.Off;//OFF可使跳转页面相应POST请求
            routes.EnableFriendlyUrls(settings);
        }
    }
}
