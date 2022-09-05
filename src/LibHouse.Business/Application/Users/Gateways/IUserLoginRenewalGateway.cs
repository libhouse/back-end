using LibHouse.Business.Application.Users.Gateways.Outputs;
using System.Threading.Tasks;

namespace LibHouse.Business.Application.Users.Gateways
{
    public interface IUserLoginRenewalGateway
    {
        Task<OutputUserLoginRenewalGateway> RenewLoginAsync(string userName, string accessTokenValue, string refreshTokenValue);
    }
}