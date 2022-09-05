using LibHouse.Business.Application.Users.Outputs;
using LibHouse.Infrastructure.Controllers.Responses.Users;

namespace LibHouse.Infrastructure.Controllers.Http.Users.Converters
{
    internal static class ConverterOutputUserRegistration
    {
        internal static UserRegistrationResponse Convert(this OutputUserRegistration output)
        {
            return new UserRegistrationResponse(output.RegistrationToken);
        }
    }
}