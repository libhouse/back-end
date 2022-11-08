using Ardalis.GuardClauses;
using System;

namespace LibHouse.Business.Entities.Users
{
    public record Cpf
    {
        private const short TotalNumberOfDigits = 11;

        public string Value { get; init; }

        private Cpf(string value)
        {
            Value = value;
        }

        public static Cpf CreateFromDocument(string document)
        {
            Guard.Against.NullOrWhiteSpace(document, nameof(document), "O documento é obrigatório.");
            bool isValidDocument = Validate(document);
            if (!isValidDocument)
            {
                throw new ArgumentException("O documento fornecido não é um CPF válido", nameof(document));
            }
            return new Cpf(document);
        }

        private static bool Validate(string document)
        {
            if (CalculateNumberOfDigits(document) != TotalNumberOfDigits)
            {
                return false;
            }
            int digitOneTotal = 0;
            int digitTwoTotal = 0;
            if (CheckIfAllDigitsAreIdentical(document))
            {
                return false;
            }
            for (int position = 0; position < 9; position++)
            {
                var digit = GetDigit(document, position);
                digitOneTotal += digit * (10 - position);
                digitTwoTotal += digit * (11 - position);
            }
            var remainderOfDigitOne = digitOneTotal % 11;
            if (remainderOfDigitOne < 2) 
            { 
                remainderOfDigitOne = 0; 
            }
            else 
            { 
                remainderOfDigitOne = 11 - remainderOfDigitOne; 
            }
            if (GetDigit(document, 9) != remainderOfDigitOne)
            {
                return false;
            }
            digitTwoTotal += remainderOfDigitOne * 2;
            var remainderOfDigitTwo = digitTwoTotal % 11;
            if (remainderOfDigitTwo < 2) 
            { 
                remainderOfDigitTwo = 0; 
            }
            else 
            { 
                remainderOfDigitTwo = 11 - remainderOfDigitTwo; 
            }
            if (GetDigit(document, 10) != remainderOfDigitTwo)
            {
                return false;
            }
            return true;
        }

        private static int CalculateNumberOfDigits(string document)
        {
            if (document == null)
            {
                return 0;
            }
            var result = 0;
            for (var i = 0; i < document.Length; i++)
            {
                if (char.IsDigit(document[i]))
                {
                    result++;
                }
            }
            return result;
        }

        private static bool CheckIfAllDigitsAreIdentical(string document)
        {
            var previous = -1;
            for (var i = 0; i < document.Length; i++)
            {
                if (char.IsDigit(document[i]))
                {
                    var digit = document[i] - '0';
                    if (previous == -1)
                    {
                        previous = digit;
                    }
                    else
                    {
                        if (previous != digit)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private static int GetDigit(string document, int position)
        {
            int count = 0;
            for (int i = 0; i < document.Length; i++)
            {
                if (char.IsDigit(document[i]))
                {
                    if (count == position)
                    {
                        return document[i] - '0';
                    }
                    count++;
                }
            }
            return 0;
        }

        public static implicit operator Cpf(string value)
            => new(value);

        public override string ToString() => Value;
    }
}