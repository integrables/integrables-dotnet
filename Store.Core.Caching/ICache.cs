using System;
using System.Threading.Tasks;

namespace Store.Core.Caching
{
    public interface ICache
    {
        Task<CacheEntity<T>> GetOrCreateAsync<T>(string key, Func<Task<T>> create, TimeSpan? timeout = null);
        Task SetAsync<T>(string key, T data, TimeSpan timeout);
        Task<T> GetAsync<T>(string key);
        Task DeleteAsync(string key);
    }
}