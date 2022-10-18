using LibHouse.Infrastructure.Authentication.Token.Generators.AccessTokens;
using LibHouse.Infrastructure.Authentication.Token.Generators.RefreshTokens;
using LibHouse.Infrastructure.Authentication.Token.Services.RefreshTokens;
using LibHouse.Infrastructure.Authentication.Token.Settings;
using LibHouse.Infrastructure.Authentication.Token.Validations.RefreshTokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LibHouse.API.Configurations.Authentication
{
    public static class AuthenticationConfig
    {
        public static IServiceCollection AddAuthenticationConfiguration(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSingleton(settings => new AccessTokenSettings()
            {
                ExpiresInSeconds = configuration.GetValue<int>("AccessTokenSettings.ExpiresInSeconds"),
                Issuer = configuration.GetValue<string>("AccessTokenSettings.Issuer"),
                Secret = configuration.GetValue<string>("AccessTokenSettings.Secret"),
                ValidIn = configuration.GetValue<string>("AccessTokenSettings.ValidIn")
            });

            services.AddSingleton(settings => new RefreshTokenSettings()
            {
                ExpiresInMonths = configuration.GetValue<int>("RefreshTokenSettings.ExpiresInMonths"),
                TokenLength = configuration.GetValue<int>("RefreshTokenSettings.TokenLength")
            });

            var key = Encoding.ASCII.GetBytes(configuration.GetValue<string>("AccessTokenSettings.Secret"));
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidAudience = configuration.GetValue<string>("AccessTokenSettings.ValidIn"),
                ValidIssuer = configuration.GetValue<string>("AccessTokenSettings.Issuer"),
            };

            services.AddSingleton(tokenValidationParameters);

            services.AddAuthentication(x => {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = tokenValidationParameters;
            });

            services.AddScoped<IRefreshTokenGenerator, JwtRefreshTokenGenerator>();

            services.AddScoped<IAccessTokenGenerator, JwtAccessTokenGenerator>();

            services.AddSingleton<IRefreshTokenValidator, RefreshTokenValidator>();

            services.AddScoped<IRefreshTokenService, IdentityRefreshTokenService>();

            return services;
        }
    }
}