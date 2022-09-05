using LibHouse.Infrastructure.Authentication.Extensions.Claims;
using LibHouse.Infrastructure.Authentication.Login.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace LibHouse.API.Authentication
{
    internal class AspNetLoggedUser : ILoggedUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AspNetLoggedUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Claim GetClaimByType(string type)
        {
            return GetClaimsIdentity().FirstOrDefault(c => c.Type == type);
        }

        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return _httpContextAccessor.HttpContext.User.Claims;
        }

        public string GetUserEmail()
        {
            return IsAuthenticated() ? _httpContextAccessor.HttpContext.User.GetUserEmail() : string.Empty;
        }

        public Guid GetUserId()
        {
            return IsAuthenticated() ? Guid.Parse(_httpContextAccessor.HttpContext.User.GetUserId()) : Guid.Empty;
        }

        public bool IsAuthenticated()
        {
            return _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public bool IsInRole(string role)
        {
            return _httpContextAccessor.HttpContext.User.IsInRole(role);
        }
    }
}