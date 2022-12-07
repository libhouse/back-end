﻿using System.ComponentModel.DataAnnotations;

namespace LibHouse.Infrastructure.Controllers.ViewModels.Residents
{
    /// <summary>
    /// Representa os dados de cadastro das preferências de garagem de um morador
    /// </summary>
    public record ResidentGaragePreferencesRegistrationViewModel
    {
        /// <summary>
        /// Indica se um morador precisa morar em uma casa que tenha garagem
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public bool WantGarage { get; init; }
    }
}