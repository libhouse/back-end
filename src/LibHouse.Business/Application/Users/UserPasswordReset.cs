using LibHouse.Business.Application.Shared;
using LibHouse.Business.Application.Users.Gateways;
using LibHouse.Business.Application.Users.Gateways.Outputs;
using LibHouse.Business.Application.Users.Inputs;
using LibHouse.Business.Application.Users.Interfaces;
using LibHouse.Business.Application.Users.Outputs;
using LibHouse.Business.Application.Users.Senders;
using LibHouse.Business.Entities.Users;
using LibHouse.Business.Notifiers;
using System.Threading.Tasks;

namespace LibHouse.Business.Application.Users
{
    public class UserPasswordReset : BaseUseCase, IUserPasswordReset
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserPasswordResetGateway _userPasswordResetGateway;
        private readonly IUserPasswordResetSender _userPasswordResetSender;

        public UserPasswordReset(
            INotifier notifier,
            IUserRepository userRepository,
            IUserPasswordResetGateway userPasswordResetGateway,
            IUserPasswordResetSender userPasswordResetSender) 
            : base(notifier)
        {
            _userRepository = userRepository;
            _userPasswordResetGateway = userPasswordResetGateway;
            _userPasswordResetSender = userPasswordResetSender;
        }

        public async Task<OutputUserPasswordReset> ExecuteAsync(InputUserPasswordReset input)
        {
            Cpf cpf = Cpf.CreateFromDocument(input.UserCpf);
            bool userAccountNotExists = await _userRepository.CheckIfUserCpfIsNotRegisteredAsync(cpf);
            if (userAccountNotExists)
            {
                Notify("Conta do usuário", $"A conta do usuário {input.UserCpf} não foi localizada");
                return new(isSuccess: false, userPasswordResetMessage: $"A conta do usuário {input.UserCpf} não foi localizada");
            }
            User user = await _userRepository.GetUserByCpfAsync(cpf.Value);
            OutputUserPasswordResetGateway outputGateway = await _userPasswordResetGateway.ResetUserPasswordAsync(user.GetEmailAddress());
            if (!outputGateway.IsSuccess)
            {
                Notify("Solicitar redefinição de senha", $"Falha ao solicitar a redefinição de senha do usuário {user.GetEmailAddress()}");
                return new(isSuccess: false, userPasswordResetMessage: outputGateway.UserPasswordResetMessage);
            }
            OutputUserPasswordResetSender outputSender = await _userPasswordResetSender.SendUserPasswordResetRequestAsync(new(user.Id, user.Name, user.GetEmailAddress(), outputGateway.PasswordResetToken));
            if (!outputSender.IsSuccess)
            {
                Notify("Enviar token de redefinição de senha", $"Falha ao enviar o token de redefinição de senha do usuário {user.GetEmailAddress()}");
                return new(isSuccess: false, userPasswordResetMessage: outputSender.SendingMessage);
            }
            return new(isSuccess: true, passwordResetToken: outputGateway.PasswordResetToken);
        }
    }
}