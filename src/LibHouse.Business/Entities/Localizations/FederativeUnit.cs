using System;
using System.Linq;

namespace LibHouse.Business.Entities.Localizations
{
    public record FederativeUnit
    {
        private const int ABBREVIATION_LENGTH_FEDERATIVE_UNIT = 2;

        public FederativeUnit(string abbreviationOfTheFederativeUnit)
        {
            if (string.IsNullOrEmpty(abbreviationOfTheFederativeUnit))
            {
                throw new ArgumentNullException(nameof(abbreviationOfTheFederativeUnit), "A sigla da unidade federativa é obrigatória");
            }
            if (abbreviationOfTheFederativeUnit.Length != ABBREVIATION_LENGTH_FEDERATIVE_UNIT)
            {
                throw new ArgumentException(paramName: nameof(abbreviationOfTheFederativeUnit), message: $"O comprimento da sigla deve ser igual a {ABBREVIATION_LENGTH_FEDERATIVE_UNIT}");
            }
            if (!abbreviationOfTheFederativeUnit.All(char.IsLetter))
            {
                throw new FormatException("A sigla da unidade federativa deve possuir apenas letras");
            }
            AbbreviationOfTheFederativeUnit = abbreviationOfTheFederativeUnit.ToUpper();
        }

        private string AbbreviationOfTheFederativeUnit { get; init; }

        public string GetAbbreviation()
        {
            return AbbreviationOfTheFederativeUnit;
        }
    }
}