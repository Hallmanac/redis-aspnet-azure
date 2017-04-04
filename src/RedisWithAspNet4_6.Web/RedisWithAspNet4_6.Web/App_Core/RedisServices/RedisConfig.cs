using System;

using StackExchange.Redis;


namespace RedisWithAspNet4_6.Web.App_Core.RedisServices
{
    /// <summary>
	///     Use this class to define the configuration of the Redis server. It is HIGHLY recommended that this class be
	///     a singleton. The RedisMultiplexer is generated in a thread-safe manner.
	/// </summary>
    public class RedisConfig
    {
        private static ConnectionMultiplexer _redisMultiplexer;
		private readonly Object _thisLock = new object();
		private readonly ConfigurationOptions _configOptions;
		private readonly bool _useConfigOptions;

		public RedisConfig(string redisConnectionString, int redisDatabaseId)
		{
			RedisDatabaseId = redisDatabaseId;
			RedisConnectionString = redisConnectionString;
			_useConfigOptions = false;
		}

		public RedisConfig(ConfigurationOptions configOptions, int redisDatabaseId)
		{
			_configOptions = configOptions;
			RedisDatabaseId = redisDatabaseId;
			_useConfigOptions = true;
		}

		public string RedisConnectionString { get; private set; }

		public int RedisDatabaseId { get; private set; }

		public ConnectionMultiplexer RedisMultiplexer
		{
			get
			{
				if (_redisMultiplexer != null)
					return _redisMultiplexer;
				lock(_thisLock)
				{
					_redisMultiplexer = !_useConfigOptions ? ConnectionMultiplexer.Connect(RedisConnectionString)
						: ConnectionMultiplexer.Connect(_configOptions);
				}
				return _redisMultiplexer;
			}
		}
    }
}
