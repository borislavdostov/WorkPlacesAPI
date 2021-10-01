using System;
using System.ComponentModel.DataAnnotations;
using Workplaces.DataModel.Models;

namespace Workplaces.DataModel.ValidationAttributes
{
    public class DateOfBirthAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //var user = (UserForManipulationDTO)validationContext.ObjectInstance;
            var dateOfBirth = Convert.ToDateTime(value);

            if (dateOfBirth > DateTime.Now)
            {
                return new ValidationResult("Date of birth should be lower than today's date.");
            }

            if (DateTime.Now.Year - dateOfBirth.Year < 18)
            {
                return new ValidationResult("User is not old enough to work.");
            }

            return ValidationResult.Success;
        }
    }
}
