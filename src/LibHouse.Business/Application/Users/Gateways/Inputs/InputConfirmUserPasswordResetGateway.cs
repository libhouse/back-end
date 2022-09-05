namespace LibHouse.Business.Application.Users.Gateways.Inputs
{
    public record InputConfirmUserPasswordResetGateway(string UserEmail, string NewPassword, string PasswordResetToken);
}