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
        /// <returns>RedisConfig type from the RedisRepo library</returns>
        public static RedisConfig GetRedisConfig()
        {
            //\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
            // -- This method returns a singleton RedisConfig object                                                  \\
            // -- The RedisConfig type is an object that holds the metadata for connecting to a Redis server through  \\
            //    the StackExchange.Redis library.                                                                    \\
            // -- It should be a singleton in your application because the StackExchange.Redis library manages your   \\
            //    connection to the Redis server and that connection itself is a singleton.                           \\
            // -- This means that if you were to create a new RedisConfig object everytime you needed to access the   \\
            //    Redis server, you would eventually run out of connections available to the Redis Server, thus       \\
            //    freezing your application or making it come to a crawl.                                             \\
            //\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

            // Check to see if we already created a RedisConfig object and return it if we did.
            if (_redisConfig != null)
            {
                return _redisConfig;
            }

            // Set the Redis database we want to connect to inside of the Redis Server. This is a logical database with no security boundaries
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

            // Set the RedisConfig object singleton and return it
            _redisConfig = new RedisConfig(AppConst.RedisConnectionString, redisDatabase);
            return _redisConfig;
        }
    }
}