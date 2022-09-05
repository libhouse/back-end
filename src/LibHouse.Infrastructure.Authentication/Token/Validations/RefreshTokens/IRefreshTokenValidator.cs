using LibHouse.Business.Monads;
using LibHouse.Infrastructure.Authentication.Token.Models;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace LibHouse.Infrastructure.Authentication.Token.Validations.RefreshTokens
{
    public interface IRefreshTokenValidator
    {
        Result CheckIfRefreshTokenCanBeUsedWithAccessToken(
            RefreshToken refreshToken, 
            SecurityToken accessToken, 
            ClaimsPrincipal accessTokenClaims);
    }
}