using LibHouse.Business.Application.Residents.Inputs;
using LibHouse.Business.Application.Residents.Inputs.Converters;
using LibHouse.Business.Application.Residents.Interfaces;
using LibHouse.Business.Application.Residents.Outputs;
using LibHouse.Business.Application.Shared;
using LibHouse.Business.Entities.Localizations;
using LibHouse.Business.Entities.Residents;
using LibHouse.Business.Entities.Residents.Preferences.Localizations;
using LibHouse.Business.Entities.Shared;
using LibHouse.Business.Notifiers;
using System.Threading.Tasks;

namespace LibHouse.Business.Application.Residents
{
    public class ResidentLocalizationPreferencesRegistration : BaseUseCase, IResidentLocalizationPreferencesRegistration
    {
        private readonly IUnitOfWork _unitOfWork;

        public ResidentLocalizationPreferencesRegistration(
            INotifier notifier, 
            IUnitOfWork unitOfWork) 
            : base(notifier)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OutputResidentLocalizationPreferencesRegistration> ExecuteAsync(InputResidentLocalizationPreferencesRegistration input)
        {
            Resident resident = await _unitOfWork.ResidentRepository.GetByIdAsNoTrackingAsync(input.ResidentId);
            if (resident is null)
            {
                Notify("Morador não encontrado", $"O morador {input.ResidentId} não foi encontrado");
                return new() { IsSuccess = false, LocalizationPreferencesRegistrationMessage = "O morador não foi encontrado" };
            }
            if (!resident.IsActive)
            {
                Notify("Morador inativo", $"O morador {input.ResidentId} não está com o seu perfil ativo");
                return new() { IsSuccess = false, LocalizationPreferencesRegistrationMessage = "O morador não está com o seu perfil ativo" };
            }
            if (resident.HaveLocalizationPreferences())
            {
                Notify("Morador com preferências", $"O morador {input.ResidentId} já possui preferências cadastradas");
                return new() { IsSuccess = false, LocalizationPreferencesRegistrationMessage = "O morador já possui preferências cadastradas" };
            }
            Address landmarkAddress = await _unitOfWork.AddressRepository.GetAddressByPostalCodeAndNumberAsync(input.AddressPostalCodeNumber, input.AddressNumber);
            await _unitOfWork.StartWorkAsync();
            if (landmarkAddress is null)
            {
                landmarkAddress = input.Convert();
                await _unitOfWork.AddressRepository.AddAsync(landmarkAddress);
            }
            LocalizationPreferences localizationPreferences = new();
            localizationPreferences.AddLandmarkPreferences(landmarkAddress);
            resident.WithPreferences();
            resident.AddLocalizationPreferences(localizationPreferences);
            _ = await _unitOfWork.ResidentRepository.AddOrUpdateResidentLocalizationPreferencesAsync(resident.Id, resident.GetLocalizationPreferences());
            bool preferencesHaveBeendAdded = await _unitOfWork.CommitAsync();
            if (!preferencesHaveBeendAdded)
            {
                Notify("Falha inesperada", $"Falha ao salvar as preferências do morador {input.ResidentId}");
                return new() { IsSuccess = false, LocalizationPreferencesRegistrationMessage = "Falha ao salvar as preferências do morador" };
            }
            return new() { IsSuccess = true };
        }
    }
}