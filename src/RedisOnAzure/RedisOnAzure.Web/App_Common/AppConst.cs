using System.Configuration;


namespace RedisOnAzure.Web.App_Common
{
    public class AppConst
    {
        private static string _siteEnvironment;
        private static string _redisConnectionString;
        private static string _cacheContainer;

        public static string CacheContainer
        {
            get
            {
                return !string.IsNullOrWhiteSpace(_cacheContainer)
                    ? _cacheContainer
                    : string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["CacheContainer"])
                        ? "Local"
                        : ConfigurationManager.AppSettings["CacheContainer"];
            }
            set { _cacheContainer = value; }
        }

        public static string SiteEnvironment
        {
            get
            {
                return !string.IsNullOrWhiteSpace(_siteEnvironment)
                    ? _siteEnvironment
                    : string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["Site_Instance"])
                        ? "Local"
                        : ConfigurationManager.AppSettings["Site_Instance"];
            }
            set { _siteEnvironment = value; }
        }

        public static string RedisConnectionString
        {
            get
            {
                return !string.IsNullOrWhiteSpace(_redisConnectionString)
                    ? _redisConnectionString
                    : string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["RedisConnectionString"])
                        ? "localhost,allowAdmin=true"
                        : ConfigurationManager.AppSettings["RedisConnectionString"];
            }
            set { _redisConnectionString = value; }
        }
    }
}