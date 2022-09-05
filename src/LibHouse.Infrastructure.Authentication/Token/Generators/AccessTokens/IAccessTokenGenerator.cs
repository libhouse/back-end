using LibHouse.Infrastructure.Authentication.Token.Models;
using System.Threading.Tasks;

namespace LibHouse.Infrastructure.Authentication.Token.Generators.AccessTokens
{
    public interface IAccessTokenGenerator
    {
        Task<AccessToken> GenerateAccessTokenAsync(string userEmail);
    }
}