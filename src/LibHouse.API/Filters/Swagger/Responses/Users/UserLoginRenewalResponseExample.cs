using LibHouse.Infrastructure.Controllers.Responses.Users;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;

namespace LibHouse.API.Filters.Swagger.Responses.Users
{
    public class UserLoginRenewalResponseExample : IExamplesProvider<UserLoginRenewalResponse>
    {
        public UserLoginRenewalResponse GetExamples()
        {
            return new(
                user: new(
                    id: Guid.NewGuid(),
                    fullName: "Lucas Dirani",
                    birthDate: new(1998, 8, 12),
                    gender: "Male",
                    email: "lucas.dirani@gmail.com",
                    userType: "Resident"),
                accessToken: "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJlMTY1NzdlYy04NTg1LTQ1MDYtYjNlOC05NzlhOWQ1MGNiZjkiLCJlbWFpbCI6Imx1Y2FzLmRpcmFuaUBnbWFpbC5jb20iLCJqdGkiOiI4MWFmOTQ0YS1iYzU0LTQxZmMtODRkNy0wMzJhZTNkZmUyOWYiLCJuYmYiOjE2NzA1MjM4MzQsImlhdCI6MTY3MDUyMzgzNCwicm9sZSI6WyJSZXNpZGVudCIsIlVzZXIiXSwiZXhwIjoxNjcwNTI0NDMzLCJpc3MiOiJMaWJIb3VzZUFQSSIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0In0.nwhGmhekRyhZwJIbJzBLf1Q6KimTLZJJE1hOJBoHRKM",
                expiresInAccessToken: DateTime.UtcNow.AddMinutes(10),
                refreshToken: "29-B9-C5-C2-18-26-E1-E5-33-37-0C-A2-35-06-B3-3B-1F-27-54-D3-86-C1-73-2B",
                expiresInRefreshToken: DateTime.UtcNow.AddMonths(3),
                claims: new List<UserLoginRenewalClaimResponse>()
                {
                    new(value: "User", type: "role"),
                    new(value: "Resident", type: "role"),
                    new(value: "1670523834", type: "iat"),
                    new(value: "1670523834", type: "nbf"),
                    new(value: "81af944a-bc54-41fc-84d7-032ae3dfe29f", type: "jti"),
                    new(value: "lucas.dirani@gmail.com", type: "email"),
                    new(value: "e16577ec-8585-4506-b3e8-979a9d50cbf9", type: "sub"),
                }
            );
        }
    }
}