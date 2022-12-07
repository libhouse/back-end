using System;
using System.ComponentModel.DataAnnotations;

namespace LibHouse.Infrastructure.Controllers.ViewModels.Residents
{
    /// <summary>
    /// Representa os dados de cadastro das preferências gerais de um morador
    /// </summary>
    public record ResidentGeneralPreferencesRegistrationViewModel
    {
        /// <summary>
        /// A identificação do morador que terá as suas preferências cadastradas
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public Guid ResidentId { get; init; }
        /// <summary>
        /// Indica se o morador deseja morar em uma casa que aceite animais
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public bool WantSpaceForAnimals { get; init; }
        /// <summary>
        /// Indica se o morador deseja morar em uma casa que aceite crianças
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public bool AcceptChildren { get; init; }
        /// <summary>
        /// Indica se o morador deseja morar em uma casa que aceite festas
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public bool WantsToParty { get; init; }
        /// <summary>
        /// Indica se o morador deseja morar em uma casa que aceite fumantes
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public bool AcceptSmokers { get; init; }
        /// <summary>
        /// Indica se o morador deseja morar em uma casa que aceite somente homens
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public bool AcceptsOnlyMenAsRoommates { get; init; }
        /// <summary>
        /// Indica se o morador deseja morar em uma casa que aceite somente mulheres
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public bool AcceptsOnlyWomenAsRoommates { get; init; }
        /// <summary>
        /// Indica a quantidade mínima de colegas de quarto que o morador deseja ter
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public int MinimumNumberOfRoommatesDesired { get; init; }
        /// <summary>
        /// Indica a quantidade máxima de colegas de quarto que o morador deseja ter
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public int MaximumNumberOfRoommatesDesired { get; init; }
    }
}