using System;

namespace LibHouse.Business.Entities.Shared
{
    public record Range
    {
        public int InitialValue { get; init; }
        public int LastValue { get; init; }

        public Range(int initialValue, int lastValue)
        {
            if (initialValue >= lastValue)
            {
                throw new ArgumentOutOfRangeException(nameof(initialValue), initialValue, "O valor mínimo deve ser menor que o valor máximo");
            }
            InitialValue = initialValue;
            LastValue = lastValue;
        }
    }
}