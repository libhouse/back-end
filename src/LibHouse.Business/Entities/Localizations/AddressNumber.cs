using System;

namespace LibHouse.Business.Entities.Localizations
{
    public record AddressNumber
    {
        public AddressNumber(ushort number)
        {
            if (number <= 0)
            {
                throw new ArgumentOutOfRangeException(paramName: nameof(number), "O número deve ser maior do que zero");
            }
            Number = number;
        }

        private ushort Number { get; init; }

        public ushort GetNumber()
        {
            return Number;
        }
    }
}