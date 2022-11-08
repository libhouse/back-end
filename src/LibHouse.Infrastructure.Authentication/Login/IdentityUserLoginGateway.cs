using LibHouse.Business.Application.Users.Gateways;
using LibHouse.Business.Application.Users.Gateways.Outputs;
using LibHouse.Infrastructure.Authentication.Token.Generators.AccessTokens;
using LibHouse.Infrastructure.Authentication.Token.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibHouse.Infrastructure.Authentication.Login
{
    public class IdentityUserLoginGateway : IUserLoginGateway
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IAccessTokenGenerator _accessTokenGenerator;

        public IdentityUserLoginGateway(
            SignInManager<IdentityUser> signInManager,
            IAccessTokenGenerator accessTokenGenerator)
        {
            _signInManager = signInManager;
            _accessTokenGenerator = accessTokenGenerator;
        }

        public async Task<OutputUserLoginGateway> LoginAsync(string userName, string userPassword)
        {
            SignInResult loginResult = await _signInManager.PasswordSignInAsync(userName, userPassword, isPersistent: true, lockoutOnFailure: true);
            if (loginResult.IsLockedOut)
            {
                return new(loginMessage: "O usuário está bloqueado.");
            }
            if (!loginResult.Succeeded)
            {
                return new(loginMessage: "As credenciais são inválidas.");
            }
            AccessToken accessToken = await _accessTokenGenerator.GenerateAccessTokenAsync(userName);
            return new(
                isSuccess: true,
                accessToken: accessToken.Value, 
                expiresInAccessToken: accessToken.ExpiresIn,
                refreshToken: accessToken.RefreshToken.Token,
                expiresInRefreshToken: accessToken.RefreshToken.ExpiresIn,
                claims: accessToken.Claims.Select(claim => 
                    new KeyValuePair<string, string>(claim.Type, claim.Value)
                )
            );
        }
    }
}