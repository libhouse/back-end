using System;

namespace LibHouse.Business.Entities.Shared
{
    public record MonetaryRange
    {
        public decimal MinimumValue { get; init; }
        public decimal MaximumValue { get; init; }

        public MonetaryRange(decimal minimumValue, decimal maximumValue)
        {
            if (minimumValue < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(minimumValue), minimumValue, "O valor mínimo deve ser positivo ou igual a zero");
            }
            if (maximumValue <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maximumValue), maximumValue, "O valor máximo deve ser positivo e maior do que zero");
            }
            if (minimumValue >= maximumValue)
            {
                throw new ArgumentOutOfRangeException(nameof(minimumValue), minimumValue, "O valor mínimo deve ser menor que o valor máximo");
            }
            MinimumValue = minimumValue;
            MaximumValue = maximumValue;
        }
    }
}