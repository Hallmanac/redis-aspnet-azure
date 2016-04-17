using RedisOnAzure.Web.App_Common;

using RedisRepo.Src;


namespace RedisOnAzure.Web
{
    public class RedisConfiguration
    {
        private static RedisConfig _redisConfig;

        /// <summary>
        /// Creates a RedisRepo configuration object (RedisRepo.RedisConfig) based on the current site environment
        /// </summary>
        /// <returns></returns>
        public static RedisConfig GetRedisConfig()
        {
            if (_redisConfig != null)
            {
                return _redisConfig;
            }
            var redisDatabase = -1;
            switch (AppConst.SiteEnvironment)
            {
                case "Local":
                    redisDatabase = 1;
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
            _redisConfig = new RedisConfig(AppConst.RedisConnectionString, redisDatabase);
            return _redisConfig;
        }
    }
}