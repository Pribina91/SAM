using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VisualAnalytics.Startup))]
namespace VisualAnalytics
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
