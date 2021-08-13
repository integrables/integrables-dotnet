using System;
using Microsoft.Extensions.DependencyInjection;
using Store.Core.Caching.Memory.Services;

namespace Store.Core.Caching.Memory.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMemoryCaching(this IServiceCollection services, Action<MemoryCacheOptions> configureOptions)
        {
            services.AddMemoryCache();
            services.Configure(configureOptions);
            services.AddScoped<ICache, MemoryCache>();
        }
    }
}