namespace LibHouse.Business.Application.Users.Inputs
{
    public record InputUserLoginRenewal(string UserEmail, string UserLoginToken, string UserLoginRenewalToken);
}