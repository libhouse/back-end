using LibHouse.Business.Application.Residents.Inputs;
using LibHouse.Business.Application.Residents.Interfaces;
using LibHouse.Business.Application.Residents.Outputs;
using LibHouse.Business.Application.Shared;
using LibHouse.Business.Entities.Residents;
using LibHouse.Business.Entities.Residents.Preferences.Services;
using LibHouse.Business.Notifiers;
using System.Threading.Tasks;

namespace LibHouse.Business.Application.Residents
{
    public class ResidentServicesPreferencesRegistration : BaseUseCase, IResidentServicesPreferencesRegistration
    {
        private readonly IResidentRepository _residentRepository;

        public ResidentServicesPreferencesRegistration(
            INotifier notifier, 
            IResidentRepository residentRepository) 
            : base(notifier)
        {
            _residentRepository = residentRepository;
        }

        public async Task<OutputResidentServicesPreferencesRegistration> ExecuteAsync(InputResidentServicesPreferencesRegistration input)
        {
            Resident resident = await _residentRepository.GetByIdAsync(input.ResidentId);
            if (resident is null)
            {
                Notify("Morador não encontrado", $"O morador {input.ResidentId} não foi encontrado");
                return new() { IsSuccess = false, ServicesPreferencesRegistrationMessage = "O morador não foi encontrado" };
            }
            if (!resident.IsActive)
            {
                Notify("Morador inativo", $"O morador {input.ResidentId} não está com o seu perfil ativo");
                return new() { IsSuccess = false, ServicesPreferencesRegistrationMessage = "O morador não está com o seu perfil ativo" };
            }
            if (resident.HaveServicesPreferences())
            {
                Notify("Morador com preferências", $"O morador {input.ResidentId} já possui preferências cadastradas");
                return new() { IsSuccess = false, ServicesPreferencesRegistrationMessage = "O morador já possui preferências cadastradas" };
            }
            resident.WithPreferences();
            ServicesPreferences servicesPreferences = new();
            servicesPreferences.AddCleaningPreferences(input.WantHouseCleaningService);
            servicesPreferences.AddInternetPreferences(input.WantInternetService);
            servicesPreferences.AddTelevisionPreferences(input.WantCableTelevisionService);
            resident.AddServicesPreferences(servicesPreferences);
            bool preferencesHaveBeendAdded = await _residentRepository.AddOrUpdateResidentServicesPreferencesAsync(resident.Id, resident.GetServicesPreferences());
            if (!preferencesHaveBeendAdded)
            {
                Notify("Falha inesperada", $"Falha ao salvar as preferências do morador {input.ResidentId}");
                return new() { IsSuccess = false, ServicesPreferencesRegistrationMessage = "Falha ao salvar as preferências do morador" };
            }
            return new() { IsSuccess = true };
        }
    }
}