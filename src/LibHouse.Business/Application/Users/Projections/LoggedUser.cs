using LibHouse.Business.Entities.Users;
using System;

namespace LibHouse.Business.Application.Users.Projections
{
    public record LoggedUser(
        Guid UserId, 
        string UserFirstName, 
        string UserLastName, 
        DateTime UserBirthDate, 
        Gender UserGender, 
        string UserEmail, 
        UserType UserType
    );
}