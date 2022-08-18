using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ConfirmOnline.Startup))]
namespace ConfirmOnline
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
