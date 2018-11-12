using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Epam.AzureWorkShop.Labs.Startup))]
namespace Epam.AzureWorkShop.Labs
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
