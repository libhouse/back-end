using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace LibHouse.Infrastructure.Authentication.Extensions.Identity
{
    internal static class IdentityErrorExtensions
    {
        public static string GetFirstErrorDescription(
            this IEnumerable<IdentityError> identityErrors)
        {
            return identityErrors.Select(e => $"Erro {e.Code}: {e.Description}").FirstOrDefault();
        }
    }
}