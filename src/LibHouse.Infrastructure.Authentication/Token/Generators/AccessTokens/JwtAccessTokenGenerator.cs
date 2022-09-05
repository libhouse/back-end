using LibHouse.Infrastructure.Authentication.Extensions.Common;
using LibHouse.Infrastructure.Authentication.Token.Generators.RefreshTokens;
using LibHouse.Infrastructure.Authentication.Token.Models;
using LibHouse.Infrastructure.Authentication.Token.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LibHouse.Infrastructure.Authentication.Token.Generators.AccessTokens
{
    public class JwtAccessTokenGenerator : IAccessTokenGenerator
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AccessTokenSettings _tokenSettings;
        private readonly IRefreshTokenGenerator _refreshTokenGenerator;

        public JwtAccessTokenGenerator(
            UserManager<IdentityUser> userManager,
            IOptions<AccessTokenSettings> tokenSettings,
            IRefreshTokenGenerator refreshTokenGenerator)
        {
            _userManager = userManager;
            _tokenSettings = tokenSettings.Value;
            _refreshTokenGenerator = refreshTokenGenerator;
        }

        public async Task<AccessToken> GenerateAccessTokenAsync(string userEmail)
        {
            IdentityUser user = await _userManager.FindByEmailAsync(userEmail);

            ClaimsIdentity identityClaims = await GenerateIdentityClaimsAsync(user);

            (SecurityToken securityToken, string accessToken) = GenerateAccessToken(identityClaims);

            return new AccessToken(
                id: securityToken.Id,
                value: accessToken,
                expiresIn: DateTime.UtcNow.AddSeconds(_tokenSettings.ExpiresInSeconds),
                claims: identityClaims.Claims.Select(c => new AccessTokenClaim(c.Value, c.Type)),
                refreshToken: await _refreshTokenGenerator.GenerateRefreshTokenAsync(userEmail, securityToken.Id)                
            );
        }

        private (SecurityToken securityToken, string accessToken) GenerateAccessToken(ClaimsIdentity identityClaims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            byte[] key = Encoding.ASCII.GetBytes(_tokenSettings.Secret);

            var symmetricKey = new SymmetricSecurityKey(key);

            var signingCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256Signature);

            SecurityToken token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenSettings.Issuer,
                Audience = _tokenSettings.ValidIn,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddSeconds(_tokenSettings.ExpiresInSeconds),
                SigningCredentials = signingCredentials,
            });

            return (token, tokenHandler.WriteToken(token));
        }

        private async Task<ClaimsIdentity> GenerateIdentityClaimsAsync(IdentityUser identityUser)
        {
            var claims = await _userManager.GetClaimsAsync(identityUser);

            var userRoles = await _userManager.GetRolesAsync(identityUser);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, identityUser.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, identityUser.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, DateTime.UtcNow.ToUnixEpochDate().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToUnixEpochDate().ToString(), ClaimValueTypes.Integer64));

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(type: "role", userRole));
            }

            var identityClaims = new ClaimsIdentity();

            identityClaims.AddClaims(claims);

            return identityClaims;
        }
    }
}