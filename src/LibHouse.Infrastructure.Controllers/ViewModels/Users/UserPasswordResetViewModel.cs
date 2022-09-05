using LibHouse.Infrastructure.Controllers.Attributes.Shared;
using System.ComponentModel.DataAnnotations;

namespace LibHouse.Infrastructure.Controllers.ViewModels.Users
{
    /// <summary>
    /// Representa os dados necessários para que um usuário solicite a redefinição da sua senha
    /// </summary>
    public record UserPasswordResetViewModel
    {
        /// <summary>
        /// O número de cpf do usuário cadastrado.
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(11, ErrorMessage = "O campo {0} deve ter {1} caracteres.", MinimumLength = 11)]
        [Cpf(ErrorMessage = "O campo {0} está em um formato inválido.")]
        public string Cpf { get; init; }
    }
}