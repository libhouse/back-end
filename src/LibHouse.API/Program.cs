using Amazon;
using LibHouse.API.Configurations.Secrets;
using LibHouse.API.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace LibHouse.API
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, builder) =>
                {
                    if (context.HostingEnvironment.IsStaging())
                    {
                        builder.AddUserSecrets<Startup>();
                    }
                    else
                    {
                        builder.AddAmazonSecretsManager(RegionEndpoint.USEast1.DisplayName, AmazonSecretName.Value);
                    }
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}