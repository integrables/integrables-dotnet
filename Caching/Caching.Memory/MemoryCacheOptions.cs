using Caching.Memory.Services;

namespace Caching.Memory
{
    /// <summary>
    /// Configuration options for <see cref="MemoryCache"/>
    /// </summary>
    public class MemoryCacheOptions : ICacheOptions
    {
        /// <inheritdoc/>
        public int DefaultTimeout { get; set; }

        /// <inheritdoc/>
        public int DefaultSlidingTimeout { get; set; }

        /// <inheritdoc/>
        public string InstanceName { get; set; }
        
        /// Limits the maximum memory size available for caching
        public int SizeLimit { get; set; }
    }
}