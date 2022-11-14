﻿using LibHouse.Business.Entities.Residents.Preferences;
using LibHouse.Business.Entities.Residents.Preferences.Rooms;
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

        public override string ToString() => $"Resident {Id}: {Name} {LastName}";
    }
}