using LibHouse.Business.Application.Users.Inputs;
using LibHouse.Infrastructure.Controllers.ViewModels.Users;

namespace LibHouse.Infrastructure.Controllers.Http.Users.Converters
{
    internal static class ConverterUserPasswordResetViewModel
    {
        internal static InputUserPasswordReset Convert(this UserPasswordResetViewModel viewModel)
        {
            return new InputUserPasswordReset(viewModel.Cpf);
        }
    }
}