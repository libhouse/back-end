namespace LibHouse.Business.Application.Users.Outputs
{
    public record OutputConfirmUserRegistration(bool IsSuccess = false, string ConfirmationMessage = "");
}