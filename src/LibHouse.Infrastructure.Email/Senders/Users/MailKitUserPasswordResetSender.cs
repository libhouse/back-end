using LibHouse.Business.Application.Users.Senders;
using LibHouse.Infrastructure.Email.Models;
using LibHouse.Infrastructure.Email.Services;
using LibHouse.Infrastructure.Email.Settings.Users;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace LibHouse.Infrastructure.Email.Senders.Users
{
    public class MailKitUserPasswordResetSender : IUserPasswordResetSender
    {
        private readonly IMailService _mailService;
        private readonly UserPasswordResetSenderSettings _settings;

        public MailKitUserPasswordResetSender(
            IMailService mailService, 
            IOptions<UserPasswordResetSenderSettings> settings)
        {
            _mailService = mailService;
            _settings = settings.Value;
        }

        public async Task<OutputUserPasswordResetSender> SendUserPasswordResetRequestAsync(
            InputUserPasswordResetSender input)
        {
            string passwordResetTokenMessage = BuildMessageForSendPasswordResetTokenToUser(input);
            var mailRequest = new MailRequest(toEmail: input.UserEmail, subject: "Redefinição de senha", body: passwordResetTokenMessage);
            return await _mailService.SendEmailAsync(mailRequest)
                ? new(IsSuccess: true)
                : new(IsSuccess: false, SendingMessage: "Falha no serviço de e-mail");
        }

        private string BuildMessageForSendPasswordResetTokenToUser(InputUserPasswordResetSender input)
        {
            return $"{input.UserName}, se você solicitou a sua redefinição de senha na plataforma LibHouse, " +
                $"clique neste link para concluir o processo: {_settings.RequestPasswordResetAddress}/" +
                $"{input.UserEmail}/{input.UserId}/{input.UserPasswordResetToken}";
        }
    }
}