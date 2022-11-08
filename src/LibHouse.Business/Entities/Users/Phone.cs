using Ardalis.GuardClauses;
using System;
using System.Text.RegularExpressions;

namespace LibHouse.Business.Entities.Users
{
    public record Phone
    {
        private const short NumberOfDigitsHomeTelephone = 10;
        private const short NumberOfDigitsCellPhone = 11;

        public string Value { get; init; }

        private Phone(string value)
        {
            Value = value;
        }

        public static Phone CreateFromNumber(string number)
        {
            Guard.Against.NullOrWhiteSpace(number, nameof(number), "O número é obrigatório.");
            bool isValidPhone = Validate(number);
            if (!isValidPhone)
            {
                throw new ArgumentException("O número fornecido não é um telefone válido", nameof(number));
            }
            return new Phone(number);
        }

        private static bool Validate(string number)
        {
            string sanitizedNumber = Regex.Replace(number, @"[^\d]", "", RegexOptions.Compiled);
            return sanitizedNumber.Length >= NumberOfDigitsHomeTelephone 
                && sanitizedNumber.Length <= NumberOfDigitsCellPhone;
        }

        public override string ToString() => Value;
    }
}