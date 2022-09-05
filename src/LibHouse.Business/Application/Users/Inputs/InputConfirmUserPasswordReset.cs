namespace LibHouse.Business.Application.Users.Inputs
{
    public record InputConfirmUserPasswordReset(string UserEmail, string NewPassword, string PasswordResetToken);
}