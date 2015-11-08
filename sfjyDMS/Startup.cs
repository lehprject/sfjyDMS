using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(sfjyDMS.Startup))]
namespace sfjyDMS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
