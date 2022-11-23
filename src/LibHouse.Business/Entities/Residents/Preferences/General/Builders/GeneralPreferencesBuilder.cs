using LibHouse.Business.Entities.Users;
using System.Collections.Generic;
using System.Linq;

namespace LibHouse.Business.Entities.Residents.Preferences.General.Builders
{
    public class GeneralPreferencesBuilder : IGeneralPreferencesBuilder
    {
        private readonly GeneralPreferences _generalPreferences;

        public GeneralPreferencesBuilder()
        {
            _generalPreferences = new();
        }

        public GeneralPreferences GetGeneralPreferences()
        {
            return _generalPreferences;
        }

        public void WithAnimalPreferences(bool wantSpaceForAnimals)
        {
            _generalPreferences.AddAnimalPreferences(wantSpaceForAnimals);
        }

        public void WithChildrenPreferences(bool acceptChildren)
        {
            _generalPreferences.AddChildrenPreferences(acceptChildren);
        }

        public void WithPartyPreferences(bool wantsToParty)
        {
            _generalPreferences.AddPartyPreferences(wantsToParty);
        }

        public void WithRoommatePreferences(
            bool acceptsOnlyMenAsRoommates, 
            bool acceptsOnlyWomenAsRoommates, 
            int minimumNumberOfRoommatesDesired, 
            int maximumNumberOfRoommatesDesired)
        {
            IList<Gender> acceptedGendersOfRoommates = new List<Gender>();
            if (!acceptsOnlyMenAsRoommates && !acceptsOnlyWomenAsRoommates)
            {
                acceptedGendersOfRoommates.Add(Gender.Male);
                acceptedGendersOfRoommates.Add(Gender.Female);
            }
            else if (!acceptsOnlyMenAsRoommates && acceptsOnlyWomenAsRoommates)
            {
                acceptedGendersOfRoommates.Add(Gender.Female);
            }
            else
            {
                acceptedGendersOfRoommates.Add(Gender.Male);
            }
            _generalPreferences.AddRoommatePreferences(
                minimumNumberOfRoommatesDesired, 
                maximumNumberOfRoommatesDesired, 
                acceptedGendersOfRoommates.ToArray());
        }

        public void WithSmokersPreferences(bool acceptSmokers)
        {
            _generalPreferences.AddSmokersPreferences(acceptSmokers);
        }
    }
}