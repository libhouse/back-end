using LibHouse.Business.Application.Localizations.Gateways.Outputs;
using System.Threading.Tasks;

namespace LibHouse.Business.Application.Localizations.Gateways
{
    public interface IAddressPostalCodeGateway
    {
        Task<OutputAddressPostalCodeGateway> GetAddressByPostalCodeAsync(string postalCode);
    }
}