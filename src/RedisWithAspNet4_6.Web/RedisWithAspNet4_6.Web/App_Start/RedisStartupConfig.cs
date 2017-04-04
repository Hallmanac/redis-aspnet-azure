using System.Configuration;

using RedisWithAspNet4_6.Web.App_Core.RedisServices;


namespace RedisWithAspNet4_6.Web.App_Start
{
    public class RedisStartupConfig
    {
        public static RedisConfig ConfigureRedis()
        {
            var siteEnvironment = ConfigurationManager.AppSettings["SiteEnvironment"];
            var redisConnectionString = ConfigurationManager.AppSettings["RedisConnectionString"];
            var redisDatabase = -1;
            switch(siteEnvironment)
            {
                case "Local":
                    redisDatabase = 2;
                    break;
                case "Dev":
                    redisDatabase = 0;
                    break;
                case "Production":
                    redisDatabase = 1;
                    break;
                case "QA":
                    redisDatabase = 3;
                    break;
                case "Test":
                    redisDatabase = 10;
                    break;
                case "Staging":
                    redisDatabase = 2;
                    break;
                default:
                    redisDatabase = 0;
                    break;
            }
            var redisConfig = new RedisConfig(redisConnectionString, redisDatabase);
            return redisConfig;
        }
    }
}
