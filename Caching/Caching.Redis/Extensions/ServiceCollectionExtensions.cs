using System;
using Caching.Redis.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Caching.Redis.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRedisCaching(this IServiceCollection services, Action<RedisCacheOptions> configureOptions)
        {
            var redisCacheOptions = new RedisCacheOptions();
            configureOptions(redisCacheOptions); 
            
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisCacheOptions.ConnectionString;
                options.InstanceName = redisCacheOptions.InstanceName;
            });
            services.Configure(configureOptions);
            services.AddScoped<ICache, RedisCache>();
        }
    }
}