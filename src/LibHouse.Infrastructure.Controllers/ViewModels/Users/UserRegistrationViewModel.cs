using LibHouse.Business.Entities.Users;
using LibHouse.Infrastructure.Controllers.Attributes.Shared;
using System;
using System.ComponentModel.DataAnnotations;

namespace LibHouse.Infrastructure.Controllers.ViewModels.Users
{
    /// <summary>
    /// Objeto que possui os dados necessários para o cadastro de um usuário.
    /// </summary>
    public record UserRegistrationViewModel
    {
        /// <summary>
        /// O nome do usuário cadastrado. Deve possuir no mínimo três, e no máximo quarenta caracteres.
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(40, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 3)]
        public string Name { get; init; }

        /// <summary>
        /// O sobrenome do usuário cadastrado. Deve possuir no mínimo três, e no máximo quarenta caracteres.
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(40, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 3)]
        public string LastName { get; init; }

        /// <summary>
        /// A data de nascimento do usuário cadastrado.
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public DateTime BirthDate { get; init; }

        /// <summary>
        /// O gênero do usuário cadastrado. O valor 0 representa masculino, o valor 1 representa feminino, o valor 2 representa outros, e o valor 3 representa não declarado.
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [EnumDataType(typeof(Gender), ErrorMessage = "O campo deve ter um dos seguintes valores: 0 (Masculino), 1 (Feminino), 2 (Outros) ou 3 (Não Declarado).")]
        public Gender Gender { get; init; }

        /// <summary>
        /// O número de telefone do usuário cadastrado. Deve possuir no mínimo dez, e no máximo onze caracteres.
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(11, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 10)]
        [Phone(ErrorMessage = "O campo {0} está em um formato inválido.")]
        public string Phone { get; init; }

        /// <summary>
        /// O endereço de e-mail do usuário cadastrado. Deve possuir no mínimo cinco, e no máximo sessenta caracteres.
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(60, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 5)]
        [EmailAddress(ErrorMessage = "O campo {0} está em um formato inválido.")]
        public string Email { get; init; }

        /// <summary>
        /// O número de cpf do usuário cadastrado.
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(11, ErrorMessage = "O campo {0} deve ter {1} caracteres.", MinimumLength = 11)]
        [Cpf(ErrorMessage = "O campo {0} está em um formato inválido.")]
        public string Cpf { get; init; }

        /// <summary>
        /// O tipo do usuário cadastrado. O valor 0 representa morador, enquanto o valor 1 representa proprietário.
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [EnumDataType(typeof(UserType), ErrorMessage = "O campo deve ter um dos seguintes valores: 0 (Morador) ou 1 (Proprietário).")]
        public UserType UserType { get; init; }

        /// <summary>
        /// A senha do usuário cadastrado. A senha deve ter pelo menos uma letra maiúscula, uma letra minúscula, um número e um caracter especial. Mínimo de dez, e máximo de trinta caracteres.
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(30, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 10)]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{10,30}$", ErrorMessage = "A senha deve ter pelo menos uma letra maiúscula, uma letra minúscula, um número e um caracter especial.")]
        public string Password { get; init; }

        /// <summary>
        /// A confirmação da senha do usuário cadastrado.
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Compare("Password", ErrorMessage = "As senhas são diferentes.")]
        public string ConfirmPassword { get; init; }
    }
}