namespace LibHouse.Infrastructure.Controllers.ViewModels.Residents
{
    /// <summary>
    /// Representa os dados de cadastro das preferências de cozinha de um morador
    /// </summary>
    public record ResidentKitchenPreferencesRegistrationViewModel
    {
        /// <summary>
        /// Indica se um morador precisa morar em uma casa com fogão
        /// </summary>
        public bool WantStove { get; init; }
        /// <summary>
        /// Indica se um morador precisa morar em uma casa com microondas
        /// </summary>
        public bool WantMicrowave { get; init; }
        /// <summary>
        /// Indica se um morador precisa morar em uma casa com geladeira
        /// </summary>
        public bool WantRefrigerator { get; init; }
    }
}