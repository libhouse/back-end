using LibHouse.Business.Entities.Users;
using System;
using System.ComponentModel.DataAnnotations;

namespace LibHouse.Infrastructure.Controllers.Attributes.Shared
{
    public class CpfAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(
            object value,
            ValidationContext validationContext)
        {
            if (value is not string)
            {
                return new ValidationResult("O documento deve ser uma cadeia de caracteres.");
            }

            try
            {
                var cpfDocumentNumber = value as string;

                _ = Cpf.CreateFromDocument(cpfDocumentNumber);

                return ValidationResult.Success;
            }
            catch (Exception ex)
            {
                return new ValidationResult(ex.Message);
            }
        }
    }
}