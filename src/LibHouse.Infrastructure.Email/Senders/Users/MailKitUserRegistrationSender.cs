using LibHouse.Business.Application.Users.Senders;
using LibHouse.Infrastructure.Email.Models;
using LibHouse.Infrastructure.Email.Services;
using LibHouse.Infrastructure.Email.Settings.Users;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace LibHouse.Infrastructure.Email.Senders.Users
{
    public class MailKitUserRegistrationSender : IUserRegistrationSender
    {
        private readonly IMailService _mailService;
        private readonly UserRegistrationSenderSettings _settings;

        public MailKitUserRegistrationSender(
            IMailService mailService,
            IOptions<UserRegistrationSenderSettings> settings)
        {
            _mailService = mailService;
            _settings = settings.Value;
        }

        public async Task<OutputUserRegistrationSender> SendUserRegistrationDataAsync(InputUserRegistrationSender input)
        {
            string userRegistrationMessage = BuildMessageForUserRegistration(input);

            var mailRequest = new MailRequest(toEmail: input.Email, subject: "Confirme o seu e-mail", body: userRegistrationMessage);

            return await _mailService.SendEmailAsync(mailRequest)
                ? new(IsSuccess: true)
                : new(IsSuccess: false, SendingMessage: "Falha no serviço de e-mail");
        }

        private string BuildMessageForUserRegistration(InputUserRegistrationSender input)
        {
            return $"{input.Name}, seja bem-vindo(a) ao LibHouse. Se você solicitou o cadastro " +
                $"na plataforma, confirme o seu e-mail clicando neste link: {_settings.ConfirmEmailAddress}/" +
                $"{input.Email}/{input.UserId}/{input.RegistrationToken}";
        }
    }
}