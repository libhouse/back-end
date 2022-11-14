using System;

namespace LibHouse.Infrastructure.Controllers.ViewModels.Residents
{
    /// <summary>
    /// Representa os dados de cadastro das preferências de cômodos de um morador
    /// </summary>
    public record ResidentRoomPreferencesRegistrationViewModel
    {
        /// <summary>
        /// A identificação do morador que terá as suas preferências cadastradas
        /// </summary>
        public Guid ResidentId { get; init; }
        /// <summary>
        /// Objeto com os dados de cadastro das preferências de dormitório de um morador
        /// </summary>
        public ResidentDormitoryPreferencesRegistrationViewModel DormitoryPreferences { get; init; }
        /// <summary>
        /// Objeto com os dados de cadastro das preferências de cozinha de um morador
        /// </summary>
        public ResidentKitchenPreferencesRegistrationViewModel KitchenPreferences { get; init; }
        /// <summary>
        /// Objeto com os dados de cadastro das preferências de garagem de um morador
        /// </summary>
        public ResidentGaragePreferencesRegistrationViewModel GaragePreferences { get; init; }
        /// <summary>
        /// Objeto com os dados de cadastro das preferências de banheiro de um morador
        /// </summary>
        public ResidentBathroomPreferencesRegistrationViewModel BathroomPreferences { get; init; }
        /// <summary>
        /// Objeto com os dados de cadastro de outras preferências de cômodos de um morador
        /// </summary>
        public ResidentOtherPreferencesRegistrationViewModel OtherPreferences { get; init; }
    }
}