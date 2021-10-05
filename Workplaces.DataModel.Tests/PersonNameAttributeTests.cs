using NUnit.Framework;
using System.ComponentModel.DataAnnotations;
using Workplaces.DataModel.ValidationAttributes;

namespace Workplaces.DataModel.Tests
{
    [TestFixture]
    public class PersonNameAttributeTests
    {
        PersonNameAttribute personNameAttribute;

        [SetUp]
        public void Initialize()
        {
            personNameAttribute = new PersonNameAttribute();
        }

        [TestCase("peter")]
        [TestCase("o Henry")]
        [TestCase("o'Riley")]
        [TestCase("hAALAND")]
        [TestCase("mcDonald")]
        [TestCase("1Peter")]
        [TestCase("'Peter")]
        [TestCase("Peter1")]
        public void IsValid_WithIncorrectName_ShouldReturnFalse(string name)
        {
            var validationResult = personNameAttribute.GetValidationResult(name, new ValidationContext(name));
            var actualResult = validationResult == ValidationResult.Success;

            Assert.IsFalse(actualResult);
        }

        [Test]
        public void IsValid_WithIncorrectName_ShouldReturnErrorMessage()
        {
            var name = "john";

            var validationResult = personNameAttribute.GetValidationResult(name, new ValidationContext(name));
            var actualResult = validationResult.ErrorMessage;

            Assert.AreEqual(ValidationMessage.IncorrectName, actualResult);
        }

        [TestCase("Peter")]
        [TestCase("O'Riley")]
        [TestCase("McDonald")]
        [TestCase("Jan van")]
        public void IsValid_WithCorrectName_ShouldReturnTrue(string name)
        {
            var validationResult = personNameAttribute.GetValidationResult(name, new ValidationContext(name));
            var actualResult = validationResult == ValidationResult.Success;

            Assert.IsTrue(actualResult);
        }
    }
}
