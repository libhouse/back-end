using LibHouse.Infrastructure.Controllers.ViewModels.Users;
using Swashbuckle.AspNetCore.Filters;

namespace LibHouse.API.Filters.Swagger.Requests.Users
{
    public class UserLoginRenewalViewModelExample : IExamplesProvider<UserLoginRenewalViewModel>
    {
        public UserLoginRenewalViewModel GetExamples()
        {
            return new()
            {
                Email = "lucas.dirani@gmail.com",
                RefreshToken = "29-B9-C5-C2-18-26-E1-E5-33-37-0C-A2-35-06-B3-3B-1F-27-54-D3-86-C1-73-2B",
                AccessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJlMTY1NzdlYy04NTg1LTQ1MDYtYjNlOC05NzlhOWQ1MGNiZjkiLCJlbWFpbCI6Imx1Y2FzLmRpcmFuaUBnbWFpbC5jb20iLCJqdGkiOiI4MWFmOTQ0YS1iYzU0LTQxZmMtODRkNy0wMzJhZTNkZmUyOWYiLCJuYmYiOjE2NzA1MjM4MzQsImlhdCI6MTY3MDUyMzgzNCwicm9sZSI6WyJSZXNpZGVudCIsIlVzZXIiXSwiZXhwIjoxNjcwNTI0NDMzLCJpc3MiOiJMaWJIb3VzZUFQSSIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0In0.nwhGmhekRyhZwJIbJzBLf1Q6KimTLZJJE1hOJBoHRKM"
            };
        }
    }
}