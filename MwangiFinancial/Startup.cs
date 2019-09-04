using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MwangiFinancial.Startup))]
namespace MwangiFinancial
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
