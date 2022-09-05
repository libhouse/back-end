using System;
using System.ComponentModel.DataAnnotations;

namespace LibHouse.Infrastructure.Email.Models
{
    public class MailRequest
    {
        [EmailAddress(ErrorMessage = "O campo {0} está em um formato inválido.")]
        public string ToEmail { get; }
        public string Subject { get; }
        public string Body { get; }

        public MailRequest(string toEmail, string subject, string body)
        {
            if (string.IsNullOrEmpty(subject))
            {
                throw new ArgumentNullException(nameof(subject), "O assunto é obrigatório.");
            }

            if (string.IsNullOrEmpty(body))
            {
                throw new ArgumentNullException(nameof(body), "O corpo é obrigatório.");
            }

            ToEmail = toEmail;
            Subject = subject;
            Body = body;
        }

        public override string ToString()
        {
            return $"Mensagem {Subject} enviada para {ToEmail}";
        }
    }
}