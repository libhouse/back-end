using LibHouse.Business.Application.Users.Gateways;
using LibHouse.Business.Application.Users.Gateways.Inputs;
using LibHouse.Business.Application.Users.Gateways.Outputs;
using LibHouse.Infrastructure.Authentication.Extensions.Identity;
using LibHouse.Infrastructure.Authentication.Extensions.Inputs;
using LibHouse.Infrastructure.Authentication.Roles;
using LibHouse.Infrastructure.Authentication.Token.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace LibHouse.Infrastructure.Authentication.Register
{
    public class IdentityUserRegistrationGateway : IUserRegistrationGateway
    {
        private readonly UserManager<IdentityUser> _userManager;

        public IdentityUserRegistrationGateway(
            UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<OutputUserRegistrationGateway> RegisterUserAsync(InputUserRegistrationGateway input)
        {
            IdentityUser identityUser = input.AsNewIdentityUser();
            IdentityResult userCreation = await _userManager.CreateAsync(identityUser, input.Password);
            if (!userCreation.Succeeded)
            {
                string userCreationError = userCreation.Errors.GetFirstErrorDescription();
                return new(IsSuccess: false, RegistrationMessage: userCreationError);
            }
            await _userManager.AddToRolesAsync(identityUser, new[] { LibHouseUserRole.User, input.GetUserTypeRole() });
            string token = await _userManager.GenerateEmailConfirmationTokenAsync(identityUser);
            var signUpConfirmationToken = new SignUpConfirmationToken(token, true);
            return new(IsSuccess: true, RegistrationToken: signUpConfirmationToken.EncodedValue);
        }
    }
}