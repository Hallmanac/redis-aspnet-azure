using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedisWithAspNet4_6.Web.App_Core.RedisServices
{
    public interface IAppCache
    {
        /// <summary>
		/// Determines whether an item with the given cache key is in the cache.
		/// </summary>
		/// <param name="key">Cache Key</param>
		/// <param name="partitionName">
		/// Optional value for the name of a partition in the application cache. Partitions can be a good way to categorize or group
		/// certain types of items in the cache together.
		/// </param>
		/// <returns>Whether or not there is an item in the cache with that key and partition name</returns>
		bool Contains(string key, string partitionName = "");


        /// <summary>
		/// Determines whether an item with the given cache key is in the cache.
		/// </summary>
		/// <param name="key">Cache Key</param>
		/// <param name="partitionName">
		/// Optional value for the name of a partition in the application cache. Partitions can be a good way to categorize or group
		/// certain types of items in the cache together.
		/// </param>
		/// <returns>Whether or not there is an item in the cache with that key and partition name</returns>
		Task<bool> ContainsAsync(string key, string partitionName = "");


        /// <summary>
        /// Gets a regular Object out of the cache.
        /// </summary>
        /// <param name="key">Cache key</param>
        /// <param name="partitionName">
        /// Optional value for the name of a partition in the application cache. Partitions can be a good way to categorize or group
		/// certain types of items in the cache together.
        /// </param>
        /// <returns>Regular (untyped) C# object representing the value in the cache</returns>
		object Get(string key, string partitionName = "");


        /// <summary>
        /// Gets a regular Object out of the cache.
        /// </summary>
        /// <param name="key">Cache key</param>
        /// <param name="partitionName">
        /// Optional value for the name of a partition in the application cache. Partitions can be a good way to categorize or group
		/// certain types of items in the cache together.
        /// </param>
        /// <returns>Regular (untyped) C# object representing the value in the cache</returns>
		Task<object> GetAsync(string key, string partitionName = "");

        
        /// <summary>
        /// Gets the strongly typed value out of the cache based on the given key (and optional partition name)
        /// </summary>
        /// <typeparam name="TValue">Strong type representing the return value</typeparam>
        /// <param name="key">Cache Key</param>
        /// <param name="partitionName">
        /// Optional value for the name of a partition in the application cache. Partitions can be a good way to categorize or group
		/// certain types of items in the cache together.
        /// </param>
        /// <returns>Value in the cache</returns>
		TValue GetValue<TValue>(string key, string partitionName = "") where TValue : class;


        /// <summary>
        /// Gets the strongly typed value out of the cache based on the given key (and optional partition name)
        /// </summary>
        /// <typeparam name="TValue">Strong type representing the return value</typeparam>
        /// <param name="key">Cache Key</param>
        /// <param name="partitionName">
        /// Optional value for the name of a partition in the application cache. Partitions can be a good way to categorize or group
		/// certain types of items in the cache together.
        /// </param>
        /// <returns>Value in the cache</returns>
		Task<TValue> GetValueAsync<TValue>(string key, string partitionName = "") where TValue : class;

        
        /// <summary>
        /// Gets all the partition names that have been entered into the cache. Each time a partition is given to be used, it is 
        /// saved in the cache for tracking purposes.
        /// </summary>
        /// <returns>List of all partition names</returns>
		Task<List<string>> GetAllPartitionNamesAsync();

        
        /// <summary>
        /// Gets all the items in a given partition.
        /// </summary>
        /// <typeparam name="TValue">Type of the return value</typeparam>
        /// <param name="partitionName">
        /// Optional value for the name of a partition in the application cache. Partitions can be a good way to categorize or group
		/// certain types of items in the cache together.
        /// </param>
        /// <returns>List of all items in a partition</returns>
		List<TValue> GetAllItemsInPartition<TValue>(string partitionName) where TValue : class;


        /// <summary>
        /// Gets all the items in a given partition.
        /// </summary>
        /// <typeparam name="TValue">Type of the return value</typeparam>
        /// <param name="partitionName">
        /// Optional value for the name of a partition in the application cache. Partitions can be a good way to categorize or group
		/// certain types of items in the cache together.
        /// </param>
        /// <returns>List of all items in a partition</returns>
		Task<List<TValue>> GetAllItemsInPartitionAsync<TValue>(string partitionName) where TValue : class;


        /// <summary>
        /// Inserts or updates the value in the cache.
        /// </summary>
        /// <param name="key">Cache Key</param>
        /// <param name="value">Value to add or update</param>
        /// <param name="timeout">Timespan of time to live in the cache. If null, then it will stay in the cache indefinitely.</param>
        /// <param name="partitionName">
        /// Optional value for the name of a partition in the application cache. Partitions can be a good way to categorize or group
		/// certain types of items in the cache together.
        /// </param>
		void AddOrUpdate(string key, object value, TimeSpan? timeout, string partitionName = "");


        /// <summary>
        /// Inserts or updates the value in the cache.
        /// </summary>
        /// <param name="key">Cache Key</param>
        /// <param name="value">Value to add or update</param>
        /// <param name="timeout">Timespan of time to live in the cache. If null, then it will stay in the cache indefinitely.</param>
        /// <param name="partitionName">
        /// Optional value for the name of a partition in the application cache. Partitions can be a good way to categorize or group
		/// certain types of items in the cache together.
        /// </param>
		Task AddOrUpdateAsync(string key, object value, TimeSpan? timeout, string partitionName = "");

        
        /// <summary>
        /// Adds or updates a partition (a.k.a. hash type) in the redis cache in on operation instead of looping through each individual hash field and hash value.
        /// </summary>
        /// <param name="partitionName">Name of partition (i.e. redis key for the hash type)</param>
        /// <param name="collectionValues">Dictionary of key value pairs that will be stored as hash field/value pairs in Redis</param>
        /// <param name="timeout">Optional timeout applied to items in the given set of collection values passed in</param>
        Task AddCollectionToPartitionAsync<T>(string partitionName, Dictionary<string, T> collectionValues, TimeSpan? timeout = null) where T : class;


        /// <summary>
        /// Removes an item from the cache
        /// </summary>
        /// <param name="key">Cache key</param>
        /// <param name="partitionName">
        /// Optional value for the name of a partition in the application cache. Partitions can be a good way to categorize or group
		/// certain types of items in the cache together.
        /// </param>
		void Remove(string key, string partitionName = "");


        /// <summary>
        /// Removes an item from the cache
        /// </summary>
        /// <param name="key">Cache key</param>
        /// <param name="partitionName">
        /// Optional value for the name of a partition in the application cache. Partitions can be a good way to categorize or group
		/// certain types of items in the cache together.
        /// </param>
		Task RemoveAsync(string key, string partitionName = "");
        
        
        /// <summary>
        /// Removes all items from a partition whose custom timeout has expired.
        /// </summary>
        /// <param name="partitionName">
        /// The name of a partition in the application cache. Partitions can be a good way to categorize or group
		/// certain types of items in the cache together.
        /// </param>
        /// <returns>Void</returns>
		Task RemoveExpiredItemsFromPartitionAsync(string partitionName);

        
        /// <summary>
        /// Clears out all Items in a cache. For Redis, it only clears out items that belong to the particular database inside the redis server
        /// based on the connection string of the current connection.
        /// </summary>
		void ClearCache();


        /// <summary>
        /// Clears out all Items in a cache. For Redis, it only clears out items that belong to the particular database inside the redis server
        /// based on the connection string of the current connection.
        /// </summary>
		Task ClearCacheAsync();
    }
}
