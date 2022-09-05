using LibHouse.Business.Application.Users.Gateways.Inputs;
using LibHouse.Business.Application.Users.Gateways.Outputs;
using System.Threading.Tasks;

namespace LibHouse.Business.Application.Users.Gateways
{
    public interface IConfirmUserRegistrationGateway
    {
        Task<OutputConfirmUserRegistrationGateway> ConfirmUserRegistrationAsync(InputConfirmUserRegistrationGateway input);
    }  
}