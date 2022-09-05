using LibHouse.Business.Monads;
using LibHouse.Infrastructure.Authentication.Extensions.Common;
using LibHouse.Infrastructure.Authentication.Token.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace LibHouse.Infrastructure.Authentication.Token.Validations.RefreshTokens
{
    public class RefreshTokenValidator : IRefreshTokenValidator
    {
        public Result CheckIfRefreshTokenCanBeUsedWithAccessToken(
            RefreshToken refreshToken,
            SecurityToken accessToken,
            ClaimsPrincipal accessTokenClaims)
        {
            try
            {
                Result tokenFormat = CheckAccessTokenFormat(accessToken);

                Result tokenEncryption = CheckAccessTokenEncryption(accessToken);

                Result tokenExpiration = CheckAccessTokenHasBeenExpired(accessTokenClaims);

                Result tokenNotUsedAndRevoked = CheckRefreshTokenNotUsedAndNotRevoked(refreshToken);

                Result tokenMatch = CheckRefreshTokenMatchesAccessToken(refreshToken, accessTokenClaims);

                Result tokenIsActive = CheckRefreshTokenHasNotBeenExpired(refreshToken);

                return Result.Combine(tokenFormat, tokenEncryption, tokenExpiration, tokenNotUsedAndRevoked, tokenMatch, tokenIsActive);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        private static Result CheckRefreshTokenHasNotBeenExpired(RefreshToken refreshToken)
        {
            if (refreshToken.ExpiresIn < DateTime.UtcNow)
            {
                return Result.Fail("O refresh token expirou");
            }

            return Result.Success();
        }

        private static Result CheckRefreshTokenMatchesAccessToken(
            RefreshToken refreshToken, 
            ClaimsPrincipal validatedTokenClaims)
        {
            string accessTokenId = validatedTokenClaims.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

            if (refreshToken.JwtId != accessTokenId)
            {
                return Result.Fail("O access token não está atrelado ao refresh token");
            }

            return Result.Success();
        }

        private static Result CheckRefreshTokenNotUsedAndNotRevoked(RefreshToken refreshToken)
        {
            if (refreshToken.IsUsed || refreshToken.IsRevoked)
            {
                return Result.Fail("O refresh token não pode ser mais utilizado");
            }

            return Result.Success();
        }

        private static Result CheckAccessTokenHasBeenExpired(ClaimsPrincipal validatedTokenClaims)
        {
            long utcExpiryDate = long.Parse(validatedTokenClaims.Claims.FirstOrDefault(v => v.Type == JwtRegisteredClaimNames.Exp).Value);

            DateTime expiryDate = utcExpiryDate.UnixTimeStampToDateTime();

            if (expiryDate > DateTime.UtcNow)
            {
                return Result.Fail("O token ainda não expirou");
            }

            return Result.Success();
        }

        private static Result CheckAccessTokenEncryption(SecurityToken validatedToken)
        {
            JwtSecurityToken jwtSecurityToken = validatedToken as JwtSecurityToken;

            bool isHmacSha256Encryption = jwtSecurityToken.Header.Alg
                .Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);

            return isHmacSha256Encryption
                ? Result.Success()
                : Result.Fail("O token informado não está encriptado");
        }

        private static Result CheckAccessTokenFormat(SecurityToken validatedToken)
        {
            return validatedToken is JwtSecurityToken
                ? Result.Success()
                : Result.Fail("O token informado não está no formato esperado");
        }
    }
}