using LibHouse.Business.Application.Users.Gateways;
using LibHouse.Business.Application.Users.Gateways.Outputs;
using LibHouse.Business.Monads;
using LibHouse.Infrastructure.Authentication.Login.Interfaces;
using LibHouse.Infrastructure.Authentication.Token.Services.RefreshTokens;
using System.Threading.Tasks;

namespace LibHouse.Infrastructure.Authentication.Logout
{
    public class IdentityUserLogoutGateway : IUserLogoutGateway
    {
        private readonly ILoggedUser _loggedUser;
        private readonly IRefreshTokenService _refreshTokenService;

        public IdentityUserLogoutGateway(
            ILoggedUser loggedUser, 
            IRefreshTokenService refreshTokenService)
        {
            _loggedUser = loggedUser;
            _refreshTokenService = refreshTokenService;
        }

        public async Task<OutputUserLogoutGateway> LogoutAsync(string userEmail, string userToken)
        {
            if (_loggedUser.GetUserEmail() != userEmail)
            {
                return new(IsForbidden: true, LogoutMessage: $"Operação negada de logout: {userEmail}");
            }
            Result refreshTokenRevoked = await _refreshTokenService.MarkRefreshTokenAsRevokedAsync(userToken);
            if (refreshTokenRevoked.Failure)
            {
                return new(IsSuccess: false, LogoutMessage: $"Falha ao revogar o token do usuário {userEmail}");
            }
            return new(IsSuccess: true);
        }
    }
}