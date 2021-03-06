﻿using System;
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
            // You might think that setting up Redis would be done here but instead we run the initial configuration setup the first time
            // redis gets used. For this sample app it's in the HomeController's Index action method.

            // Setup the Session state to use Redis and point to the appropriate Redis server
            var webConfigPath = Server.MapPath("/web.config");
            RedisSessionStateConfig.InitializeSessionStateConfiguration(webConfigPath);

            // Standard ASP.NET stuff
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Removes header from response that identifies this as an asp.net application (security risk)
            MvcHandler.DisableMvcResponseHeader = true;
        }


        protected void Application_PreSendRequestHeaders(object sender, EventArgs e)
        {
            Context.Response.Headers.Remove("Server");
        }
    }
}