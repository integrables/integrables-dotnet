using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Caching.Redis.Services
{
    /// <summary>
    /// Service responsible for memory caching
    /// </summary>
    public class RedisCache : ICache
    {
        private readonly IDistributedCache _distributedCache;
        private readonly RedisCacheOptions _redisCacheOptions;

        /// <summary>
        /// Create new instance of <see cref="RedisCache"/>
        /// </summary>
        public RedisCache(IOptions<RedisCacheOptions> redisCacheOptions, IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
            _redisCacheOptions = redisCacheOptions.Value;
        }
        
        /// <inheritdoc/>
        public async Task<CacheEntity<T>> GetOrCreateAsync<T>(string key, Func<Task<T>> create, CacheEntryOptions cacheEntryOptions = null)
        {
            var data = await GetAsync<T>(key);
            var isFromCache = true;
            if (Equals(data, default(T)))
            {
                data = await create();
                await SetAsync(key, data, cacheEntryOptions);
                isFromCache = false;
            }
            return new CacheEntity<T>()
            {
                Data = data,
                IsFromCache = isFromCache
            };
        }

        /// <inheritdoc/>
        public async Task SetAsync<T>(string key, T data, CacheEntryOptions cacheEntryOptions = null)
        {
            var dataString = JsonSerializer.Serialize<T>(data);
            var distributedCacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(cacheEntryOptions?.Timeout ?? _redisCacheOptions.DefaultTimeout),
                SlidingExpiration = TimeSpan.FromSeconds(cacheEntryOptions?.SlidingTimeout ?? _redisCacheOptions.DefaultSlidingTimeout)
            };
            await _distributedCache.SetStringAsync(key, dataString, distributedCacheOptions);
        }

        /// <inheritdoc/>
        public async Task<T> GetAsync<T>(string key)
        {
            var cachedData = await _distributedCache.GetStringAsync(key);
            return cachedData is null ? default(T) : JsonSerializer.Deserialize<T>(cachedData);
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(string key)
        {
            await _distributedCache.RemoveAsync(key);
        }
    }
}