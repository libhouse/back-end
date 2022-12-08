using LibHouse.Business.Entities.Localizations;
using System;
using System.ComponentModel.DataAnnotations;

namespace LibHouse.Infrastructure.Controllers.Attributes.Shared
{
    public class PostalCodeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(
            object value,
            ValidationContext validationContext)
        {
            if (value is not string)
            {
                return new ValidationResult("O código deve ser uma cadeia de caracteres.");
            }
            try
            {
                var postalCodeNumber = value as string;
                _ = new PostalCode(postalCodeNumber);
                return ValidationResult.Success;
            }
            catch (Exception ex)
            {
                return new ValidationResult(ex.Message);
            }
        }
    }
}