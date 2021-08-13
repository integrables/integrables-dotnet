using System;

namespace Store.Core.Caching.Memory
{
    public class MemoryCacheOptions : ICacheOptions
    {
        public TimeSpan DefaultTimeout { get; set; }
        public string InstanceName { get; set; }
    }
}