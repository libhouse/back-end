using LibHouse.Business.Application.Shared;
using LibHouse.Business.Application.Users.Adapters;
using LibHouse.Business.Application.Users.Gateways;
using LibHouse.Business.Application.Users.Gateways.Outputs;
using LibHouse.Business.Application.Users.Inputs;
using LibHouse.Business.Application.Users.Interfaces;
using LibHouse.Business.Application.Users.Outputs;
using LibHouse.Business.Application.Users.Projections;
using LibHouse.Business.Entities.Users;
using LibHouse.Business.Notifiers;
using System.Threading.Tasks;

namespace LibHouse.Business.Application.Users
{
    public class UserLoginRenewal : BaseUseCase, IUserLoginRenewal
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserLoginRenewalGateway _userLoginRenewalGateway;

        public UserLoginRenewal(
            INotifier notifier,
            IUserRepository userRepository,
            IUserLoginRenewalGateway userLoginRenewalGateway) 
            : base(notifier)
        {
            _userLoginRenewalGateway = userLoginRenewalGateway;
            _userRepository = userRepository;
        }

        public async Task<OutputUserLoginRenewal> ExecuteAsync(InputUserLoginRenewal input)
        {
            OutputUserLoginRenewalGateway outputUserLoginRenewalGateway = await _userLoginRenewalGateway.RenewLoginAsync(input.UserEmail, input.UserLoginToken, input.UserLoginRenewalToken);
            if (!outputUserLoginRenewalGateway.IsSuccess)
            {
                Notify("Falha ao renovar o login do usuário", outputUserLoginRenewalGateway.LoginRenewalMessage);
                return new(isSuccess: false, loginRenewalMessage: $"Falha ao renovar o login do usuário {input.UserEmail}: {outputUserLoginRenewalGateway.LoginRenewalMessage}");
            }
            LoggedUser userLoginData = await _userRepository.GetUserLoginDataByEmailAsync(input.UserEmail);
            return outputUserLoginRenewalGateway.AdaptUsing(userLoginData);
        }
    }
}