using LibHouse.Business.Application.Users.Inputs;
using LibHouse.Business.Extensions;
using LibHouse.Infrastructure.Controllers.ViewModels.Users;

namespace LibHouse.Infrastructure.Controllers.Http.Users.Converters
{
    internal static class ConverterUserRegistrationViewModel
    {
        public static InputUserRegistration Convert(this UserRegistrationViewModel viewModel)
        {
            return new InputUserRegistration(
                name: viewModel.Name,
                lastName: viewModel.LastName,
                birthDate: viewModel.BirthDate,
                gender: viewModel.Gender.GetDescription(),
                phone: viewModel.Phone,
                email: viewModel.Email,
                cpf: viewModel.Cpf,
                userType: viewModel.UserType.GetDescription(),
                password: viewModel.Password
            );
        }
    }
}