using LibHouse.Business.Application.Users.Inputs;
using LibHouse.Business.Application.Users.Outputs;
using LibHouse.Business.Monads;
using LibHouse.Infrastructure.Controllers.Http.Users.Converters;
using LibHouse.Infrastructure.Controllers.Http.Users.Interfaces;
using LibHouse.Infrastructure.Controllers.Responses.Users;
using LibHouse.Infrastructure.Controllers.ViewModels.Users;
using System;
using System.Threading.Tasks;

namespace LibHouse.Infrastructure.Controllers.Http.Users.Adapters
{
    public class UsersWebApiAdapter : IHttpUser
    {
        private Func<InputUserRegistration, Task<OutputUserRegistration>> OnUserRegistrationFunction { get; set; }

        public void OnUserRegistration(Func<InputUserRegistration, Task<OutputUserRegistration>> on) => OnUserRegistrationFunction = on;

        public async Task<Result<UserRegistrationResponse>> UserRegistration(UserRegistrationViewModel userRegistrationViewModel)
        {
            InputUserRegistration input = userRegistrationViewModel.Convert();
            OutputUserRegistration output = await OnUserRegistrationFunction(input);
            return output.IsSuccess ? Result.Success(output.Convert()) : Result.Fail<UserRegistrationResponse>(output.RegistrationMessage);
        }

        private Func<InputConfirmUserRegistration, Task<OutputConfirmUserRegistration>> OnConfirmUserRegistrationFunction { get; set; }

        public void OnConfirmUserRegistration(Func<InputConfirmUserRegistration, Task<OutputConfirmUserRegistration>> on) => OnConfirmUserRegistrationFunction = on;

        public async Task<Result> ConfirmUserRegistration(
            ConfirmUserRegistrationViewModel confirmUserRegistrationViewModel)
        {
            InputConfirmUserRegistration input = confirmUserRegistrationViewModel.Convert();
            OutputConfirmUserRegistration output = await OnConfirmUserRegistrationFunction(input);
            return output.IsSuccess ? Result.Success() : Result.Fail(output.ConfirmationMessage);
        }

        private Func<InputUserLogin, Task<OutputUserLogin>> OnUserLoginFunction { get; set; }

        public void OnUserLogin(Func<InputUserLogin, Task<OutputUserLogin>> on) => OnUserLoginFunction = on;

        public async Task<Result<UserLoginResponse>> UserLogin(UserLoginViewModel userLoginViewModel)
        {
            InputUserLogin input = userLoginViewModel.Convert();
            OutputUserLogin output = await OnUserLoginFunction(input);
            return output.IsSuccess ? Result.Success(output.Convert()) : Result.Fail<UserLoginResponse>(output.LoginMessage);
        }

        private Func<InputUserLogout, Task<OutputUserLogout>> OnUserLogoutFunction { get; set; }

        public void OnUserLogout(Func<InputUserLogout, Task<OutputUserLogout>> on) => OnUserLogoutFunction = on;

        public async Task<Result> UserLogout(UserLogoutViewModel userLogoutViewModel)
        {
            InputUserLogout input = userLogoutViewModel.Convert();
            OutputUserLogout output = await OnUserLogoutFunction(input);
            return output.IsSuccess ? Result.Success() : Result.Fail(output.LogoutMessage);
        }

        private Func<InputUserLoginRenewal, Task<OutputUserLoginRenewal>> OnUserLoginRenewalFunction { get; set; }

        public void OnUserLoginRenewal(Func<InputUserLoginRenewal, Task<OutputUserLoginRenewal>> on) => OnUserLoginRenewalFunction = on;

        public async Task<Result<UserLoginRenewalResponse>> UserLoginRenewal(
            UserLoginRenewalViewModel userLoginRenewalViewModel)
        {
            InputUserLoginRenewal input = userLoginRenewalViewModel.Convert();
            OutputUserLoginRenewal output = await OnUserLoginRenewalFunction(input);
            return output.IsSuccess ? Result.Success(output.Convert()) : Result.Fail<UserLoginRenewalResponse>(output.LoginRenewalMessage);
        }

        private Func<InputUserPasswordReset, Task<OutputUserPasswordReset>> OnUserPasswordResetFunction { get; set; }

        public void OnUserPasswordReset(Func<InputUserPasswordReset, Task<OutputUserPasswordReset>> on) => OnUserPasswordResetFunction = on;

        public async Task<Result<UserPasswordResetResponse>> UserPasswordReset(UserPasswordResetViewModel userPasswordResetViewModel)
        {
            InputUserPasswordReset input = userPasswordResetViewModel.Convert();
            OutputUserPasswordReset output = await OnUserPasswordResetFunction(input);
            return output.IsSuccess ? Result.Success(output.Convert()) : Result.Fail<UserPasswordResetResponse>(output.UserPasswordResetMessage);
        }

        private Func<InputConfirmUserPasswordReset, Task<OutputConfirmUserPasswordReset>> OnConfirmUserPasswordResetFunction { get; set; }

        public void OnConfirmUserPasswordReset(Func<InputConfirmUserPasswordReset, Task<OutputConfirmUserPasswordReset>> on) => OnConfirmUserPasswordResetFunction = on;

        public async Task<Result> ConfirmUserPasswordReset(ConfirmUserPasswordResetViewModel confirmUserPasswordResetViewModel)
        {
            InputConfirmUserPasswordReset input = confirmUserPasswordResetViewModel.Convert();
            OutputConfirmUserPasswordReset output = await OnConfirmUserPasswordResetFunction(input);
            return output.IsSuccess ? Result.Success() : Result.Fail(output.ConfirmUserPasswordResetMessage);
        }
    }
}