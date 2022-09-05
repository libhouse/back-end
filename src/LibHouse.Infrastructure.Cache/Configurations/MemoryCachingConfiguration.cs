namespace LibHouse.Infrastructure.Cache.Configurations
{
    public class MemoryCachingConfiguration
    {
        public long CacheSizeLimit { get; set; }
        public long? CacheEntrySize { get; set; }
    }
}