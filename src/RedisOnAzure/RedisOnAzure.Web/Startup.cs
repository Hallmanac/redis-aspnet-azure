using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RedisOnAzure.Web.Startup))]
namespace RedisOnAzure.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
