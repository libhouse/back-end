using LibHouse.Business.Application.Shared;
using LibHouse.Business.Application.Users.Gateways;
using LibHouse.Business.Application.Users.Gateways.Outputs;
using LibHouse.Business.Application.Users.Inputs;
using LibHouse.Business.Application.Users.Interfaces;
using LibHouse.Business.Application.Users.Outputs;
using LibHouse.Business.Notifiers;
using System.Threading.Tasks;

namespace LibHouse.Business.Application.Users
{
    public class UserLogout : BaseUseCase, IUserLogout
    {
        private readonly IUserLogoutGateway _userLogoutGateway;

        public UserLogout(
            INotifier notifier,
            IUserLogoutGateway userLogoutGateway) 
            : base(notifier)
        {
            _userLogoutGateway = userLogoutGateway;
        }

        public async Task<OutputUserLogout> ExecuteAsync(InputUserLogout input)
        {
            OutputUserLogoutGateway outputGateway = await _userLogoutGateway.LogoutAsync(input.UserEmail, input.UserToken);

            if (!outputGateway.IsSuccess)
            {
                Notify("Falha no logout do usuário", $"O usuário {input.UserEmail} não realizou logout");

                return new(IsSuccess: false, LogoutMessage: outputGateway.LogoutMessage);
            }

            return new(IsSuccess: true);
        }
    }
}