﻿using System.ComponentModel.DataAnnotations;

namespace LibHouse.Infrastructure.Controllers.ViewModels.Residents
{
    /// <summary>
    /// Representa os dados de cadastro de outras preferências de cômodos de um morador
    /// </summary>
    public record ResidentOtherPreferencesRegistrationViewModel
    {
        /// <summary>
        /// Indica se um morador precisa morar em uma casa que tenha área de serviço
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public bool WantServiceArea { get; init; }
        /// <summary>
        /// Indica se um morador precisa morar em uma casa que tenha área de lazer
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public bool WantRecreationArea { get; init; }
    }
}