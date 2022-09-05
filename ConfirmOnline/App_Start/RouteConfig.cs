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
            //settings.AutoRedirectMode = RedirectMode.Off;//OFF��ʹ��תҳ����ӦPOST����
            routes.EnableFriendlyUrls(settings);
        }
    }
}
