using System;
using System.ComponentModel.DataAnnotations;

namespace LibHouse.Infrastructure.Controllers.ViewModels.Users
{
    /// <summary>
    /// Objeto que possui os dados necessários para realizar a confirmação do registro de um usuário.
    /// </summary>
    public record ConfirmUserRegistrationViewModel
    {
        /// <summary>
        /// O token de confirmação recebido no e-mail do usuário.
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string ConfirmationToken { get; init; }

        /// <summary>
        /// O endereço de e-mail utilizado no registro do usuário.
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(60, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 5)]
        [EmailAddress(ErrorMessage = "O campo {0} está em um formato inválido.")]
        public string UserEmail { get; init; }

        /// <summary>
        /// O identificador único do usuário retornado junto com o token de confirmação.
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public Guid UserId { get; init; }
    }
}