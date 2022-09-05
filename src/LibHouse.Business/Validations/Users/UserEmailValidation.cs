using FluentValidation;
using LibHouse.Business.Entities.Users;

namespace LibHouse.Business.Validations.Users
{
    internal class UserEmailValidation : AbstractValidator<User>
    {
        public UserEmailValidation(IUserRepository userRepository)
        {
            RuleFor(u => u.Email).MustAsync(
                async (email, cancellationToken) => await userRepository.CheckIfUserEmailIsNotRegisteredAsync(email.Value)
            ).WithMessage("O endereço de e-mail já foi registrado.");
        }
    }
}