using LibHouse.Business.Application.Users.Inputs;
using LibHouse.Infrastructure.Controllers.ViewModels.Users;

namespace LibHouse.Infrastructure.Controllers.Http.Users.Converters
{
    internal static class ConverterUserLogoutViewModel
    {
        internal static InputUserLogout Convert(this UserLogoutViewModel viewModel)
        {
            return new InputUserLogout(viewModel.Email, viewModel.RefreshToken);
        }
    }
}