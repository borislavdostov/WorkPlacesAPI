using System;
using System.ComponentModel.DataAnnotations;
using WorkPlaces.DataModel.Models;

namespace WorkPlaces.Common.ValidationAttributes
{
    public class DateOfBirthAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var user = (UserForManipulationDTO)validationContext.ObjectInstance;

            if (user.DateOfBirth > DateTime.Now)
            {
                return new ValidationResult(
                    "The date of birth must be lower than today's date.",
                    new[] { nameof(UserForManipulationDTO) });
            }

            return ValidationResult.Success;
        }
    }
}
