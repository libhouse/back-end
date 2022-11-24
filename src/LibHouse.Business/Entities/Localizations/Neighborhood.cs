using System;
using System.Text.RegularExpressions;

namespace LibHouse.Business.Entities.Localizations
{
    public record Neighborhood
    {
        private const int MAXIMUM_LENGTH_ALLOWED_NAME = 60;

        public Neighborhood(string name, string neighborhoodCity, string abbreviationOfCityFederativeUnit)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name), "O nome do bairro é obrigatório");
            }
            if (name.Length > MAXIMUM_LENGTH_ALLOWED_NAME)
            {
                throw new ArgumentException(paramName: nameof(name), message: $"O comprimento do nome não pode ultrapassar {MAXIMUM_LENGTH_ALLOWED_NAME} caracteres");
            }
            if (!Regex.IsMatch(name, @"^[\p{L}\p{M}\s]+$", RegexOptions.Compiled))
            {
                throw new FormatException("O nome do bairro deve possuir apenas letras");
            }
            Name = name;
            City = new(neighborhoodCity, abbreviationOfCityFederativeUnit);
        }

        private string Name { get; init; }
        private City City { get; init; }

        public string GetName()
        {
            return Name;
        }

        public string GetCityName()
        {
            return City.GetName();
        }

        public string GetAbbreviationOfTheFederativeUnit()
        {
            return City.GetAbbreviationOfTheFederativeUnit();
        }
    }
}