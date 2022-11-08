using LibHouse.Business.Application.Shared;
using LibHouse.Business.Application.Users.Adapters;
using LibHouse.Business.Application.Users.Gateways;
using LibHouse.Business.Application.Users.Gateways.Outputs;
using LibHouse.Business.Application.Users.Inputs;
using LibHouse.Business.Application.Users.Interfaces;
using LibHouse.Business.Application.Users.Outputs;
using LibHouse.Business.Application.Users.Senders;
using LibHouse.Business.Entities.Shared;
using LibHouse.Business.Entities.Users;
using LibHouse.Business.Notifiers;
using LibHouse.Business.Validations.Users;
using System.Threading.Tasks;

namespace LibHouse.Business.Application.Users
{
    public class UserRegistration : BaseUseCase, IUserRegistration
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRegistrationGateway _userRegistrationGateway;
        private readonly IUserRegistrationSender _userRegistrationSender;
        private readonly UserRegistrationValidator _userRegistrationValidator;

        public UserRegistration(
            INotifier notifier,
            IUnitOfWork unitOfWork,
            IUserRegistrationGateway userRegistrationGateway,
            IUserRegistrationSender userRegistrationSender,
            UserRegistrationValidator userRegistrationValidator) 
            : base(notifier)
        {
            _unitOfWork = unitOfWork;
            _userRegistrationGateway = userRegistrationGateway;
            _userRegistrationSender = userRegistrationSender;
            _userRegistrationValidator = userRegistrationValidator;
        }

        public async Task<OutputUserRegistration> ExecuteAsync(InputUserRegistration input)
        {
            User user = input.Adapt();
            user.Inactivate();
            if (!ExecuteValidation(_userRegistrationValidator, user))
            {
                Notify("Usuário registrado", $"O usuário {user.GetEmailAddress()} já está registrado.");
                return new(registrationMessage: $"O usuário {user.GetEmailAddress()} já está registrado.");
            }
            OutputUserRegistrationGateway outputGateway = await _userRegistrationGateway.RegisterUserAsync(new(input.Email, input.Phone, user.UserType, input.Password));
            if (!outputGateway.IsSuccess)
            {
                Notify("Registrar usuário", $"Falha ao registrar o usuário {user.GetEmailAddress()}: {outputGateway.RegistrationMessage}.");
                return new(registrationMessage: $"Falha ao registrar o usuário {user.GetEmailAddress()}.");
            }
            await _unitOfWork.UserRepository.AddAsync(user);
            bool userDataHasBeenPersisted = await _unitOfWork.CommitAsync();
            if (!userDataHasBeenPersisted)
            {
                Notify("Registrar usuário", $"Falha ao armazenar os dados do usuário {user.GetEmailAddress()}.");
                return new(registrationMessage: $"Falha ao armazenar os dados do usuário {user.GetEmailAddress()}.");
            }
            OutputUserRegistrationSender outputSender = await _userRegistrationSender.SendUserRegistrationDataAsync(new(input.Email, input.Name, user.Id, outputGateway.RegistrationToken));
            if (!outputSender.IsSuccess)
            {
                Notify("Comunicar usuário", $"Falha ao comunicar o usuário {user.GetEmailAddress()}: {outputSender.SendingMessage}.");
                return new(registrationMessage: $"Falha ao comunicar o usuário {user.GetEmailAddress()}.");
            }
            return new(isSuccess: true, registrationToken: outputGateway.RegistrationToken);
        }
    }
}