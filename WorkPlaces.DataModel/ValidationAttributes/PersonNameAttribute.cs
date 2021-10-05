using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Workplaces.DataModel.ValidationAttributes
{
    public class PersonNameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var name = value as string;

            if (!Regex.IsMatch(name, @"^[A-Z][A-Za-z\'\s]*$"))
            {
                return new ValidationResult(ValidationMessage.IncorrectName);
            }

            return ValidationResult.Success;
        }
    }
}
