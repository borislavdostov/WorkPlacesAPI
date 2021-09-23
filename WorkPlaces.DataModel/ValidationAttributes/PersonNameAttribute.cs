using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace WorkPlaces.DataModel.ValidationAttributes
{
    public class PersonNameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var name = value as string;

            if (!Regex.IsMatch(name, @"[A-Z]{1}[a-z]*"))
            {
                return new ValidationResult(
                "The name should start with a capital letter and cannot contain numbers or any special symbols.");
            }

            return ValidationResult.Success;
        }
    }
}
