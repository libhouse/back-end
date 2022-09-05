using System.ComponentModel.DataAnnotations;

namespace LibHouse.Infrastructure.Controllers.ViewModels.Users
{
    /// <summary>
    /// Objeto que possui os dados necessários para realizar a redefinição de senha de um usuário.
    /// </summary>
    public record ConfirmUserPasswordResetViewModel
    {
        /// <summary>
        /// O token de redefinição de senha recebido no e-mail do usuário.
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string PasswordResetToken { get; init; }

        /// <summary>
        /// O endereço de e-mail utilizado no registro do usuário.
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(60, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 5)]
        [EmailAddress(ErrorMessage = "O campo {0} está em um formato inválido.")]
        public string UserEmail { get; init; }

        /// <summary>
        /// A nova senha do usuário cadastrado. A senha deve ter pelo menos uma letra maiúscula, uma letra minúscula, um número e um caracter especial. Mínimo de dez, e máximo de trinta caracteres.
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(30, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 10)]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{10,30}$", ErrorMessage = "A senha deve ter pelo menos uma letra maiúscula, uma letra minúscula, um número e um caracter especial.")]
        public string Password { get; init; }

        /// <summary>
        /// A confirmação da nova senha do usuário cadastrado.
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Compare("Password", ErrorMessage = "As senhas são diferentes.")]
        public string ConfirmPassword { get; init; }
    }
}