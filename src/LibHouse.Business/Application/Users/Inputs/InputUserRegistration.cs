using System;

namespace LibHouse.Business.Application.Users.Inputs
{
    public record InputUserRegistration
    {
        public string Name { get; init; }
        public string LastName { get; init; }
        public DateTime BirthDate { get; init; }
        public string Gender { get; init; }
        public string Phone { get; init; }
        public string Email { get; init; }
        public string CPF { get; init; }
        public string UserType { get; init; }
        public string Password { get; init; }

        public InputUserRegistration(
            string name,
            string lastName, 
            DateTime birthDate, 
            string gender,
            string phone, 
            string email, 
            string cpf, 
            string userType,
            string password)
        {
            Name = name;
            LastName = lastName;
            BirthDate = birthDate;
            Gender = gender;
            Phone = phone;
            Email = email;
            CPF = cpf;
            UserType = userType;
            Password = password;
        }
    }
}