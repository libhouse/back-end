using LibHouse.Business.Application.Residents.Inputs;
using LibHouse.Business.Application.Residents.Interfaces;
using LibHouse.Business.Application.Residents.Outputs;
using LibHouse.Business.Application.Shared;
using LibHouse.Business.Entities.Residents;
using LibHouse.Business.Entities.Residents.Preferences.General.Builders;
using LibHouse.Business.Notifiers;
using System.Threading.Tasks;

namespace LibHouse.Business.Application.Residents
{
    public class ResidentGeneralPreferencesRegistration : BaseUseCase, IResidentGeneralPreferencesRegistration
    {
        private readonly IResidentRepository _residentRepository;
        private readonly IGeneralPreferencesBuilder _generalPreferencesBuilder;

        public ResidentGeneralPreferencesRegistration(
            INotifier notifier, 
            IResidentRepository residentRepository, 
            IGeneralPreferencesBuilder generalPreferencesBuilder) 
            : base(notifier)
        {
            _residentRepository = residentRepository;
            _generalPreferencesBuilder = generalPreferencesBuilder;
        }

        public async Task<OutputResidentGeneralPreferencesRegistration> ExecuteAsync(InputResidentGeneralPreferencesRegistration input)
        {
            Resident resident = await _residentRepository.GetByIdAsNoTrackingAsync(input.ResidentId);
            if (resident is null)
            {
                Notify("Morador não encontrado", $"O morador {input.ResidentId} não foi encontrado");
                return new() { IsSuccess = false, GeneralPreferencesRegistrationMessage = "O morador não foi encontrado" };
            }
            if (!resident.IsActive)
            {
                Notify("Morador inativo", $"O morador {input.ResidentId} não está com o seu perfil ativo");
                return new() { IsSuccess = false, GeneralPreferencesRegistrationMessage = "O morador não está com o seu perfil ativo" };
            }
            if (resident.HaveGeneralPreferences())
            {
                Notify("Morador com preferências", $"O morador {input.ResidentId} já possui preferências cadastradas");
                return new() { IsSuccess = false, GeneralPreferencesRegistrationMessage = "O morador já possui preferências cadastradas" };
            }
            resident.WithPreferences();
            _generalPreferencesBuilder.WithAnimalPreferences(input.WantSpaceForAnimals);
            _generalPreferencesBuilder.WithChildrenPreferences(input.AcceptChildren);
            _generalPreferencesBuilder.WithPartyPreferences(input.WantsToParty);
            _generalPreferencesBuilder.WithRoommatePreferences(input.AcceptsOnlyMenAsRoommates, input.AcceptsOnlyWomenAsRoommates, input.MinimumNumberOfRoommatesDesired, input.MaximumNumberOfRoommatesDesired);
            _generalPreferencesBuilder.WithSmokersPreferences(input.AcceptSmokers);
            resident.AddGeneralPreferences(_generalPreferencesBuilder.GetGeneralPreferences());
            bool preferencesHaveBeendAdded = await _residentRepository.AddOrUpdateResidentGeneralPreferencesAsync(resident.Id, resident.GetGeneralPreferences());
            if (!preferencesHaveBeendAdded)
            {
                Notify("Falha inesperada", $"Falha ao salvar as preferências do morador {input.ResidentId}");
                return new() { IsSuccess = false, GeneralPreferencesRegistrationMessage = "Falha ao salvar as preferências do morador" };
            }
            return new() { IsSuccess = true };
        }
    }
}