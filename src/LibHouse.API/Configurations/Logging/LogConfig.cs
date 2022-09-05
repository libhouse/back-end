using KissLog;
using KissLog.AspNetCore;
using KissLog.CloudListeners.Auth;
using KissLog.CloudListeners.RequestLogsListener;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace LibHouse.API.Configurations.Logging
{
    public static class LogConfig
    {
        public static IServiceCollection AddLoggingConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IKLogger>((provider) => Logger.Factory.Get());

            services.AddLogging(logging =>
            {
                logging.AddKissLog();
            });

            return services;
        }

        public static IApplicationBuilder UseLoggingConfiguration(
            this IApplicationBuilder app,
            IConfiguration configuration)
        {
            app.UseKissLogMiddleware(options => {
                ConfigureKissLog(options, configuration);
            });

            return app;
        }

        private static void ConfigureKissLog(
            IOptionsBuilder options, 
            IConfiguration configuration)
        {
            options.InternalLog = (message) =>
            {
                Debug.WriteLine(message);
            };

            RegisterKissLogListeners(options, configuration);
        }

        private static void RegisterKissLogListeners(
            IOptionsBuilder options, 
            IConfiguration configuration)
        {
            options.Listeners.Add(new RequestLogsApiListener(new Application(
                configuration["KissLog:KissLog.OrganizationId"],
                configuration["KissLog:KissLog.ApplicationId"])
            )
            {
                ApiUrl = configuration["KissLog:KissLog.ApiUrl"]
            });
        }
    }
}