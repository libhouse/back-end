namespace LibHouse.Business.Application.Users.Gateways.Outputs
{
    public record OutputUserPasswordResetGateway
    {
        public bool IsSuccess { get; init; }
        public string PasswordResetToken { get; init; }
        public string UserPasswordResetMessage { get; init; }

        public OutputUserPasswordResetGateway(
            bool isSuccess = false,
            string passwordResetToken = "",
            string userPasswordResetMessage = "")
        {
            IsSuccess = isSuccess;
            PasswordResetToken = passwordResetToken;
            UserPasswordResetMessage = userPasswordResetMessage;
        }
    }
}