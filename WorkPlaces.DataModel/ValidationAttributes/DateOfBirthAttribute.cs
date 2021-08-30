using System;
using System.ComponentModel.DataAnnotations;
using WorkPlaces.DataModel.Models;

namespace WorkPlaces.Common.ValidationAttributes
{
    public class DateOfBirthAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var user = (UserForCreationDTO)validationContext.ObjectInstance;

            if (user.DateOfBirth > DateTime.Now)
            {
                return new ValidationResult(
                    "Date of birth cannot be greater than now.",
                    new[] { nameof(UserForCreationDTO) });
            }

            return ValidationResult.Success;
        }
    }
}
