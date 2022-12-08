using LibHouse.Infrastructure.Controllers.ViewModels.Users;
using Swashbuckle.AspNetCore.Filters;

namespace LibHouse.API.Filters.Swagger.Requests.Users
{
    public class UserLoginViewModelExample : IExamplesProvider<UserLoginViewModel>
    {
        public UserLoginViewModel GetExamples()
        {
            return new()
            {
                Email = "lucas.dirani@gmail.com",
                Password = "Senh@123456"
            };
        }
    }
}