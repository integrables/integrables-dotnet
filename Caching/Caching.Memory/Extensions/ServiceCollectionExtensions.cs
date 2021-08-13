using System;
using Caching.Memory.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Caching.Memory.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMemoryCaching(this IServiceCollection services, Action<MemoryCacheOptions> configureOptions)
        {
            var memoryCacheOptions = new MemoryCacheOptions();
            configureOptions(memoryCacheOptions); 
            
            services.AddMemoryCache(options =>
            {
                options.SizeLimit = memoryCacheOptions.SizeLimit;
            });
            services.Configure(configureOptions);
            services.AddScoped<ICache, MemoryCache>();
        }
    }
}