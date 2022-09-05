using LibHouse.Business.Application.Users.Inputs;
using LibHouse.Infrastructure.Controllers.ViewModels.Users;

namespace LibHouse.Infrastructure.Controllers.Http.Users.Converters
{
    internal static class ConverterConfirmUserRegistrationViewModel
    {
        public static InputConfirmUserRegistration Convert(this ConfirmUserRegistrationViewModel viewModel)
        {
            return new InputConfirmUserRegistration(
                RegistrationToken: viewModel.ConfirmationToken,
                UserEmail: viewModel.UserEmail,
                UserId: viewModel.UserId
            );
        }
    }
}