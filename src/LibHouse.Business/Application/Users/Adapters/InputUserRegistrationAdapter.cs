using LibHouse.Business.Application.Users.Inputs;
using LibHouse.Business.Entities.Owners;
using LibHouse.Business.Entities.Residents;
using LibHouse.Business.Entities.Users;
using LibHouse.Business.Extensions;
using System;

namespace LibHouse.Business.Application.Users.Adapters
{
    internal static class InputUserRegistrationAdapter
    {
        internal static User Adapt(this InputUserRegistration input) => 
            input.UserType.GetValueFromDescription<UserType>() switch
            {
                UserType.Resident => AdaptToResident(input),
                UserType.Owner => AdaptToOwner(input),
                _ => throw new InvalidOperationException("Tipo de usuário não encontrado")
            };

        private static Owner AdaptToOwner(InputUserRegistration input) => 
            new(
                input.Name,
                input.LastName, 
                input.BirthDate, 
                input.Gender.GetValueFromDescription<Gender>(), 
                input.Phone,
                input.Email,
                input.CPF
             );

        private static Resident AdaptToResident(InputUserRegistration input) => 
            new(
                input.Name, 
                input.LastName, 
                input.BirthDate,
                input.Gender.GetValueFromDescription<Gender>(), 
                input.Phone, 
                input.Email, 
                input.CPF
            );
    }
}