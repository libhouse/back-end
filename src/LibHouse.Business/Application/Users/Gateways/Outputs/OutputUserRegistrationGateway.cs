namespace LibHouse.Business.Application.Users.Gateways.Outputs
{
    public record OutputUserRegistrationGateway(bool IsSuccess, string RegistrationToken = "", string RegistrationMessage = "");
}