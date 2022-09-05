namespace LibHouse.Business.Application.Users.Outputs
{
    public record OutputConfirmUserPasswordReset(bool IsSuccess = false, string ConfirmUserPasswordResetMessage = "");
}