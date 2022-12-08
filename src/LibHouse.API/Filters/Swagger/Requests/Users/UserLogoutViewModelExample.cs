using LibHouse.Infrastructure.Controllers.ViewModels.Users;
using Swashbuckle.AspNetCore.Filters;

namespace LibHouse.API.Filters.Swagger.Requests.Users
{
    public class UserLogoutViewModelExample : IExamplesProvider<UserLogoutViewModel>
    {
        public UserLogoutViewModel GetExamples()
        {
            return new()
            {
                Email = "lucas.dirani@gmail.com",
                RefreshToken = "29-B9-C5-C2-18-26-E1-E5-33-37-0C-A2-35-06-B3-3B-1F-27-54-D3-86-C1-73-2B"
            };
        }
    }
}