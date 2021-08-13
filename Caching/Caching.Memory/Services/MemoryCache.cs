using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Caching.Memory.Services
{
    /// <summary>
    /// Service responsible for memory caching
    /// </summary>
    public class MemoryCache : ICache
    {
        private readonly IMemoryCache _memoryCache;
        private readonly MemoryCacheOptions _memoryCacheOptions;

        /// <summary>
        /// Create new instance of <see cref="MemoryCache"/>
        /// </summary>
        /// <param name="memoryCacheOptions"></param>
        /// <param name="memoryCache"></param>
        public MemoryCache(IOptions<MemoryCacheOptions> memoryCacheOptions, IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _memoryCacheOptions = memoryCacheOptions.Value;
        }
        
        /// <inheritdoc/>
        public async Task<CacheEntity<T>> GetOrCreateAsync<T>(string key, Func<Task<T>> create, CacheEntryOptions cacheEntryOptions = null)
        {
            var data = await GetAsync<T>(GetKey(key));
            var isFromCache = true;
            if (Equals(data, default(T)))
            {
                data = await create();
                await SetAsync(GetKey(key), data, cacheEntryOptions);
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
            await Task.Run(() =>
            {
                var memoryCacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(cacheEntryOptions?.Timeout ?? _memoryCacheOptions.DefaultTimeout))
                    .SetSlidingExpiration(TimeSpan.FromSeconds(cacheEntryOptions?.SlidingTimeout ?? _memoryCacheOptions.DefaultSlidingTimeout));

                _memoryCache.Set(GetKey(key), data, memoryCacheEntryOptions);
            });
        }

        /// <inheritdoc/>
        public async Task<T> GetAsync<T>(string key)
        {
            return await Task.Run(() => _memoryCache.Get<T>(GetKey(key)));
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(string key)
        {
            await Task.Run(() => _memoryCache.Remove(GetKey(key)));
        }

        private string GetKey(string key)
        {
            return $"{_memoryCacheOptions.InstanceName}{key}";
        }
    }
}