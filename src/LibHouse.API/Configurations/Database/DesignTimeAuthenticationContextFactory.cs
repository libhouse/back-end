using LibHouse.Infrastructure.Authentication.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace LibHouse.API.Configurations.Database
{
    public class DesignTimeAuthenticationContextFactory : IDesignTimeDbContextFactory<AuthenticationContext>
    {
        public AuthenticationContext CreateDbContext(string[] args)
        {
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@Directory.GetCurrentDirectory() + "/../LibHouse.API/appsettings.json", false, true)
                .AddJsonFile(@Directory.GetCurrentDirectory() + $"/../LibHouse.API/appsettings.{environment}.json", true)
                .Build();
            var builder = new DbContextOptionsBuilder<AuthenticationContext>();
            string connectionString = configuration.GetValue<string>("LibHouseAuthConnectionString");
            builder.UseSqlServer(connectionString);
            return new AuthenticationContext(builder.Options);
        }
    }
}