namespace LibHouse.Business.Entities.Residents.Preferences.General
{
    public record PartyPreferences
    {
        public PartyPreferences(bool wantsToParty)
        {
            WantsToParty = wantsToParty;
        }

        public bool WantsToParty { get; }

        public bool RequiresAcceptanceOfParties()
        {
            return WantsToParty;
        }
    }
}