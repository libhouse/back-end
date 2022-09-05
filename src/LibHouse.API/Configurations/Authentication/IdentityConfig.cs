using LibHouse.Infrastructure.Authentication.Context;
using LibHouse.Infrastructure.Authentication.Token.Providers.EmailConfirmation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace LibHouse.API.Configurations.Authentication
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfiguration(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("LibHouseAuthConnectionString");

            services.AddDbContext<AuthenticationContext>(options => options.UseSqlServer(connectionString));

            services
              .AddIdentity<IdentityUser, IdentityRole>(opt =>
              {
                  opt.User.RequireUniqueEmail = true;
                  opt.SignIn.RequireConfirmedEmail = true;
                  opt.Tokens.EmailConfirmationTokenProvider = "EmailConfirmation";
              })
              .AddEntityFrameworkStores<AuthenticationContext>()
              .AddDefaultTokenProviders()
              .AddTokenProvider<EmailConfirmationTokenProvider<IdentityUser>>("EmailConfirmation");

            services.Configure<DataProtectionTokenProviderOptions>(opt => opt.TokenLifespan = TimeSpan.FromHours(1));

            services.Configure<EmailConfirmationTokenProviderOptions>(opt => opt.TokenLifespan = TimeSpan.FromDays(2));

            return services;
        }
    }
}