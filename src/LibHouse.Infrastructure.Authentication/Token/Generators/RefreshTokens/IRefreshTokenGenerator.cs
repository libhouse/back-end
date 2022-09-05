using LibHouse.Infrastructure.Authentication.Token.Models;
using System.Threading.Tasks;

namespace LibHouse.Infrastructure.Authentication.Token.Generators.RefreshTokens
{
    public interface IRefreshTokenGenerator
    {
        Task<RefreshToken> GenerateRefreshTokenAsync(string userEmail, string accessTokenId);
    }
}