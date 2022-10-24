using LibHouse.Infrastructure.Email.Services;
using LibHouse.Infrastructure.Email.Settings.Shared;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LibHouse.API.Configurations.Email
{
    internal static class EmailConfig
    {
        public static IServiceCollection AddEmailConfig(
            this IServiceCollection services,
            IConfiguration configuration,
            IWebHostEnvironment environment)
        {
            services.AddSingleton(settings => new MailSettings
            {
                DisplayName = configuration.GetValue<string>("MailSettings.DisplayName"),
                Host = configuration.GetValue<string>("MailSettings.Host"),
                Mail = configuration.GetValue<string>("MailSettings.Mail"),
                Password = configuration.GetValue<string>("MailSettings.Password"),
                Port = configuration.GetValue<int>("MailSettings.Port")
            });
            if (environment.IsStaging())
            {
                services.AddSingleton<IMailService, StagingMailService>();
            }
            else
            {
                services.AddSingleton<IMailService, MailKitService>();
            }
            return services;
        }
    }
}