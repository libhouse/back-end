using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LibHouse.Infrastructure.Authentication.Context.Extensions
{
    public static class AuthenticationContextExtensions
    {
        public static async Task CleanContextDataAsync(this AuthenticationContext authenticationContext)
        {
            await authenticationContext.Database.ExecuteSqlRawAsync("DELETE FROM[Authentication].[AspNetUsers]");
            await authenticationContext.Database.ExecuteSqlRawAsync("DELETE FROM[Authentication].[AspNetUserClaims]");
            await authenticationContext.Database.ExecuteSqlRawAsync("DELETE FROM[Authentication].[AspNetUserRoles]");
            await authenticationContext.Database.ExecuteSqlRawAsync("DELETE FROM[Authentication].[AspNetUserLogins]");
            await authenticationContext.Database.ExecuteSqlRawAsync("DELETE FROM[Authentication].[AspNetRoleClaims]");
            await authenticationContext.Database.ExecuteSqlRawAsync("DELETE FROM[Authentication].[AspNetUserTokens]");
            await authenticationContext.Database.ExecuteSqlRawAsync("DELETE FROM[Authentication].[AspNetRefreshTokens]");
        }
    }
}