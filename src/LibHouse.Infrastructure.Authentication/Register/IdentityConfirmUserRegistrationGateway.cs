using LibHouse.Business.Application.Users.Gateways;
using LibHouse.Business.Application.Users.Gateways.Inputs;
using LibHouse.Business.Application.Users.Gateways.Outputs;
using LibHouse.Infrastructure.Authentication.Extensions.Identity;
using LibHouse.Infrastructure.Authentication.Token.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace LibHouse.Infrastructure.Authentication.Register
{
    public class IdentityConfirmUserRegistrationGateway : IConfirmUserRegistrationGateway
    {
        private readonly UserManager<IdentityUser> _userManager;

        public IdentityConfirmUserRegistrationGateway(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<OutputConfirmUserRegistrationGateway> ConfirmUserRegistrationAsync(
            InputConfirmUserRegistrationGateway input)
        {
            IdentityUser user = await _userManager.FindByEmailAsync(input.UserEmail);

            if (user is null)
            {
                return new(ConfirmationMessage: "O endereço de e-mail do usuário não foi localizado.");
            }

            SignUpConfirmationToken confirmationToken = new(input.RegistrationToken, isEncoded: false);

            IdentityResult userEmailConfirmed = await _userManager.ConfirmEmailAsync(user, confirmationToken.Value);

            return userEmailConfirmed.Succeeded
                ? new(IsSuccess: true)
                : new(ConfirmationMessage: userEmailConfirmed.Errors.GetFirstErrorDescription());
        }
    }
}