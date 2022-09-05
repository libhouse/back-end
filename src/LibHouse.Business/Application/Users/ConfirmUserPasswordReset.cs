using LibHouse.Business.Application.Shared;
using LibHouse.Business.Application.Users.Gateways;
using LibHouse.Business.Application.Users.Gateways.Inputs;
using LibHouse.Business.Application.Users.Gateways.Outputs;
using LibHouse.Business.Application.Users.Inputs;
using LibHouse.Business.Application.Users.Interfaces;
using LibHouse.Business.Application.Users.Outputs;
using LibHouse.Business.Notifiers;
using System.Threading.Tasks;

namespace LibHouse.Business.Application.Users
{
    public class ConfirmUserPasswordReset : BaseUseCase, IConfirmUserPasswordReset
    {
        private readonly IConfirmUserPasswordResetGateway _confirmUserPasswordResetGateway;

        public ConfirmUserPasswordReset(
            INotifier notifier,
            IConfirmUserPasswordResetGateway confirmUserPasswordResetGateway) 
            : base(notifier)
        {
            _confirmUserPasswordResetGateway = confirmUserPasswordResetGateway;
        }

        public async Task<OutputConfirmUserPasswordReset> ExecuteAsync(InputConfirmUserPasswordReset input)
        {
            InputConfirmUserPasswordResetGateway inputGateway = new(input.UserEmail, input.NewPassword, input.PasswordResetToken);

            OutputConfirmUserPasswordResetGateway outputGateway = await _confirmUserPasswordResetGateway.ConfirmUserPasswordResetAsync(inputGateway);

            if (!outputGateway.IsSuccess)
            {
                Notify("Confirmar troca de senha", $"Falha ao confirmar a troca de senha para {input.UserEmail}");

                return new(IsSuccess: false, outputGateway.ConfirmUserPasswordResetMessage);
            }

            return new(IsSuccess: true);
        }
    }
}