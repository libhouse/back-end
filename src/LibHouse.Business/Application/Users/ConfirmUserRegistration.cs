using LibHouse.Business.Application.Shared;
using LibHouse.Business.Application.Users.Gateways;
using LibHouse.Business.Application.Users.Gateways.Outputs;
using LibHouse.Business.Application.Users.Inputs;
using LibHouse.Business.Application.Users.Interfaces;
using LibHouse.Business.Application.Users.Outputs;
using LibHouse.Business.Entities.Shared;
using LibHouse.Business.Entities.Users;
using LibHouse.Business.Notifiers;
using System.Threading.Tasks;

namespace LibHouse.Business.Application.Users
{
    public class ConfirmUserRegistration : BaseUseCase, IConfirmUserRegistration
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfirmUserRegistrationGateway _confirmUserRegistrationGateway;

        public ConfirmUserRegistration(
            INotifier notifier,
            IUnitOfWork unitOfWork,
            IConfirmUserRegistrationGateway confirmUserRegistrationGateway) 
            : base(notifier)
        {
            _unitOfWork = unitOfWork;
            _confirmUserRegistrationGateway = confirmUserRegistrationGateway;
        }

        public async Task<OutputConfirmUserRegistration> ExecuteAsync(InputConfirmUserRegistration input)
        {
            User user = await _unitOfWork.UserRepository.GetByIdAsync(input.UserId);

            if (user is null)
            {
                Notify("Confirmar cadastro", "O usuário não foi encontrado.");

                return new(ConfirmationMessage: $"O usuário {input.UserEmail} não foi encontrado.");
            }

            if (user.IsActive)
            {
                return new(IsSuccess: true);
            }

            OutputConfirmUserRegistrationGateway outputGateway = await _confirmUserRegistrationGateway.ConfirmUserRegistrationAsync(new(input.UserEmail, input.RegistrationToken));

            if (!outputGateway.IsSuccess)
            {
                Notify("Aceitar confirmação do usuário", outputGateway.ConfirmationMessage);

                return new(ConfirmationMessage: outputGateway.ConfirmationMessage);
            }

            user.Activate();

            bool isUserActivated = await _unitOfWork.CommitAsync();

            if (!isUserActivated)
            {
                Notify("Confirmar cadastro", "Erro ao ativar o cadastro do usuário.");

                return new(ConfirmationMessage: $"Erro ao ativar o cadastro do usuário {input.UserEmail}.");
            }

            return new(IsSuccess: true);
        }
    }
}