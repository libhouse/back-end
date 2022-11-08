using LibHouse.Infrastructure.Email.Models;
using LibHouse.Infrastructure.Email.Settings.Shared;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace LibHouse.Infrastructure.Email.Services
{
    public class MailKitService : IMailService
    {
        private readonly MailSettings _mailSettings;

        public MailKitService(MailSettings mailSettings)
        {
            _mailSettings = mailSettings;
        }

        public async Task<bool> SendEmailAsync(MailRequest mailRequest)
        {
            try
            {
                MimeMessage email = BuildMessageFromMailRequest(mailRequest);
                using var smtp = new SmtpClient();
                smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private MimeMessage BuildMessageFromMailRequest(MailRequest mailRequest)
        {
            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_mailSettings.Mail),
                Subject = mailRequest.Subject,
            };
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            var builder = new BodyBuilder
            {
                HtmlBody = mailRequest.Body
            };
            email.Body = builder.ToMessageBody();
            return email;
        }
    }
}