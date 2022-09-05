using System;
using System.Threading.Tasks;

namespace LibHouse.Business.Application.Users.Senders
{
    public interface IUserRegistrationSender
    {
        Task<OutputUserRegistrationSender> SendUserRegistrationDataAsync(InputUserRegistrationSender input);
    }

    public record InputUserRegistrationSender(string Email, string Name, Guid UserId, string RegistrationToken);

    public record OutputUserRegistrationSender(bool IsSuccess, string SendingMessage = "");
}