namespace Caching.Redis
{
    /// <summary>
    /// Configuration options for RedisCache
    /// </summary>
    public class RedisCacheOptions : ICacheOptions
    {
        /// <inheritdoc/>
        public int DefaultTimeout { get; set; }

        /// <inheritdoc/>
        public int DefaultSlidingTimeout { get; set; }

        /// <inheritdoc/>
        public string InstanceName { get; set; }
        
        /// <summary>
        /// Redis server connection string 
        /// </summary>
        public string ConnectionString { get; set; }
    }
}