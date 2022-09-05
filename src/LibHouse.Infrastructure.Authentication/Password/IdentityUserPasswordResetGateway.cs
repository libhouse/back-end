using Ardalis.GuardClauses;
using LibHouse.Business.Application.Users.Gateways;
using LibHouse.Business.Application.Users.Gateways.Outputs;
using LibHouse.Infrastructure.Authentication.Token.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace LibHouse.Infrastructure.Authentication.Password
{
    public class IdentityUserPasswordResetGateway : IUserPasswordResetGateway
    {
        private readonly UserManager<IdentityUser> _userManager;

        public IdentityUserPasswordResetGateway(
            UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<OutputUserPasswordResetGateway> ResetUserPasswordAsync(string userEmail)
        {
            Guard.Against.NullOrEmpty(userEmail, nameof(userEmail), "O valor do e-mail é obrigatório");

            IdentityUser user = await _userManager.FindByEmailAsync(userEmail);

            if (user is null)
            {
                return new(isSuccess: false, userPasswordResetMessage: $"O usuário {userEmail} não foi encontrado");
            }

            string passwordResetTokenValue = await _userManager.GeneratePasswordResetTokenAsync(user);

            PasswordResetToken passwordResetToken = new(passwordResetTokenValue);

            return new(isSuccess: true, passwordResetToken: passwordResetToken.Value);
        }
    }
}