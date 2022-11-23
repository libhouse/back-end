namespace LibHouse.Business.Entities.Residents.Preferences.General.Builders
{
    public interface IGeneralPreferencesBuilder
    {
        void WithAnimalPreferences(bool wantSpaceForAnimals);
        void WithChildrenPreferences(bool acceptChildren);
        void WithPartyPreferences(bool wantsToParty);
        void WithRoommatePreferences(bool acceptsOnlyMenAsRoommates, bool acceptsOnlyWomenAsRoommates, int minimumNumberOfRoommatesDesired, int maximumNumberOfRoommatesDesired);
        void WithSmokersPreferences(bool acceptSmokers);
        GeneralPreferences GetGeneralPreferences();
    }
}