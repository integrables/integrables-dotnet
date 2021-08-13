using System;

namespace Caching
{
    /// <summary>
    /// Caching configuration options
    /// </summary>
    public interface ICacheOptions
    {
        /// <summary>
        /// Default timeout (in seconds) used for the storing the cache entry
        /// </summary>
        int DefaultTimeout { get; set; }
        
        /// <summary>
        /// Default Timeout (in seconds) used for the storing the cache entry when its not called
        /// </summary>
        int DefaultSlidingTimeout { get; set; }
        
        /// <summary>
        /// Name of the application that use the cache service
        /// </summary>
        string InstanceName { get; set; }
    }
}