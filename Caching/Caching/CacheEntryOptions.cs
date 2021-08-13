using System;

namespace Caching
{
    /// <summary>
    /// Options for storing cache entry
    /// </summary>
    public class CacheEntryOptions
    {
        /// <summary>
        /// timeout (in seconds) used for the storing the cache entry
        /// </summary>
        public int Timeout { get; set; }
        
        /// <summary>
        /// Timeout (in seconds) used for the storing the cache entry when its not called
        /// </summary>
        public int SlidingTimeout { get; set; }
    }
}