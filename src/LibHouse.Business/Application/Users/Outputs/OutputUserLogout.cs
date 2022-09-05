namespace LibHouse.Business.Application.Users.Outputs
{
    public record OutputUserLogout(bool IsSuccess = false, string LogoutMessage = "");
}