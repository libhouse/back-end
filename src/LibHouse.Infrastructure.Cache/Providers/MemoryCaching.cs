using LibHouse.Infrastructure.Cache.Configurations;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace LibHouse.Infrastructure.Cache.Providers
{
    public class MemoryCaching
    {
        private readonly MemoryCache _cache;
        private readonly MemoryCachingConfiguration _memoryCachingConfiguration;

        public MemoryCaching(
            IOptions<MemoryCachingConfiguration> memoryCachingConfig)
        {
            _memoryCachingConfiguration = memoryCachingConfig.Value;

            _cache = new(new MemoryCacheOptions()
            {
                SizeLimit = _memoryCachingConfiguration.CacheSizeLimit,
            });
        }

        public async Task<Resource> GetOrCreateResourceAsync<Resource>(
            string key,
            TimeSpan slidingExpiration,
            TimeSpan absoluteExpiration,
            Func<Task<Resource>> getResource)
        {
            return await _cache.GetOrCreateAsync(
                key,
                cacheEntry =>
                {
                    cacheEntry.SlidingExpiration = slidingExpiration;
                    cacheEntry.AbsoluteExpirationRelativeToNow = absoluteExpiration;
                    cacheEntry.Size = _memoryCachingConfiguration.CacheEntrySize;
                    return getResource();
                }
            );
        }

        public async Task<Resource> GetOrCreateResourceAsync<Resource>(
            string key,
            TimeSpan slidingExpiration,
            TimeSpan absoluteExpiration,
            Resource resource)
        {
            return await _cache.GetOrCreateAsync(
                key,
                cacheEntry =>
                {
                    cacheEntry.SlidingExpiration = slidingExpiration;
                    cacheEntry.AbsoluteExpirationRelativeToNow = absoluteExpiration;
                    cacheEntry.Size = _memoryCachingConfiguration.CacheEntrySize;
                    return Task.FromResult(resource);
                }
            );
        }
    }
}