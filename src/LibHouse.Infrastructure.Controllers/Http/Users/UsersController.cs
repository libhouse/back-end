using LibHouse.Business.Application.Users.Interfaces;
using LibHouse.Infrastructure.Controllers.Http.Users.Interfaces;

namespace LibHouse.Infrastructure.Controllers.Http.Users
{
    public class UsersController
    {
        public UsersController(
            IHttpUser httpUser, 
            IUserRegistration userRegistration,
            IConfirmUserRegistration confirmUserRegistration,
            IUserLogin userLogin,
            IUserLogout userLogout,
            IUserLoginRenewal userLoginRenewal,
            IUserPasswordReset userPasswordReset,
            IConfirmUserPasswordReset confirmUserPasswordReset)
        {
            httpUser.OnUserRegistration(
                async (input) =>
                {
                    return await userRegistration.ExecuteAsync(input);
                }
            );

            httpUser.OnConfirmUserRegistration(
                async (input) =>
                {
                    return await confirmUserRegistration.ExecuteAsync(input);
                }
            );

            httpUser.OnUserLogin(
                async (input) =>
                {
                    return await userLogin.ExecuteAsync(input);
                }
            );

            httpUser.OnUserLogout(
                async (input) =>
                {
                    return await userLogout.ExecuteAsync(input);
                }
            );

            httpUser.OnUserLoginRenewal(
                async (input) =>
                {
                    return await userLoginRenewal.ExecuteAsync(input);
                }
            );

            httpUser.OnUserPasswordReset(
                async (input) =>
                {
                    return await userPasswordReset.ExecuteAsync(input);
                }
            );

            httpUser.OnConfirmUserPasswordReset(
                async (input) =>
                {
                    return await confirmUserPasswordReset.ExecuteAsync(input);
                }
            );
        }
    }
}