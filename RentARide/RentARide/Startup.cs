using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RentARide.Startup))]
namespace RentARide
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
