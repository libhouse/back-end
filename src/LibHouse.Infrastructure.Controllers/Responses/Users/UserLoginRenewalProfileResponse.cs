using System;

namespace LibHouse.Infrastructure.Controllers.Responses.Users
{
    /// <summary>
    /// Representa os dados de um usuário autenticado
    /// </summary>
    public record UserLoginRenewalProfileResponse
    {
        /// <summary>
        /// O identificador único do usuário
        /// </summary>
        public Guid Id { get; init; }

        /// <summary>
        /// O nome completo do usuário
        /// </summary>
        public string FullName { get; init; }

        /// <summary>
        /// A data de nascimento do usuário
        /// </summary>
        public DateTime BirthDate { get; init; }

        /// <summary>
        /// O gênero do usuário
        /// </summary>
        public string Gender { get; init; }

        /// <summary>
        /// O email do usuário
        /// </summary>
        public string Email { get; init; }

        /// <summary>
        /// O tipo de cadastro do usuário
        /// </summary>
        public string UserType { get; init; }

        public UserLoginRenewalProfileResponse(
            Guid id,
            string fullName,
            DateTime birthDate,
            string gender,
            string email,
            string userType)
        {
            Id = id;
            FullName = fullName;
            BirthDate = birthDate;
            Gender = gender;
            Email = email;
            UserType = userType;
        }
    }
}