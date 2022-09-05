using System.ComponentModel.DataAnnotations;

namespace LibHouse.Infrastructure.Controllers.ViewModels.Users
{
    /// <summary>
    /// Objeto que possui os dados necessários para obter um novo token de acesso
    /// </summary>
    public record UserLoginRenewalViewModel
    {
        /// <summary>
        /// O endereço de e-mail do usuário cadastrado
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(60, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 5)]
        [EmailAddress(ErrorMessage = "O campo {0} está em um formato inválido.")]
        public string Email { get; init; }

        /// <summary>
        /// O access token expirado que será renovado
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string AccessToken { get; init; }

        /// <summary>
        /// O refresh token que será utilizado para renovar o access token
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string RefreshToken { get; init; }
    }
}