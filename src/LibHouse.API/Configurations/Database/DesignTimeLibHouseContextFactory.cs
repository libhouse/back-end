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
        const string AppSettingsPath = "/../LibHouse.API/appsettings.json";

        public LibHouseContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@Directory.GetCurrentDirectory() + AppSettingsPath).AddUserSecrets<Startup>().Build();

            var builder = new DbContextOptionsBuilder<LibHouseContext>();

            string connectionString = configuration.GetConnectionString("LibHouseConnectionString");

            builder
             .UseSqlServer(connectionString, s => s.CommandTimeout(180).EnableRetryOnFailure(3, TimeSpan.FromSeconds(5), null))
             .LogTo(Console.WriteLine, LogLevel.Information, DbContextLoggerOptions.LocalTime | DbContextLoggerOptions.SingleLine)
             .EnableDetailedErrors()
             .EnableSensitiveDataLogging();

            return new LibHouseContext(builder.Options);
        }
    }
}