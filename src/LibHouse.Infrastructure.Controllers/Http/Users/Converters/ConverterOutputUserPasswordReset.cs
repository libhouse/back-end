using LibHouse.Business.Application.Users.Outputs;
using LibHouse.Infrastructure.Controllers.Responses.Users;

namespace LibHouse.Infrastructure.Controllers.Http.Users.Converters
{
    internal static class ConverterOutputUserPasswordReset
    {
        internal static UserPasswordResetResponse Convert(this OutputUserPasswordReset output)
        {
            return new UserPasswordResetResponse(output.PasswordResetToken);
        }
    }
}