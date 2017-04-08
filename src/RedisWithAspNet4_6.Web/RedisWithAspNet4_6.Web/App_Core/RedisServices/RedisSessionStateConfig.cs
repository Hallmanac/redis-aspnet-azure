using System;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;

namespace RedisWithAspNet4_6.Web.App_Core.RedisServices
{
    public class RedisSessionStateConfig
    {
        /// <summary>
        /// This is a technique to allow you to hold appSetting values for the specific environments and apply them to the RedisSessionStateProvider. The
        /// goal is to pull the values from appSettings and overwrite the "sessionstate" node inside web.config. The reason why you 
        /// would want to do this is so that you don't carry around credentials to your production redis instance as well as preventing you from
        /// accidentally writing to your redis production instance while developing. This will only run the very first time you deploy.
        /// </summary>
        /// <param name="path">Server path to the web.config file so it can be loaded by LinqToXML</param>
        public static void InitializeSessionStateConfiguration(string path)
        {
            const string redisSessionProvider = "RedisSessionProvider";
            const string providers = "providers";
            const string redisSiteInstanceConst = "RedisSiteInstance";
            const string refreshRedisSessionStateConfig = "RefreshRedisSessionStateConfig";
            const string site_Instance = "Site_Instance";
            const string refreshRedisSessionProvider = "RefreshRedisSessionStateConfig";

            // Get Access to the web.config
            var appSettings = ConfigurationManager.AppSettings; //config.AppSettings.Settings;
            var siteInstance = string.IsNullOrWhiteSpace(appSettings[site_Instance]) ? "Local" : appSettings[site_Instance];
            var redisSiteInstance = string.IsNullOrWhiteSpace(appSettings[redisSiteInstanceConst]) ? "" : appSettings[redisSiteInstanceConst];
            var refreshRedisConfig = string.IsNullOrWhiteSpace(appSettings[refreshRedisSessionStateConfig])
                ? "false"
                : appSettings[refreshRedisSessionStateConfig];

            // If the site instance and the redis site instance app settings are the same then we've already setup the session state section in web.config
            if (string.Equals(siteInstance, redisSiteInstance, StringComparison.CurrentCultureIgnoreCase) && !refreshRedisConfig.ParseBool())
            {
                return;
            }

            // pull out the values in app settings for the redis configuration. We'll stick these in the session state element
            var redisHost = string.IsNullOrWhiteSpace(appSettings["RedisHost"]) ? "localhost" : appSettings["RedisHost"];
            var redisPort = string.IsNullOrWhiteSpace(appSettings["RedisPort"]) ? "6379" : appSettings["RedisPort"];
            var redisAccessKey = string.IsNullOrWhiteSpace(appSettings["RedisAccessKey"]) ? "" : appSettings["RedisAccessKey"];
            var redisSsl = string.IsNullOrWhiteSpace(appSettings["RedisSsl"]) ? "false" : appSettings["RedisSsl"];
            var redisDatabaseId = string.IsNullOrWhiteSpace(appSettings["RedisDatabaseId"]) ? "0" : appSettings["RedisDatabaseId"];

            // Get a reference to the web.config xml document so we can manipulate it using Linq to XML
            var configPath = path;
            var webConfigXmlDoc = XDocument.Load(configPath);
            var root = webConfigXmlDoc.Root;

            if (root == null)
            {
                return;
            }

            // -- Drill down to the sessionState element so we can set that up -- \\

            // Get the system.web element
            var sysWeb = root.Element("system.web");
            if (sysWeb == null)
            {
                sysWeb = new XElement("system.web");
                root.Add(sysWeb);
            }

            // Get or create the sessionState element
            var sessionStateElem = sysWeb.Element("sessionState");
            sessionStateElem?.Remove();
            sessionStateElem = new XElement("sessionState",
                new XAttribute("mode", "Custom"),
                new XAttribute("customProvider", redisSessionProvider),
                new XAttribute("stateNetworkTimeout", "1200"));
            sysWeb.Add(sessionStateElem);

            // Get and then remove the providers element (so we can re-add it back from scratch)
            var providersElem = sessionStateElem.Element(providers);
            providersElem?.Remove();

            // Recreate the providers element and add it to the sessionState element
            var providersReplacement = new XElement(providers,
                new XElement("add",
                    new XAttribute("name", redisSessionProvider),
                    new XAttribute("type", "Microsoft.Web.Redis.RedisSessionStateProvider"),
                    new XAttribute("port", redisPort),
                    new XAttribute("host", redisHost),
                    new XAttribute("accessKey", redisAccessKey),
                    new XAttribute("ssl", redisSsl),
                    new XAttribute("databaseId", redisDatabaseId)));
            sessionStateElem.Add(providersReplacement);

            // -- Set the RedisSiteInstance app setting value to be equal to the value of the site instance app setting -- \\

            // Get the appSettings element
            var appSettingsElem = root.Element("appSettings");
            if (appSettingsElem == null)
            {
                root.Add(new XElement("appSettings"));
            }

            // Get and then remove the RedisSiteInstance appSetting element
            var redisSiteInstanceElem =
                appSettingsElem.Elements("add").FirstOrDefault(e => string.Equals(e.Attribute("key").Value, redisSiteInstanceConst));
            redisSiteInstanceElem?.Remove();

            // Recreate and add back in the RedisSiteInstance appSetting
            appSettingsElem.Add(new XElement("add",
                new XAttribute("key", redisSiteInstanceConst),
                new XAttribute("value", siteInstance)));

            // Get the RefreshRedisSessionStateConfig elemnent from app settings
            var refreshRedisElem =
                appSettingsElem.Elements("add").FirstOrDefault(e => string.Equals(e.Attribute("key").Value, refreshRedisSessionProvider));
            refreshRedisElem?.Remove();

            // Add the RefreshRedisSessionStateConfig element back in but set it to false
            appSettingsElem.Add(new XElement("add",
                new XAttribute("key", refreshRedisSessionProvider),
                new XAttribute("value", "false")));

            // Save the web.config file
            webConfigXmlDoc.Save(configPath);
        }
    }
}
