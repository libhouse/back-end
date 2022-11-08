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
    public class UserLogin : BaseUseCase, IUserLogin
    {
        private readonly IUserLoginGateway _userLoginGateway;
        private readonly IUserRepository _userRepository;

        public UserLogin(
            INotifier notifier,
            IUserLoginGateway userLoginGateway,
            IUserRepository userRepository) 
            : base(notifier)
        {
            _userLoginGateway = userLoginGateway;
            _userRepository = userRepository;
        }

        public async Task<OutputUserLogin> ExecuteAsync(InputUserLogin input)
        {
            OutputUserLoginGateway outputUserLoginGateway = await _userLoginGateway.LoginAsync(input.UserEmail, input.UserPassword);
            if (!outputUserLoginGateway.IsSuccess)
            {
                Notify("Falha na autenticação do usuário", outputUserLoginGateway.LoginMessage);
                return new(isSuccess: false, loginMessage: $"Falha na autenticação do usuário {input.UserEmail}");
            }
            LoggedUser userLoginData = await _userRepository.GetUserLoginDataByEmailAsync(input.UserEmail);
            return outputUserLoginGateway.AdaptUsing(userLoginData);
        }
    }
}