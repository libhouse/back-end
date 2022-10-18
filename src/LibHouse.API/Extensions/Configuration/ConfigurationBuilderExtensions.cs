using LibHouse.API.Configurations.Secrets;
using Microsoft.Extensions.Configuration;

namespace LibHouse.API.Extensions.Configuration
{
    internal static class ConfigurationBuilderExtensions
    {
        internal static void AddAmazonSecretsManager(
            this IConfigurationBuilder configurationBuilder,
            string region, 
            string secretName)
        {
            AmazonSecretsManagerConfigurationSource configurationSource = new(region, secretName);

            configurationBuilder.Add(configurationSource);
        }
    }
}