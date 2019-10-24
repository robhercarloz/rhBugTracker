using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(rhBugTracker.Startup))]
namespace rhBugTracker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
