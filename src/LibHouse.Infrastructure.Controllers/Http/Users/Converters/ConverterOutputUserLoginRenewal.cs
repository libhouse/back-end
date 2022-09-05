using LibHouse.Business.Application.Users.Outputs;
using LibHouse.Infrastructure.Controllers.Responses.Users;
using System.Linq;

namespace LibHouse.Infrastructure.Controllers.Http.Users.Converters
{
    internal static class ConverterOutputUserLoginRenewal
    {
        internal static UserLoginRenewalResponse Convert(this OutputUserLoginRenewal output)
        {
            return new UserLoginRenewalResponse(
                user: new UserLoginRenewalProfileResponse(
                    id: output.UserId.Value,
                    fullName: output.UserFullName,
                    birthDate: output.UserBirthDate.Value,
                    gender: output.UserGender,
                    email: output.UserEmail,
                    userType: output.UserType
                ),
                accessToken: output.LoginToken,
                expiresInAccessToken: output.ExpiresInLoginToken.Value,
                refreshToken: output.LoginRenewalToken,
                expiresInRefreshToken: output.ExpiresInLoginRenewalToken.Value,
                claims: output.Claims.Select(claim => new UserLoginRenewalClaimResponse(claim.Value, claim.Key))
            );
        }
    }
}