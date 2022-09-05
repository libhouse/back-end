using LibHouse.Business.Entities.Users;

namespace LibHouse.Business.Application.Users.Gateways.Inputs
{
    public record InputUserRegistrationGateway(string Email, string Phone, UserType UserType, string Password);
}