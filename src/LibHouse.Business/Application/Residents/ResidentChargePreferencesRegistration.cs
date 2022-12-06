using LibHouse.Business.Application.Residents.Inputs;
using LibHouse.Business.Application.Residents.Interfaces;
using LibHouse.Business.Application.Residents.Outputs;
using LibHouse.Business.Application.Shared;
using LibHouse.Business.Entities.Residents;
using LibHouse.Business.Entities.Residents.Preferences.Charges;
using LibHouse.Business.Notifiers;
using System.Threading.Tasks;

namespace LibHouse.Business.Application.Residents
{
    public class ResidentChargePreferencesRegistration : BaseUseCase, IResidentChargePreferencesRegistration
    {
        private readonly IResidentRepository _residentRepository;

        public ResidentChargePreferencesRegistration(
            INotifier notifier,
            IResidentRepository residentRepository) 
            : base(notifier)
        {
            _residentRepository = residentRepository;
        }

        public async Task<OutputResidentChargePreferencesRegistration> ExecuteAsync(InputResidentChargePreferencesRegistration input)
        {
            Resident resident = await _residentRepository.GetByIdAsNoTrackingAsync(input.ResidentId);
            if (resident is null)
            {
                Notify("Morador não encontrado", $"O morador {input.ResidentId} não foi encontrado");
                return new() { IsSuccess = false, ChargePreferencesRegistrationMessage = "O morador não foi encontrado" };
            }
            if (!resident.IsActive)
            {
                Notify("Morador inativo", $"O morador {input.ResidentId} não está com o seu perfil ativo");
                return new() { IsSuccess = false, ChargePreferencesRegistrationMessage = "O morador não está com o seu perfil ativo" };
            }
            if (resident.HaveChargePreferences())
            {
                Notify("Morador com preferências", $"O morador {input.ResidentId} já possui preferências cadastradas");
                return new() { IsSuccess = false, ChargePreferencesRegistrationMessage = "O morador já possui preferências cadastradas" };
            }
            resident.WithPreferences();
            ChargePreferences chargePreferences = new();
            chargePreferences.AddExpensePreferences(input.MinimumExpenseAmountDesired, input.MaximumExpenseAmountDesired);
            chargePreferences.AddRentPreferences(input.MinimumRentalAmountDesired, input.MaximumRentalAmountDesired);
            resident.AddChargePreferences(chargePreferences);
            bool preferencesHaveBeendAdded = await _residentRepository.AddOrUpdateResidentChargePreferencesAsync(resident.Id, resident.GetChargePreferences());
            if (!preferencesHaveBeendAdded)
            {
                Notify("Falha inesperada", $"Falha ao salvar as preferências do morador {input.ResidentId}");
                return new() { IsSuccess = false, ChargePreferencesRegistrationMessage = "Falha ao salvar as preferências do morador" };
            }
            return new() { IsSuccess = true };
        }
    }
}