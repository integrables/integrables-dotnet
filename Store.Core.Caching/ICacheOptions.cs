using System;

namespace Store.Core.Caching
{
    public interface ICacheOptions
    {
        TimeSpan DefaultTimeout { get; set; }
        string InstanceName { get; set; }
    }
}