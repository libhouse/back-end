using LibHouse.Business.Application.Residents.Inputs;
using LibHouse.Business.Application.Residents.Outputs;
using System.Threading.Tasks;

namespace LibHouse.Business.Application.Residents.Interfaces
{
    public interface IResidentChargePreferencesRegistration
    {
        Task<OutputResidentChargePreferencesRegistration> ExecuteAsync(InputResidentChargePreferencesRegistration input);
    }
}