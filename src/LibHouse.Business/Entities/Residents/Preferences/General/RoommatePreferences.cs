using LibHouse.Business.Entities.Shared;
using LibHouse.Business.Entities.Users;
using System.Linq;

namespace LibHouse.Business.Entities.Residents.Preferences.General
{
    public record RoommatePreferences
    {
        public Range AcceptedRangeOfRoommates { get; init; }
        public bool AcceptsOnlyMaleRoommates { get; init; }
        public bool AcceptsOnlyFemaleRoommates { get; init; }
        public bool AcceptsRoommatesOfAllGenders { get; init; }

        public RoommatePreferences(
            int minimumNumberOfRoommatesDesired,
            int maximumNumberOfRoommatesDesired,
            Gender[] acceptedGendersOfRoommates)
        {
            if (minimumNumberOfRoommatesDesired <= 0)
            {
                throw new System.ArgumentOutOfRangeException(nameof(minimumNumberOfRoommatesDesired), minimumNumberOfRoommatesDesired, "O número mínimo de colegas de quarto deve ser maior do que zero");
            }
            if (maximumNumberOfRoommatesDesired <= 0)
            {
                throw new System.ArgumentOutOfRangeException(nameof(maximumNumberOfRoommatesDesired), maximumNumberOfRoommatesDesired, "O número máximo de colegas de quarto deve ser maior do que zero");
            }
            if (!acceptedGendersOfRoommates.Any())
            {
                throw new System.ArgumentException("É obrigatório informar pelo menos um gênero aceito para os colegas de quarto", nameof(acceptedGendersOfRoommates));
            }
            AcceptedRangeOfRoommates = new(minimumNumberOfRoommatesDesired, maximumNumberOfRoommatesDesired);
            AcceptsOnlyMaleRoommates = acceptedGendersOfRoommates.All(g => g == Gender.Male);
            AcceptsOnlyFemaleRoommates = acceptedGendersOfRoommates.All(g => g == Gender.Female);
            AcceptsRoommatesOfAllGenders = !AcceptsOnlyMaleRoommates && !AcceptsOnlyFemaleRoommates;
        }

        private RoommatePreferences() { }

        public int GetMaximumNumberOfRoommatesDesired()
        {
            return AcceptedRangeOfRoommates.LastValue;
        }

        public int GetMinimumNumberOfRoommatesDesired()
        {
            return AcceptedRangeOfRoommates.InitialValue;
        }
    }
}