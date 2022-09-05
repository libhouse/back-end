using Ardalis.GuardClauses;
using System;
using System.Text.RegularExpressions;

namespace LibHouse.Business.Entities.Users
{
    public record Email
    {
        public string Value { get; init; }

        private Email(string value)
        {
            Value = value;
        }

        public static Email CreateFromAddress(string address)
        {
            Guard.Against.NullOrWhiteSpace(address, nameof(address), "O endereço é obrigatório.");

            bool isValidAddress = Validate(address);

            if (!isValidAddress)
            {
                throw new ArgumentException("O endereço fornecido não é um e-mail válido", nameof(address));
            }

            return new Email(address);
        }

        private static bool Validate(string address)
        {
            return Regex.IsMatch(
                address,
                @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", 
                RegexOptions.IgnoreCase | RegexOptions.Compiled
            );
        }

        public override string ToString() => Value;
    }
}