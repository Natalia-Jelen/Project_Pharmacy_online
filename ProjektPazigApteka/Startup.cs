using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjektPazigApteka.Startup))]
namespace ProjektPazigApteka
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
