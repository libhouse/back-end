using LibHouse.Business.Application.Users.Gateways;
using LibHouse.Business.Application.Users.Gateways.Outputs;
using LibHouse.Business.Monads;
using LibHouse.Infrastructure.Authentication.Token.Generators.AccessTokens;
using LibHouse.Infrastructure.Authentication.Token.Models;
using LibHouse.Infrastructure.Authentication.Token.Services.RefreshTokens;
using LibHouse.Infrastructure.Authentication.Token.Validations.RefreshTokens;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LibHouse.Infrastructure.Authentication.Login
{
    public class IdentityUserLoginRenewalGateway : IUserLoginRenewalGateway
    {
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly IAccessTokenGenerator _accessTokenGenerator;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IRefreshTokenValidator _refreshTokenValidator;

        public IdentityUserLoginRenewalGateway(
            TokenValidationParameters tokenValidationParameters,
            IAccessTokenGenerator accessTokenGenerator, 
            IRefreshTokenService refreshTokenService,
            IRefreshTokenValidator refreshTokenValidator)
        {
            _tokenValidationParameters = tokenValidationParameters;
            _accessTokenGenerator = accessTokenGenerator;
            _refreshTokenService = refreshTokenService;
            _refreshTokenValidator = refreshTokenValidator;
        }

        public async Task<OutputUserLoginRenewalGateway> RenewLoginAsync(
            string userName, 
            string accessTokenValue,
            string refreshTokenValue)
        {
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new();

            AccessToken accessToken = new(accessTokenValue);

            ClaimsPrincipal accessTokenClaims = jwtSecurityTokenHandler.ValidateToken(accessToken.Value, _tokenValidationParameters, out var securityToken);

            RefreshToken refreshToken = await _refreshTokenService.GetRefreshTokenByValueAsync(refreshTokenValue);

            Result refreshTokenCanBeUsed = _refreshTokenValidator.CheckIfRefreshTokenCanBeUsedWithAccessToken(refreshToken, securityToken, accessTokenClaims);

            if (refreshTokenCanBeUsed.Failure)
            {
                return new(isSuccess: false, loginRenewalMessage: refreshTokenCanBeUsed.Error);
            }

            _ = await _refreshTokenService.MarkRefreshTokenAsRevokedAsync(refreshToken);

            AccessToken newAccessToken = await _accessTokenGenerator.GenerateAccessTokenAsync(userName);

            return new(
                isSuccess: true,
                accessToken: newAccessToken.Value,
                expiresInAccessToken: newAccessToken.ExpiresIn,
                refreshToken: newAccessToken.RefreshToken.Token,
                expiresInRefreshToken: newAccessToken.RefreshToken.ExpiresIn,
                claims: newAccessToken.Claims.Select(claim 
                    => new KeyValuePair<string, string>(claim.Type, claim.Value)
                )
            );
        }
    }
}