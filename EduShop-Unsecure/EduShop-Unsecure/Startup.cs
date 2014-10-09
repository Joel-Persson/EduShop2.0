using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EduShop_Unsecure.Startup))]
namespace EduShop_Unsecure
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
