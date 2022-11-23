using LibHouse.Business.Entities.Residents.Preferences;
using LibHouse.Business.Entities.Residents.Preferences.Charges;
using LibHouse.Business.Entities.Residents.Preferences.General;
using LibHouse.Business.Entities.Residents.Preferences.Rooms;
using LibHouse.Business.Entities.Residents.Preferences.Services;
using LibHouse.Business.Entities.Users;
using System;

namespace LibHouse.Business.Entities.Residents
{
    public class Resident : User
    {
        private ResidentPreferences ResidentPreferences { get; set; }

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

        public void WithPreferences()
        {
            if (ResidentPreferences is null)
            {
                ResidentPreferences = new();
            }
        }

        public void AddRoomPreferences(RoomPreferences roomPreferences)
        {
            ResidentPreferences.AddRoomPreferences(roomPreferences);
        }

        public RoomPreferences GetRoomPreferences()
        {
            return ResidentPreferences.GetRoomPreferences();
        }

        public bool HaveRoomPreferences()
        {
            return ResidentPreferences is not null && ResidentPreferences.HaveRoomPreferences();
        }

        public void AddServicesPreferences(ServicesPreferences servicesPreferences)
        {
            ResidentPreferences.AddServicesPreferences(servicesPreferences);
        }

        public ServicesPreferences GetServicesPreferences()
        {
            return ResidentPreferences.GetServicesPreferences();
        }

        public bool HaveServicesPreferences()
        {
            return ResidentPreferences is not null && ResidentPreferences.HaveServicesPreferences();
        }

        public void AddChargePreferences(ChargePreferences chargePreferences)
        {
            ResidentPreferences.AddChargePreferences(chargePreferences);
        }

        public ChargePreferences GetChargePreferences()
        {
            return ResidentPreferences.GetChargePreferences();
        }

        public bool HaveChargePreferences()
        {
            return ResidentPreferences is not null && ResidentPreferences.HaveChargePreferences();
        }

        public void AddGeneralPreferences(GeneralPreferences generalPreferences)
        {
            ResidentPreferences.AddGeneralPreferences(generalPreferences);
        }

        public bool HaveGeneralPreferences()
        {
            return ResidentPreferences is not null && ResidentPreferences.HaveGeneralPreferences();
        }

        public override string ToString() => $"Resident {Id}: {Name} {LastName}";
    }
}