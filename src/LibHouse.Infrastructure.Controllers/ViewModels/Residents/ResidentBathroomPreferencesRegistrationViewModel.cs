﻿namespace LibHouse.Infrastructure.Controllers.ViewModels.Residents
{
    /// <summary>
    /// Representa os dados de cadastro das preferências de banheiro de um morador
    /// </summary>
    public record ResidentBathroomPreferencesRegistrationViewModel
    {
        /// <summary>
        /// Indica se um morador quer morar em uma casa que tenha um banheiro individual para ele
        /// </summary>
        public bool WantPrivateBathroom { get; init; }
    }
}