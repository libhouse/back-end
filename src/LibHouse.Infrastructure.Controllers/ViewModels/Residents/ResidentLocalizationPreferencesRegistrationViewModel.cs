using LibHouse.Infrastructure.Controllers.Attributes.Shared;
using System;
using System.ComponentModel.DataAnnotations;

namespace LibHouse.Infrastructure.Controllers.ViewModels.Residents
{
    /// <summary>
    /// Representa os dados de cadastro das preferências de localização de um morador
    /// </summary>
    public record ResidentLocalizationPreferencesRegistrationViewModel
    {
        /// <summary>
        /// A identificação do morador que terá as suas preferências cadastradas
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public Guid ResidentId { get; init; }
        /// <summary>
        /// A descrição do endereço de referência do morador
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(60, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 1)]
        public string LandmarkAddressDescription { get; init; }
        /// <summary>
        /// O complemento do endereço de referência do morador
        /// </summary>
        public string LandmarkAddressComplement { get; init; }
        /// <summary>
        /// O número do endereço de referência do morador
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Range(typeof(ushort), "1", "65535", ErrorMessage = "O campo {0} deve possuir um valor entre {1} and {2}.")]
        public ushort LandmarkAddressNumber { get; init; }
        /// <summary>
        /// O nome do bairro do endereço de referência do morador
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(60, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 1)]
        public string LandmarkAddressNeighborhood { get; init; }
        /// <summary>
        /// O nome da cidade do endereço de referência do morador
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(30, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 1)]
        public string LandmarkAddressCity { get; init; }
        /// <summary>
        /// A sigla da unidade federativa do endereço de referência do morador
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(2, ErrorMessage = "O campo {0} deve ter {1} caracteres.", MinimumLength = 2)]
        public string LandmarkAddressFederativeUnit { get; init; }
        /// <summary>
        /// O número do código postal do endereço de referência do morador
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [PostalCode(ErrorMessage = "O campo {0} está em um formato inválido.")]
        public string LandmarkAddressPostalCodeNumber { get; init; }
    }
}