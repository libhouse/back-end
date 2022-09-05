using Ardalis.GuardClauses;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace LibHouse.Infrastructure.Authentication.Extensions.Claims
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            Guard.Against.Null(claimsPrincipal, nameof(claimsPrincipal), "Claims do usuário não encontradas");

            var claim = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier);

            return claim?.Value;
        }

        public static string GetUserEmail(this ClaimsPrincipal claimsPrincipal)
        {
            Guard.Against.Null(claimsPrincipal, nameof(claimsPrincipal), "Claims do usuário não encontradas");

            var claim = claimsPrincipal.FindFirst(ClaimTypes.Email);

            return claim?.Value;
        }

        public static string GetUserName(this ClaimsPrincipal claimsPrincipal)
        {
            Guard.Against.Null(claimsPrincipal, nameof(claimsPrincipal), "Claims do usuário não encontradas");

            var claim = claimsPrincipal.FindFirst(ClaimTypes.Name);

            return claim?.Value;
        }

        public static bool CheckIfUserHasOneOfTheseRoles(
            this ClaimsPrincipal claimsPrincipal, 
            IList<string> roles)
        {
            Guard.Against.Null(claimsPrincipal, nameof(claimsPrincipal), "Claims do usuário não encontradas");

            var claims = claimsPrincipal.FindAll(ClaimTypes.Role);

            if (!claims.Any()) return false;

            return claims.Select(c => c.Value).Any(r => roles.Contains(r));
        }
    }
}