namespace LibHouse.Business.Application.Users.Outputs
{
    public record OutputUserPasswordReset
    {
        public bool IsSuccess { get; init; }
        public string PasswordResetToken { get; init; }
        public string UserPasswordResetMessage { get; init; }

        public OutputUserPasswordReset(
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