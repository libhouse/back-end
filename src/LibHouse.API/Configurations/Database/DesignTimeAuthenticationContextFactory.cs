using LibHouse.Infrastructure.Authentication.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace LibHouse.API.Configurations.Database
{
    public class DesignTimeAuthenticationContextFactory : IDesignTimeDbContextFactory<AuthenticationContext>
    {
        const string AppSettingsPath = "/../LibHouse.API/appsettings.json";

        public AuthenticationContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@Directory.GetCurrentDirectory() + AppSettingsPath).AddUserSecrets<Startup>().Build();

            var builder = new DbContextOptionsBuilder<AuthenticationContext>();

            string connectionString = configuration.GetConnectionString("LibHouseAuthConnectionString");

            builder.UseSqlServer(connectionString);

            return new AuthenticationContext(builder.Options);
        }
    }
}