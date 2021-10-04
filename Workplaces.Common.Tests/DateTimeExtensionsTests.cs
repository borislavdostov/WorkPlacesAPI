using NUnit.Framework;
using System;
using Workplaces.Extensions;

namespace Workplaces.Common.Tests
{
    [TestFixture]
    public class DateTimeExtensionsTests
    {
        [Test]
        public void GetAgeMethod_WithAgeTwentyFive_ShouldReturnAgeCorrectly()
        {
            var date = DateTime.Now.AddYears(-25);

            var actualResult = date.GetAge();

            Assert.AreEqual(25, actualResult);
        }

        [Test]
        public void GetAgeMethod_WithLeapYear_ShouldReturnAgeCorrectly()
        {
            var date = new DateTime(2004, 2, 29);

            var actualResult = date.GetAge();

            Assert.AreEqual(17, actualResult);
        }

        [Test]
        public void GetAgeMethod_WithAgeZero_ShouldReturnZeroAge()
        {
            var date = DateTime.Now;

            var actualResult = date.GetAge();

            Assert.AreEqual(0, actualResult);
        }

        [Test]
        public void GetAgeMethod_WithNegativeAge_ShouldReturnNegativeAge()
        {
            var date = new DateTime(2025, 3, 3);

            var actualResult = date.GetAge();

            Assert.AreEqual(-4, actualResult);
        }
    }
}
