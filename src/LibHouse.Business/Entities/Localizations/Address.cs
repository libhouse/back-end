using LibHouse.Business.Entities.Shared;
using System;
using System.Text.RegularExpressions;

namespace LibHouse.Business.Entities.Localizations
{
    public class Address : Entity
    {
        private const int MAXIMUM_LENGTH_ALLOWED_NAME = 60;

        public Address(
            string name, 
            ushort number,
            string streetNeighborhood, 
            string streetCity,
            string abbreviationOfCityFederativeUnit,
            string postalCode,
            string complement = null)
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
            AddressNumber = new(number);
            AddressComplement = !string.IsNullOrEmpty(complement) ? new(complement) : null;
            Neighborhood = new(streetNeighborhood, streetCity, abbreviationOfCityFederativeUnit);
            PostalCode = new(postalCode);
        }

        private Address() { }

        private string Name { get; init; }
        private AddressNumber AddressNumber { get; init; }
        private AddressComplement AddressComplement { get; init; }
        private Neighborhood Neighborhood { get; init; }
        private PostalCode PostalCode { get; init; }

        public string GetName()
        {
            return Name;
        }

        public ushort GetNumber()
        {
            return AddressNumber.GetNumber();
        }

        public string GetComplement()
        {
            return AddressComplement?.GetDescription();
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

        public string GetPostalCodeNumber()
        {
            return PostalCode.GetNumber();
        }
    }
}