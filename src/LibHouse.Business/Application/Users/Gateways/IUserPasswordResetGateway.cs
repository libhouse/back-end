using LibHouse.Business.Application.Users.Gateways.Outputs;
using System.Threading.Tasks;

namespace LibHouse.Business.Application.Users.Gateways
{
    public interface IUserPasswordResetGateway
    {
        Task<OutputUserPasswordResetGateway> ResetUserPasswordAsync(string userEmail);
    }
}