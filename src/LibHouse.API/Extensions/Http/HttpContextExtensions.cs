using LibHouse.Infrastructure.Authentication.Token.Services.RefreshTokens;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace LibHouse.API.Extensions.Http
{
    internal static class HttpContextExtensions
    {
        public static async Task<bool> CheckIfUserAccessTokenIsRevokedAsync(this HttpContext context)
        {
            var tokenValidation = context.RequestServices.GetRequiredService<TokenValidationParameters>();

            string accessToken = context.Request.GetBearerTokenValueFromAuthorizationHeader();

            _ = new JwtSecurityTokenHandler().ValidateToken(accessToken, tokenValidation, out var securityToken);

            var refreshTokenService = context.RequestServices.GetRequiredService<IRefreshTokenService>();

            return await refreshTokenService.CheckIfRefreshTokenIsRevokedBasedOnAccessTokenIdAsync(securityToken.Id);
        }
    }
}