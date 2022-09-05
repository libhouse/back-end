using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace LibHouse.Infrastructure.Authentication.Login.Interfaces
{
    public interface ILoggedUser
    {
        Guid GetUserId();
        string GetUserEmail();
        bool IsAuthenticated();
        bool IsInRole(string role);
        IEnumerable<Claim> GetClaimsIdentity();
        Claim GetClaimByType(string type);
    }
}