using System;

namespace LibHouse.Business.Entities.Localizations
{
    public record AddressComplement
    {
        public AddressComplement(string description)
        {
            if (string.IsNullOrEmpty(description))
            {
                throw new ArgumentNullException(nameof(description), "A descrição é obrigatória");
            }
            Description = description;
        }

        private string Description { get; init; }

        public string GetDescription()
        {
            return Description;
        }
    }
}