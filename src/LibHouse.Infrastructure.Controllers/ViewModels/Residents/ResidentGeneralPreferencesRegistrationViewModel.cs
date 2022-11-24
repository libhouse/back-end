using System;

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
        public Guid ResidentId { get; init; }
        /// <summary>
        /// Indica se o morador deseja morar em uma casa que aceite animais
        /// </summary>
        public bool WantSpaceForAnimals { get; init; }
        /// <summary>
        /// Indica se o morador deseja morar em uma casa que aceite crianças
        /// </summary>
        public bool AcceptChildren { get; init; }
        /// <summary>
        /// Indica se o morador deseja morar em uma casa que aceite festas
        /// </summary>
        public bool WantsToParty { get; init; }
        /// <summary>
        /// Indica se o morador deseja morar em uma casa que aceite fumantes
        /// </summary>
        public bool AcceptSmokers { get; init; }
        /// <summary>
        /// Indica se o morador deseja morar em uma casa que aceite somente homens
        /// </summary>
        public bool AcceptsOnlyMenAsRoommates { get; init; }
        /// <summary>
        /// Indica se o morador deseja morar em uma casa que aceite somente mulheres
        /// </summary>
        public bool AcceptsOnlyWomenAsRoommates { get; init; }
        /// <summary>
        /// Indica a quantidade mínima de colegas de quarto que o morador deseja ter
        /// </summary>
        public int MinimumNumberOfRoommatesDesired { get; init; }
        /// <summary>
        /// Indica a quantidade máxima de colegas de quarto que o morador deseja ter
        /// </summary>
        public int MaximumNumberOfRoommatesDesired { get; init; }
    }
}