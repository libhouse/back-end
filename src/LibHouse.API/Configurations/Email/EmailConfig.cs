using LibHouse.Infrastructure.Email.Services;
using LibHouse.Infrastructure.Email.Settings;
using LibHouse.Infrastructure.Email.Settings.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LibHouse.API.Configurations.Email
{
    internal static class EmailConfig
    {
        public static IServiceCollection AddEmailConfig(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<MailSettings>(options => configuration.GetSection("MailSettings").Bind(options));

            services.AddSingleton<IMailService, MailKitService>();

            return services;
        }
    }
}