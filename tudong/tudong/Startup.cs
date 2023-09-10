using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(tudong.Startup))]
namespace tudong
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
