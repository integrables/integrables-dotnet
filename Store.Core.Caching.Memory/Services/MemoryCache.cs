using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Store.Core.Caching.Memory.Services
{
    public class MemoryCache : ICache
    {
        private readonly IMemoryCache _memoryCache;
        private readonly MemoryCacheOptions _options;

        public MemoryCache(IOptions<MemoryCacheOptions> options, IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _options = options.Value;
        }
        
        public async Task<CacheEntity<T>> GetOrCreateAsync<T>(string key, Func<Task<T>> create, TimeSpan? timeout = null)
        {
            var data = await GetAsync<T>(GetKey(key));
            var isFromCache = true;
            if (data is null)
            {
                data = await create();
                await SetAsync(GetKey(key), data, timeout ?? _options.DefaultTimeout);
                isFromCache = false;
            }
            return new CacheEntity<T>()
            {
                Data = data,
                IsFromCache = isFromCache
            };
        }

        public async Task SetAsync<T>(string key, T data, TimeSpan timeout)
        {
            await Task.Run(() =>
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(timeout)
                    .SetSlidingExpiration(timeout);

                _memoryCache.Set(GetKey(key), data, cacheEntryOptions);
            });
        }

        public async Task<T> GetAsync<T>(string key)
        {
            return await Task.Run(() => _memoryCache.Get<T>(GetKey(key)));
        }

        public async Task DeleteAsync(string key)
        {
            await Task.Run(() => _memoryCache.Remove(GetKey(key)));
        }

        private string GetKey(string key)
        {
            return $"{_options.InstanceName}{key}";
        }
    }
}