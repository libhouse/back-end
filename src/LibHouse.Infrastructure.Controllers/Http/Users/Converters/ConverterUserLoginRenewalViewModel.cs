using LibHouse.Business.Application.Users.Inputs;
using LibHouse.Infrastructure.Controllers.ViewModels.Users;

namespace LibHouse.Infrastructure.Controllers.Http.Users.Converters
{
    internal static class ConverterUserLoginRenewalViewModel
    {
        internal static InputUserLoginRenewal Convert(this UserLoginRenewalViewModel viewModel)
        {
            return new InputUserLoginRenewal(viewModel.Email, viewModel.AccessToken, viewModel.RefreshToken);
        }
    }
}