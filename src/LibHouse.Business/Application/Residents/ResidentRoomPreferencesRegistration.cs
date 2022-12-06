using LibHouse.Business.Application.Residents.Inputs;
using LibHouse.Business.Application.Residents.Interfaces;
using LibHouse.Business.Application.Residents.Outputs;
using LibHouse.Business.Application.Shared;
using LibHouse.Business.Entities.Residents;
using LibHouse.Business.Entities.Residents.Preferences.Rooms.Builders;
using LibHouse.Business.Notifiers;
using System.Threading.Tasks;

namespace LibHouse.Business.Application.Residents
{
    public class ResidentRoomPreferencesRegistration : BaseUseCase, IResidentRoomPreferencesRegistration
    {
        private readonly IResidentRepository _residentRepository;
        private readonly IRoomPreferencesBuilder _roomPreferencesBuilder;

        public ResidentRoomPreferencesRegistration(
            INotifier notifier, 
            IResidentRepository residentRepository,
            IRoomPreferencesBuilder roomPreferencesBuilder)
            : base(notifier)
        {
            _residentRepository = residentRepository;
            _roomPreferencesBuilder = roomPreferencesBuilder;
        }

        public async Task<OutputResidentRoomPreferencesRegistration> ExecuteAsync(
            InputResidentRoomPreferencesRegistration input)
        {
            Resident resident = await _residentRepository.GetByIdAsNoTrackingAsync(input.ResidentId);
            if (resident is null)
            {
                Notify("Morador não encontrado", $"O morador {input.ResidentId} não foi encontrado");
                return new() { IsSuccess = false, RoomPreferencesRegistrationMessage = "O morador não foi encontrado" };
            }
            if (!resident.IsActive)
            {
                Notify("Morador inativo", $"O morador {input.ResidentId} não está com o seu perfil ativo");
                return new() { IsSuccess = false, RoomPreferencesRegistrationMessage = "O morador não está com o seu perfil ativo" };
            }
            if (resident.HaveRoomPreferences())
            {
                Notify("Morador com preferências", $"O morador {input.ResidentId} já possui preferências cadastradas");
                return new() { IsSuccess = false, RoomPreferencesRegistrationMessage = "O morador já possui preferências cadastradas" };
            }
            resident.WithPreferences();
            _roomPreferencesBuilder.WithBathroomPreferences(input.WantPrivateBathroom);
            _roomPreferencesBuilder.WithDormitoryPreferences(input.WantSingleDormitory, input.WantFurnishedDormitory);
            _roomPreferencesBuilder.WithGaragePreferences(input.WantGarage);
            _roomPreferencesBuilder.WithKitchenPreferences(input.WantStove, input.WantMicrowave, input.WantRefrigerator);
            _roomPreferencesBuilder.WithOtherPreferences(input.WantServiceArea, input.WantRecreationArea);
            resident.AddRoomPreferences(_roomPreferencesBuilder.GetRoomPreferences());
            bool preferencesHaveBeendAdded = await _residentRepository.AddOrUpdateResidentRoomPreferencesAsync(resident.Id, resident.GetRoomPreferences());
            if (!preferencesHaveBeendAdded)
            {
                Notify("Falha inesperada", $"Falha ao salvar as preferências do morador {input.ResidentId}");
                return new() { IsSuccess = false, RoomPreferencesRegistrationMessage = "Falha ao salvar as preferências do morador" };
            }
            return new() { IsSuccess = true };
        }
    }
}