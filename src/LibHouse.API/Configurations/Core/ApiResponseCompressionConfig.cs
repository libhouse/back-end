using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using System.IO.Compression;

namespace LibHouse.API.Configurations.Core
{
    public static class ApiResponseCompressionConfig
    {
        public static IServiceCollection AddApiResponseCompressionConfig(this IServiceCollection services)
        {
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);

            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
                options.EnableForHttps = true;
            });

            return services;
        }
    }
}