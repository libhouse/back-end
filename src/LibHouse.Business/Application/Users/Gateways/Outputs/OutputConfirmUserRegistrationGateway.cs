namespace LibHouse.Business.Application.Users.Gateways.Outputs
{
    public record OutputConfirmUserRegistrationGateway(bool IsSuccess = false, string ConfirmationMessage = "");
}