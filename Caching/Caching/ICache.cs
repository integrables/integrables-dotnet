using System;
using System.Threading.Tasks;

namespace Caching
{
    /// <summary>
    /// Service responsible for caching
    /// </summary>
    public interface ICache
    {
        /// <summary>
        /// Try get the the value from cache or creates new cache entry and return it
        /// </summary>
        /// <param name="key"></param>
        /// <param name="create"></param>
        /// <param name="cacheEntryOptions"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<CacheEntity<T>> GetOrCreateAsync<T>(string key, Func<Task<T>> create, CacheEntryOptions cacheEntryOptions = null);
        
        /// <summary>
        /// Set cache entry 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="cacheEntryOptions"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task SetAsync<T>(string key, T data, CacheEntryOptions cacheEntryOptions = null);
        
        /// <summary>
        /// Get cache entry
        /// </summary>
        /// <param name="key"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> GetAsync<T>(string key);
        
        /// <summary>
        /// deletes cache entry
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task DeleteAsync(string key);
    }
}