using LibHouse.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace LibHouse.API.Configurations.Database
{
    public class DesignTimeLibHouseContextFactory : IDesignTimeDbContextFactory<LibHouseContext>
    {
        public LibHouseContext CreateDbContext(string[] args)
        {
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@Directory.GetCurrentDirectory() + "/../LibHouse.API/appsettings.json", false, true)
                .AddJsonFile(@Directory.GetCurrentDirectory() + $"/../LibHouse.API/appsettings.{environment}.json", true)
                .Build();
            var builder = new DbContextOptionsBuilder<LibHouseContext>();
            string connectionString = configuration.GetValue<string>("LibHouseConnectionString");
            builder
             .UseSqlServer(connectionString, s => s.CommandTimeout(180).EnableRetryOnFailure(3, TimeSpan.FromSeconds(5), null))
             .LogTo(Console.WriteLine, LogLevel.Information, DbContextLoggerOptions.LocalTime | DbContextLoggerOptions.SingleLine)
             .EnableDetailedErrors()
             .EnableSensitiveDataLogging();
            return new LibHouseContext(builder.Options);
        }
    }
}