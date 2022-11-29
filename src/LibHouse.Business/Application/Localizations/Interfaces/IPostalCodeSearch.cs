using LibHouse.Business.Application.Localizations.Outputs;
using System.Threading.Tasks;

namespace LibHouse.Business.Application.Localizations.Interfaces
{
    public interface IPostalCodeSearch
    {
        Task<OutputPostalCodeSearch> ExecuteAsync(string postalCodeNumber);
    }
}