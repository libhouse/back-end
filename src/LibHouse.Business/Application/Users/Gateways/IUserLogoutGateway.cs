using LibHouse.Business.Application.Users.Gateways.Outputs;
using System.Threading.Tasks;

namespace LibHouse.Business.Application.Users.Gateways
{
    public interface IUserLogoutGateway
    {
        Task<OutputUserLogoutGateway> LogoutAsync(string userEmail, string userToken);
    }
}