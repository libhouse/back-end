using System;

namespace LibHouse.Business.Application.Users.Inputs
{
    public record InputConfirmUserRegistration(string RegistrationToken, string UserEmail, Guid UserId);
}