using LibHouse.Business.Entities.Users;

namespace LibHouse.Business.Entities.Residents.Preferences.General
{
    public class GeneralPreferences
    {
        public AnimalPreferences AnimalPreferences { get; private set; }
        public ChildrenPreferences ChildrenPreferences { get; private set; }
        public PartyPreferences PartyPreferences { get; private set; }
        public RoommatePreferences RoommatePreferences { get; private set; }
        public SmokersPreferences SmokersPreferences { get; private set; }

        public void AddAnimalPreferences(bool wantSpaceForAnimals)
        {
            AnimalPreferences = new(wantSpaceForAnimals);
        }

        public bool HaveAnimalPreferences()
        {
            return AnimalPreferences is not null;
        }

        public void AddChildrenPreferences(bool acceptChildren)
        {
            ChildrenPreferences = new(acceptChildren);
        }

        public bool HaveChildrenPreferences()
        {
            return ChildrenPreferences is not null;
        }

        public void AddPartyPreferences(bool wantsToParty)
        {
            PartyPreferences = new(wantsToParty);
        }

        public bool HavePartyPreferences()
        {
            return PartyPreferences is not null;
        }

        public void AddRoommatePreferences(
            int minimumNumberOfRoommatesDesired,
            int maximumNumberOfRoommatesDesired,
            Gender[] acceptedGendersOfRoommates)
        {
            RoommatePreferences = new(
                minimumNumberOfRoommatesDesired,
                maximumNumberOfRoommatesDesired, 
                acceptedGendersOfRoommates);
        }

        public bool HaveRoommatePreferences()
        {
            return RoommatePreferences is not null;
        }

        public void AddSmokersPreferences(bool acceptSmokers)
        {
            SmokersPreferences = new(acceptSmokers);
        }

        public bool HaveSmokersPreferences()
        {
            return SmokersPreferences is not null;
        }
    }
}