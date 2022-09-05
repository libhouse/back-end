using LibHouse.Business.Application.Users.Gateways.Outputs;
using LibHouse.Business.Application.Users.Outputs;
using LibHouse.Business.Application.Users.Projections;
using LibHouse.Business.Extensions;

namespace LibHouse.Business.Application.Users.Adapters
{
    internal static class OutputUserLoginRenewalGatewayAdapter
    {
        internal static OutputUserLoginRenewal AdaptUsing(
            this OutputUserLoginRenewalGateway output, 
            LoggedUser userLoginData) =>
            new(
                isSuccess: true,
                userId: userLoginData.UserId,
                userFullName: string.Concat(userLoginData.UserFirstName, " ", userLoginData.UserLastName),
                userBirthDate: userLoginData.UserBirthDate,
                userGender: userLoginData.UserGender.GetDescription(),
                userEmail: userLoginData.UserEmail,
                userType: userLoginData.UserType.GetDescription(),
                loginToken: output.AccessToken,
                expiresInLoginToken: output.ExpiresInAccessToken,
                loginRenewalToken: output.RefreshToken,
                expiresInLoginRenewalToken: output.ExpiresInRefreshToken,
                claims: output.Claims
            );
    }
}