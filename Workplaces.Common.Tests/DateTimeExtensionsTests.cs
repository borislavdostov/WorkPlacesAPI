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
        public void GetAgeMethod_WithAgeZero_ShouldReturnZeroAge()
        {
            var date = DateTime.Now;

            var actualResult = date.GetAge();

            Assert.AreEqual(0, actualResult);
        }

        [Test]
        public void GetAgeMethod_WithNegativeAge_ShouldReturnNegativeAge()
        {
            var date = DateTime.Now.AddYears(1);

            var actualResult = date.GetAge();

            Assert.AreEqual(-1, actualResult);
        }
    }
}
