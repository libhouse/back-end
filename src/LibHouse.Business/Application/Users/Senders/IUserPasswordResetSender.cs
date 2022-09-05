using System;
using System.Threading.Tasks;

namespace LibHouse.Business.Application.Users.Senders
{
    public interface IUserPasswordResetSender
    {
        Task<OutputUserPasswordResetSender> SendUserPasswordResetRequestAsync(InputUserPasswordResetSender input);
    }

    public record InputUserPasswordResetSender
    {
        public Guid UserId { get; init; }
        public string UserName { get; init; }
        public string UserEmail { get; init; }
        public string UserPasswordResetToken { get; init; }

        public InputUserPasswordResetSender(
            Guid userId, 
            string userName, 
            string userEmail, 
            string userPasswordResetToken)
        {
            UserId = userId;
            UserName = userName;
            UserEmail = userEmail;
            UserPasswordResetToken = userPasswordResetToken;
        }
    }

    public record OutputUserPasswordResetSender(bool IsSuccess, string SendingMessage = "");
}