using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Horatio_2._0.Startup))]
namespace Horatio_2._0
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
