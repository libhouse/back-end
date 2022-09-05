using FluentValidation;
using LibHouse.Business.Entities.Users;

namespace LibHouse.Business.Validations.Users
{
    public class UserRegistrationValidator : AbstractValidator<User> 
    {
        public UserRegistrationValidator(IUserRepository userRepository)
        {
            Include(new UserCpfValidation(userRepository));
            Include(new UserEmailValidation(userRepository));
        }
    }
}