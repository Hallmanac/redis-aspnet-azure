using RedisWithAspNet4_6.Web.App_Core;
using RedisWithAspNet4_6.Web.App_Core.RedisServices;
using RedisWithAspNet4_6.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace RedisWithAspNet4_6.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Setup the Session state to use Redis and point to the appropriate Redis server
            var webConfigPath = Server.MapPath("/web.config");
            RedisSessionStateConfig.InitializeSessionStateConfiguration(webConfigPath);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Setup Redis for use as a regular cache
            AppConsts.RedisConfig = RedisStartupConfig.ConfigureRedis();
        }
    }
}
