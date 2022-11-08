using Ardalis.GuardClauses;
using LibHouse.Business.Entities.Shared;
using System;

namespace LibHouse.Business.Entities.Users
{
    public abstract class User : Entity
    {
        public string Name { get; protected set; }
        public string LastName { get; protected set; }
        public DateTime BirthDate { get; protected set; }
        public Gender Gender { get; protected set; }
        public Phone Phone { get; protected set; }
        public Email Email { get; protected set; }
        public Cpf CPF { get; protected set; }
        public UserType UserType { get; protected set; }

        protected User(
            string name, 
            string lastName, 
            DateTime birthDate, 
            Gender gender, 
            Phone phone, 
            Email email, 
            Cpf cpf, 
            UserType userType)
            : this(name, lastName, birthDate, gender, userType)
        {
            CPF = cpf;
            Phone = phone;
            Email = email;
        }

        protected User(
            string name,
            string lastName,
            DateTime birthDate,
            Gender gender,
            UserType userType)
        {
            Guard.Against.NullOrEmpty(name, nameof(name), "O nome do usuário é obrigatório");
            Guard.Against.NullOrEmpty(lastName, nameof(lastName), "O sobrenome do usuário é obrigatório");
            Name = name;
            LastName = lastName;
            BirthDate = birthDate;
            Gender = gender;
            UserType = userType;
        }

        public string GetPhoneNumber() => Phone.Value;

        public string GetEmailAddress() => Email.Value;

        public string GetCpfNumber() => CPF.Value;

        public override string ToString() =>
            $"User {Id}: {Name} {LastName}";
    }
}