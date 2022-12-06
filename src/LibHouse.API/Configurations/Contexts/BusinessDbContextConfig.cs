using LibHouse.Data.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace LibHouse.API.Configurations.Contexts
{
    internal static class BusinessDbContextConfig
    {
        internal static IServiceCollection AddLibHouseContext(
            this IServiceCollection services,
            IWebHostEnvironment environment, 
            string connectionString)
        {
            return environment.EnvironmentName switch
            {
                string currentEnvironment when currentEnvironment == Environments.Staging => GetStagingConfiguration(services, connectionString),
                string currentEnvironment when currentEnvironment == Environments.Development => GetDevelopmentConfiguration(services, connectionString),
                _ => GetStagingConfiguration(services, connectionString),
            };
        }

        private static IServiceCollection GetStagingConfiguration(
            IServiceCollection services, 
            string connectionString)
        {
            services.AddDbContext<LibHouseContext>(options =>
                options.UseSqlServer(
                    connectionString,
                    s => s.CommandTimeout(180)
                ).LogTo(
                    Console.WriteLine,
                    LogLevel.Debug,
                    DbContextLoggerOptions.LocalTime | DbContextLoggerOptions.SingleLine
                ).EnableDetailedErrors()
                .EnableSensitiveDataLogging()
            );
            return services;
        }

        private static IServiceCollection GetDevelopmentConfiguration(
            IServiceCollection services,
            string connectionString)
        {
            services.AddDbContext<LibHouseContext>(options =>
                options.UseSqlServer(
                    connectionString,
                    s => s.CommandTimeout(180)
                )
            );
            return services;
        }
    }
}