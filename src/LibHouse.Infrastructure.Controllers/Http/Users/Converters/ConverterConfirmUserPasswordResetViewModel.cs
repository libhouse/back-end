using LibHouse.Business.Application.Users.Inputs;
using LibHouse.Infrastructure.Controllers.ViewModels.Users;

namespace LibHouse.Infrastructure.Controllers.Http.Users.Converters
{
    internal static class ConverterConfirmUserPasswordResetViewModel
    {
        internal static InputConfirmUserPasswordReset Convert(this ConfirmUserPasswordResetViewModel viewModel)
        {
            return new InputConfirmUserPasswordReset(viewModel.UserEmail, viewModel.Password, viewModel.PasswordResetToken);
        }
    }
}