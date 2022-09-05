using LibHouse.Business.Application.Users.Gateways.Inputs;
using LibHouse.Business.Entities.Users;
using LibHouse.Infrastructure.Authentication.Roles;
using Microsoft.AspNetCore.Identity;
using System;

namespace LibHouse.Infrastructure.Authentication.Extensions.Inputs
{
    internal static class InputUserRegistrationGatewayExtensions
    {
        public static IdentityUser AsNewIdentityUser(this InputUserRegistrationGateway input)
        {
            return new()
            {
                EmailConfirmed = false,
                Email = input.Email,
                UserName = input.Email,
                PhoneNumber = input.Phone,
            };
        }

        public static string GetUserTypeRole(this InputUserRegistrationGateway input)
        {
            return input.UserType switch
            {
                UserType.Resident => LibHouseUserRole.Resident,
                UserType.Owner => LibHouseUserRole.Owner,
                _ => throw new NotImplementedException("Tipo de usuário não encontrado"),
            };
        }
    }
}