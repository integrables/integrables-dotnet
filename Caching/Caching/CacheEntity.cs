namespace Caching
{
    /// <summary>
    /// Holds the cache entry and another caching meta data
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CacheEntity<T>
    {
        /// <summary>
        /// Cache entry value
        /// </summary>
        public T Data { get; set; }
        
        /// <summary>
        /// Indicates whether the value is retrieved from the cache or reevaluated again.
        /// </summary>
        public bool IsFromCache { get; set; }
    }
}