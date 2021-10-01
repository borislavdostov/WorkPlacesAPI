using NUnit.Framework;
using System;
using System.ComponentModel.DataAnnotations;
using Workplaces.DataModel.Models;
using Workplaces.DataModel.ValidationAttributes;

namespace Workplaces.DataModel.Tests
{
    [TestFixture]
    public class DateOfBirthAttributeTests
    {
        DateOfBirthAttribute dateOfBirthAttribute;

        [SetUp]
        public void Initialize()
        {
            dateOfBirthAttribute = new DateOfBirthAttribute();
        }

        [Test]
        public void IsValidMethod_WithCorrectDateOfBirth_ShouldReturnTrue()
        {
            var dateOfBirth = new DateTime(1995, 5, 23);

            var validationResult = dateOfBirthAttribute.GetValidationResult(dateOfBirth, new ValidationContext(dateOfBirth));
            var actualResult = validationResult == ValidationResult.Success;

            Assert.IsTrue(actualResult);
        }

        [Test]
        public void IsValidMethod_WithBiggerDateThanToday_ShouldReturnFalse()
        {
            var validationResult = dateOfBirthAttribute.GetValidationResult(user, new ValidationContext(user));
            var actualResult = validationResult == ValidationResult.Success;

            Assert.IsFalse(actualResult);
        }

        [Test]
        public void IsValidMethod_WithDateLessThanEighteenYears_ShouldReturnFalse()
        {
            var user = new UserForManipulationDTO { DateOfBirth = DateTime.Now.AddYears(-1) };

            var validationResult = dateOfBirthAttribute.GetValidationResult(user, new ValidationContext(user));
            var actualResult = validationResult == ValidationResult.Success;

            Assert.IsFalse(actualResult);
        }

        [Test]
        public void IsValidMethod_WithBiggerDateThanToday_ShouldReturnErrorMessage()
        {
            var user = new UserForManipulationDTO { DateOfBirth = DateTime.Now.AddDays(1) };

            var validationResult = dateOfBirthAttribute.GetValidationResult(user, new ValidationContext(user));
            var actualResult = validationResult.ErrorMessage;

            Assert.AreEqual("Date of birth should be lower than today's date.", actualResult);
        }

        [Test]
        public void IsValidMethod_WithDateLessThanEighteenYears_ShouldReturnErrorMessage()
        {
            var user = new UserForManipulationDTO { DateOfBirth = DateTime.Now.AddYears(-1) };

            var validationResult = dateOfBirthAttribute.GetValidationResult(user, new ValidationContext(user));
            var actualResult = validationResult.ErrorMessage;

            Assert.AreEqual("User is not old enough to work.", actualResult);
        }
    }
}
