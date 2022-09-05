namespace LibHouse.Business.Application.Users.Gateways.Outputs
{
    public record OutputUserLogoutGateway(bool IsSuccess = false, bool IsForbidden = false, string LogoutMessage = "");
}