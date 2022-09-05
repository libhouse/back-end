using LibHouse.Infrastructure.Cache.Configurations;
using LibHouse.Infrastructure.Cache.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LibHouse.API.Configurations.Cache
{
    public static class CachingConfig
    {
        public static IServiceCollection AddApiCachingConfig(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.Configure<MemoryCachingConfiguration>(
                options => configuration.GetSection("MemoryCachingConfiguration").Bind(options));

            services.AddSingleton<MemoryCaching>();

            return services;
        }
    }
}