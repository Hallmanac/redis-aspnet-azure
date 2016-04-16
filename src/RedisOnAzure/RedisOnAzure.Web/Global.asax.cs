using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;


namespace RedisOnAzure.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            // Setup the Session state to use Redis and point to the appropriate Redis server
            var webConfigPath = Server.MapPath("/web.config");
            RedisSessionStateConfig.InitializeSessionStateConfiguration(webConfigPath);

            // Standard ASP.NET stuff
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}