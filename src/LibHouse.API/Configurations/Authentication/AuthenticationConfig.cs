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
            var tokenSettingsSection = configuration.GetSection("AccessTokenSettings");
            services.Configure<AccessTokenSettings>(tokenSettingsSection);

            var refreshTokenSettingsSection = configuration.GetSection("RefreshTokenSettings");
            services.Configure<RefreshTokenSettings>(refreshTokenSettingsSection);

            var tokenSettings = tokenSettingsSection.Get<AccessTokenSettings>();
            var key = Encoding.ASCII.GetBytes(tokenSettings.Secret);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidAudience = tokenSettings.ValidIn,
                ValidIssuer = tokenSettings.Issuer,
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