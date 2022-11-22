using System;

namespace LibHouse.Infrastructure.Controllers.ViewModels.Residents
{
    /// <summary>
    /// Representa os dados de cadastro das preferências de cobranças de um morador
    /// </summary>
    public record ResidentChargePreferencesRegistrationViewModel
    {
        /// <summary>
        /// A identificação do morador que terá as suas preferências cadastradas
        /// </summary>
        public Guid ResidentId { get; init; }
        /// <summary>
        /// Indica o valor mínimo que o morador estará disposto a pagar pelo aluguel de uma moradia
        /// </summary>
        public decimal MinimumRentalAmountDesired { get; init; }
        /// <summary>
        /// Indica o valor máximo que o morador estará disposto a pagar pelo aluguel de uma moradia
        /// </summary>
        public decimal MaximumRentalAmountDesired { get; init; }
        /// <summary>
        /// Indica o valor mínimo que o morador estará disposto a pagar pelas despesas de uma moradia
        /// </summary>
        public decimal MinimumExpenseAmountDesired { get; init; }
        /// <summary>
        /// Indica o valor máximo que o morador estará disposto a pagar pelas despesas de uma moradia
        /// </summary>
        public decimal MaximumExpenseAmountDesired { get; init; }
    }
}