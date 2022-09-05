namespace LibHouse.Business.Application.Users.Gateways.Inputs
{
    public record InputConfirmUserRegistrationGateway(string UserEmail, string RegistrationToken);
}