using System.ComponentModel.DataAnnotations;

namespace LibHouse.Infrastructure.Controllers.ViewModels.Users
{
    /// <summary>
    /// Objeto que possui os dados necessários para o login de um usuário.
    /// </summary>
    public record UserLoginViewModel
    {
        /// <summary>
        /// O endereço de e-mail do usuário cadastrado.
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(60, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 5)]
        [EmailAddress(ErrorMessage = "O campo {0} está em um formato inválido.")]
        public string Email { get; init; }

        /// <summary>
        /// A senha do usuário cadastrado. 
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(30, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 10)]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{10,30}$", ErrorMessage = "A senha deve ter pelo menos uma letra maiúscula, uma letra minúscula, um número e um caracter especial.")]
        public string Password { get; init; }
    }
}