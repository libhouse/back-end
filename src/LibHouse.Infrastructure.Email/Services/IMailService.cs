using LibHouse.Infrastructure.Email.Models;
using System.Threading.Tasks;

namespace LibHouse.Infrastructure.Email.Services
{
    public interface IMailService
    {
        Task<bool> SendEmailAsync(MailRequest mailRequest);
    }
}