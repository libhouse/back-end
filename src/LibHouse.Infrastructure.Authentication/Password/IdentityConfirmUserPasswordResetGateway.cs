using LibHouse.Business.Application.Users.Gateways;
using LibHouse.Business.Application.Users.Gateways.Inputs;
using LibHouse.Business.Application.Users.Gateways.Outputs;
using LibHouse.Infrastructure.Authentication.Extensions.Identity;
using LibHouse.Infrastructure.Authentication.Token.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace LibHouse.Infrastructure.Authentication.Password
{
    public class IdentityConfirmUserPasswordResetGateway : IConfirmUserPasswordResetGateway
    {
        private readonly UserManager<IdentityUser> _userManager;

        public IdentityConfirmUserPasswordResetGateway(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<OutputConfirmUserPasswordResetGateway> ConfirmUserPasswordResetAsync(
            InputConfirmUserPasswordResetGateway input)
        {
            IdentityUser identityUser = await _userManager.FindByEmailAsync(input.UserEmail);
            if (identityUser is null)
            {
                return new(IsSuccess: false, $"O usuário {input.UserEmail} não foi encontrado");
            }
            PasswordResetToken passwordResetToken = new(input.PasswordResetToken, isEncoded: false);
            IdentityResult passwordResetResult = await _userManager.ResetPasswordAsync(identityUser, passwordResetToken.Value, input.NewPassword);
            return passwordResetResult.Succeeded ? new(IsSuccess: true) : new(IsSuccess: false, passwordResetResult.Errors.GetFirstErrorDescription());
        }
    }
}