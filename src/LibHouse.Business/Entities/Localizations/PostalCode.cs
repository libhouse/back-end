using System;
using System.Text.RegularExpressions;

namespace LibHouse.Business.Entities.Localizations
{
    public record PostalCode
    {
        public PostalCode(string postalCodeNumber)
        {
            if (string.IsNullOrEmpty(postalCodeNumber))
            {
                throw new ArgumentNullException(nameof(postalCodeNumber), "O número é obrigatório");
            }
            if (!Regex.IsMatch(postalCodeNumber, @"^[0-9]{5}[-]?[0-9]{3}$", RegexOptions.Compiled))
            {
                throw new FormatException("O número deve possuir oito dígitos, com os três últimos separados opcionalmente por traço");
            }
            PostalCodeNumber = postalCodeNumber.Replace("-", "");
        }

        private string PostalCodeNumber { get; init; }

        public string GetNumber()
        {
            return PostalCodeNumber;
        }
    }
}