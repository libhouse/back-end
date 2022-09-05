using LibHouse.Business.Application.Users.Gateways.Outputs;
using System.Threading.Tasks;

namespace LibHouse.Business.Application.Users.Gateways
{
    public interface IUserLoginGateway
    {
        Task<OutputUserLoginGateway> LoginAsync(string userName, string userPassword);
    }
}