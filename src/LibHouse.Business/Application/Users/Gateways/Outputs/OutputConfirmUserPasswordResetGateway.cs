namespace LibHouse.Business.Application.Users.Gateways.Outputs
{
    public record OutputConfirmUserPasswordResetGateway(bool IsSuccess = false, string ConfirmUserPasswordResetMessage = "");
}