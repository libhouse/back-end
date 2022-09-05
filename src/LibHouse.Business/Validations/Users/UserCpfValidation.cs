using FluentValidation;
using LibHouse.Business.Entities.Users;

namespace LibHouse.Business.Validations.Users
{
    internal class UserCpfValidation : AbstractValidator<User>
    {
        public UserCpfValidation(IUserRepository userRepository)
        {
            RuleFor(u => u.CPF).MustAsync(
                async (cpf, cancellationToken) => await userRepository.CheckIfUserCpfIsNotRegisteredAsync(cpf)
            ).WithMessage("O CPF já foi registrado.");
        }
    }
}