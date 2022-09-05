using LibHouse.Business.Entities.Users;
using System;

namespace LibHouse.Business.Entities.Owners
{
    public class Owner : User
    {
        public Owner(
            string name, 
            string lastName, 
            DateTime birthDate,
            Gender gender, 
            string phone, 
            string email, 
            string cpf) 
            : this(name, lastName, birthDate, gender, UserType.Owner)
        {
            CPF = Cpf.CreateFromDocument(cpf);
            Phone = Phone.CreateFromNumber(phone);
            Email = Email.CreateFromAddress(email);
        }

        private Owner(
            string name,
            string lastName,
            DateTime birthDate,
            Gender gender,
            UserType userType)
            : base(name, lastName, birthDate, gender, userType)
        {
        }

        public override string ToString() =>
            $"Owner {Id}: {Name} {LastName}";
    }
}