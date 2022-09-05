using LibHouse.Business.Application.Users.Outputs;
using LibHouse.Infrastructure.Controllers.Responses.Users;
using System.Linq;

namespace LibHouse.Infrastructure.Controllers.Http.Users.Converters
{
    internal static class ConverterOutputUserLogin
    {
        internal static UserLoginResponse Convert(this OutputUserLogin output)
        {
            return new UserLoginResponse(
                user: new UserLoginProfileResponse(
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
                claims: output.Claims.Select(claim => new UserLoginClaimResponse(claim.Value, claim.Key))
            );
        }
    }
}