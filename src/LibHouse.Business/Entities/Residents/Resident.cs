using LibHouse.Business.Entities.Users;
using System;

namespace LibHouse.Business.Entities.Residents
{
    public class Resident : User
    {
        public Resident(
            string name,
            string lastName,
            DateTime birthDate,
            Gender gender,
            string phone,
            string email,
            string cpf)
            : this(name, lastName, birthDate, gender, UserType.Resident)
        {
            CPF = Cpf.CreateFromDocument(cpf);
            Phone = Phone.CreateFromNumber(phone);
            Email = Email.CreateFromAddress(email);
        }

        private Resident(
            string name,
            string lastName,
            DateTime birthDate,
            Gender gender,
            UserType userType)
            : base(name, lastName, birthDate, gender, userType)
        {
        }

        public override string ToString() =>
            $"Resident {Id}: {Name} {LastName}";
    }
}