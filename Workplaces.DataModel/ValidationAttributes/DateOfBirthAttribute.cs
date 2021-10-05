using System;
using System.ComponentModel.DataAnnotations;

namespace Workplaces.DataModel.ValidationAttributes
{
    public class DateOfBirthAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dateOfBirth = Convert.ToDateTime(value);

            if (dateOfBirth > DateTime.Now)
            {
                return new ValidationResult(ValidationMessage.IncorrectDateOfBirth);
            }

            if (DateTime.Now.Year - dateOfBirth.Year < 18)
            {
                return new ValidationResult(ValidationMessage.UserNotOldEnough);
            }

            return ValidationResult.Success;
        }
    }
}
