using LibHouse.Business.Application.Users.Inputs;
using LibHouse.Infrastructure.Controllers.ViewModels.Users;

namespace LibHouse.Infrastructure.Controllers.Http.Users.Converters
{
    internal static class ConverterUserLoginViewModel
    {
        internal static InputUserLogin Convert(this UserLoginViewModel viewModel)
        {
            return new InputUserLogin(viewModel.Email, viewModel.Password);
        }
    }
}