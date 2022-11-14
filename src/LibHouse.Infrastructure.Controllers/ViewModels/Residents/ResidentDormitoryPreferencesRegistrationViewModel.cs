namespace LibHouse.Infrastructure.Controllers.ViewModels.Residents
{
    /// <summary>
    /// Representa os dados de cadastro das preferências de dormitório de um morador
    /// </summary>
    public record ResidentDormitoryPreferencesRegistrationViewModel
    {
        /// <summary>
        /// Indica se um morador deseja dormir em um quarto individual
        /// </summary>
        public bool WantSingleDormitory { get; init; }
        /// <summary>
        /// Indica se um morador deseja dormir em um quarto mobiliado
        /// </summary>
        public bool WantFurnishedDormitory { get; init; }
    }
}