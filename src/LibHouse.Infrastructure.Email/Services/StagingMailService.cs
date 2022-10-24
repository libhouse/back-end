using LibHouse.Infrastructure.Email.Models;
using System.Threading.Tasks;

namespace LibHouse.Infrastructure.Email.Services
{
    public class StagingMailService : IMailService
    {
        public Task<bool> SendEmailAsync(MailRequest mailRequest)
        {
            return Task.FromResult(true);
        }
    }
}