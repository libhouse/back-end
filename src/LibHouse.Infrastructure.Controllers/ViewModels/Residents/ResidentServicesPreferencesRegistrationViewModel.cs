using System;
using System.ComponentModel.DataAnnotations;

namespace LibHouse.Infrastructure.Controllers.ViewModels.Residents
{
    /// <summary>
    /// Representa os dados de cadastro das preferências de serviços de um morador
    /// </summary>
    public record ResidentServicesPreferencesRegistrationViewModel
    {
        /// <summary>
        /// A identificação do morador que terá as suas preferências cadastradas
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public Guid ResidentId { get; init; }
        /// <summary>
        /// Indica se o morador deseja morar em uma casa com serviço de limpeza
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public bool WantHouseCleaningService { get; init; }
        /// <summary>
        /// Indica se o morador deseja morar em uma casa com serviço de internet
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public bool WantInternetService { get; init; }
        /// <summary>
        /// Indica se o morador deseja morar em uma casa com serviço de televisão a cabo
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public bool WantCableTelevisionService { get; init; }
    }
}