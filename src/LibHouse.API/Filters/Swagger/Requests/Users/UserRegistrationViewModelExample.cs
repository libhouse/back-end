using LibHouse.Business.Entities.Users;
using LibHouse.Infrastructure.Controllers.ViewModels.Users;
using Swashbuckle.AspNetCore.Filters;
using System;

namespace LibHouse.API.Filters.Swagger.Requests.Users
{
    public class UserRegistrationViewModelExample : IExamplesProvider<UserRegistrationViewModel>
    {
        public UserRegistrationViewModel GetExamples()
        {
            return new()
            {
                BirthDate = new DateTime(1998, 8, 12),
                ConfirmPassword = "Senh@123456",
                Cpf = "37991633012",
                Email = "lucas.dirani@gmail.com",
                Gender = Gender.Male,
                LastName = "Dirani",
                Name = "Lucas",
                Password = "Senh@123456",
                Phone = "11918314320",
                UserType = UserType.Resident
            };
        }
    }
}