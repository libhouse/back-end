using LibHouse.Business.Application.Users.Inputs;
using LibHouse.Business.Application.Users.Outputs;
using System;
using System.Threading.Tasks;

namespace LibHouse.Infrastructure.Controllers.Http.Users.Interfaces
{
    public interface IHttpUser
    {
        void OnUserRegistration(Func<InputUserRegistration, Task<OutputUserRegistration>> on);
        void OnConfirmUserRegistration(Func<InputConfirmUserRegistration, Task<OutputConfirmUserRegistration>> on);
        void OnUserLogin(Func<InputUserLogin, Task<OutputUserLogin>> on);
        void OnUserLogout(Func<InputUserLogout, Task<OutputUserLogout>> on);
        void OnUserLoginRenewal(Func<InputUserLoginRenewal, Task<OutputUserLoginRenewal>> on);
        void OnUserPasswordReset(Func<InputUserPasswordReset, Task<OutputUserPasswordReset>> on);
        void OnConfirmUserPasswordReset(Func<InputConfirmUserPasswordReset, Task<OutputConfirmUserPasswordReset>> on);
    }
}