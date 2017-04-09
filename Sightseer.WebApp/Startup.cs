using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Sightseer.WebApp.Startup))]
namespace Sightseer.WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
