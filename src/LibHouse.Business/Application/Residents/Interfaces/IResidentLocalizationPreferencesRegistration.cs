using LibHouse.Business.Application.Residents.Inputs;
using LibHouse.Business.Application.Residents.Outputs;
using System.Threading.Tasks;

namespace LibHouse.Business.Application.Residents.Interfaces
{
    public interface IResidentLocalizationPreferencesRegistration
    {
        Task<OutputResidentLocalizationPreferencesRegistration> ExecuteAsync(InputResidentLocalizationPreferencesRegistration input);
    }
}