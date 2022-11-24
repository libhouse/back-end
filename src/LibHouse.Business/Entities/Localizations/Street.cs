using System;
using System.Text.RegularExpressions;

namespace LibHouse.Business.Entities.Localizations
{
    public record Street
    {
        private const int MAXIMUM_LENGTH_ALLOWED_NAME = 60;

        public Street(
            string name, 
            string streetNeighborhood, 
            string streetCity,
            string abbreviationOfCityFederativeUnit)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name), "O nome do logradouro é obrigatório");
            }
            if (name.Length > MAXIMUM_LENGTH_ALLOWED_NAME)
            {
                throw new ArgumentException(paramName: nameof(name), message: $"O comprimento do nome não pode ultrapassar {MAXIMUM_LENGTH_ALLOWED_NAME} caracteres");
            }
            if (!Regex.IsMatch(name, @"^[\p{L}\p{M}\s]+$", RegexOptions.Compiled))
            {
                throw new FormatException("O nome do logradouro deve possuir apenas letras");
            }
            Name = name;
            Neighborhood = new(streetNeighborhood, streetCity, abbreviationOfCityFederativeUnit);
        }

        private string Name { get; init; }
        private Neighborhood Neighborhood { get; init; }

        public string GetName()
        {
            return Name;
        }

        public string GetNeighborhoodName()
        {
            return Neighborhood.GetName();
        }

        public string GetAbbreviationOfTheFederativeUnit()
        {
            return Neighborhood.GetAbbreviationOfTheFederativeUnit();
        }

        public string GetCityName()
        {
            return Neighborhood.GetCityName();
        }
    }
}