using LibHouse.Infrastructure.Controllers.ViewModels.Users;
using Swashbuckle.AspNetCore.Filters;

namespace LibHouse.API.Filters.Swagger.Requests.Users
{
    public class UserPasswordResetViewModelExample : IExamplesProvider<UserPasswordResetViewModel>
    {
        public UserPasswordResetViewModel GetExamples()
        {
            return new()
            {
                Cpf = "37991633012"
            };
        }
    }
}