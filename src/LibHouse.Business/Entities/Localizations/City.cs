using System;
using System.Text.RegularExpressions;

namespace LibHouse.Business.Entities.Localizations
{
    public record City
    {
        private const int MAXIMUM_LENGTH_ALLOWED_NAME = 30;

        public City(string name, string abbreviationOfCityFederativeUnit)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name), "O nome da cidade é obrigatório");
            }
            if (name.Length > MAXIMUM_LENGTH_ALLOWED_NAME)
            {
                throw new ArgumentException(paramName: nameof(name), message: $"O comprimento do nome não pode ultrapassar {MAXIMUM_LENGTH_ALLOWED_NAME} caracteres");
            }
            if (!Regex.IsMatch(name, @"^[\p{L}\p{M}\s]+$", RegexOptions.Compiled))
            {
                throw new FormatException("O nome da cidade deve possuir apenas letras");
            }
            Name = name;
            FederativeUnit = new(abbreviationOfCityFederativeUnit);
        }

        private string Name { get; init; }
        private FederativeUnit FederativeUnit { get; init; }

        public string GetName()
        {
            return Name;
        }

        public string GetAbbreviationOfTheFederativeUnit()
        {
            return FederativeUnit.GetAbbreviation();
        }
    }
}