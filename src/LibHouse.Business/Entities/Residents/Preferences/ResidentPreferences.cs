using LibHouse.Business.Entities.Residents.Preferences.Charges;
using LibHouse.Business.Entities.Residents.Preferences.General;
using LibHouse.Business.Entities.Residents.Preferences.Localizations;
using LibHouse.Business.Entities.Residents.Preferences.Rooms;
using LibHouse.Business.Entities.Residents.Preferences.Services;
using System;

namespace LibHouse.Business.Entities.Residents.Preferences
{
    public class ResidentPreferences
    {
        private Resident Resident { get; }
        private Guid ResidentId { get; }
        private RoomPreferences RoomPreferences { get; set; }
        private ServicesPreferences ServicesPreferences { get; set; }
        private ChargePreferences ChargePreferences { get; set; }
        private GeneralPreferences GeneralPreferences { get; set; }
        private LocalizationPreferences LocalizationPreferences { get; set; }

        public void AddRoomPreferences(RoomPreferences roomPreferences)
        {
            RoomPreferences = roomPreferences;
        }

        public RoomPreferences GetRoomPreferences()
        {
            return RoomPreferences;
        }

        public bool HaveRoomPreferences()
        {
            return RoomPreferences is not null;
        }

        public void AddServicesPreferences(ServicesPreferences servicesPreferences)
        {
            ServicesPreferences = servicesPreferences;
        }

        public ServicesPreferences GetServicesPreferences()
        {
            return ServicesPreferences;
        }

        public bool HaveServicesPreferences()
        {
            return ServicesPreferences is not null;
        }

        public void AddChargePreferences(ChargePreferences chargePreferences)
        {
            ChargePreferences = chargePreferences;
        }

        public ChargePreferences GetChargePreferences()
        {
            return ChargePreferences;
        }

        public bool HaveChargePreferences()
        {
            return ChargePreferences is not null;
        }

        public void AddGeneralPreferences(GeneralPreferences generalPreferences)
        {
            GeneralPreferences = generalPreferences;
        }

        public GeneralPreferences GetGeneralPreferences()
        {
            return GeneralPreferences;
        }

        public bool HaveGeneralPreferences()
        {
            return GeneralPreferences is not null;
        }

        public void AddLocalizationPreferences(LocalizationPreferences localizationPreferences)
        {
            LocalizationPreferences = localizationPreferences;
        }

        public LocalizationPreferences GetLocalizationPreferences()
        {
            return LocalizationPreferences;
        }

        public bool HaveLocalizationPreferences()
        {
            return LocalizationPreferences is not null;
        }

        public override string ToString() => $"Resident {ResidentId} preferences";
    }
}