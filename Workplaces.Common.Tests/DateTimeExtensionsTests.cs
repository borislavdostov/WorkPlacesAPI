using NUnit.Framework;
using System;
using Workplaces.Extensions;

namespace Workplaces.Common.Tests
{
    [TestFixture]
    public class DateTimeExtensionsTests
    {
        [Test]
        public void GetAge_WithFutureDate_ShouldReturnNegativeAge()
        {
            var date = DateTime.Now.AddYears(1);

            var actualResult = date.GetAge();

            Assert.AreEqual(-1, actualResult);
        }

        [Test]
        public void GetAge_WithDateTimeNow_ShouldReturnZeroAge()
        {
            var date = DateTime.Now;

            var actualResult = date.GetAge();

            Assert.AreEqual(0, actualResult);
        }

        [Test]
        public void GetAge_WithDateBeforeTwentyFiveYears_ShouldReturnAgeCorrectly()
        {
            var date = DateTime.Now.AddYears(-25);

            var actualResult = date.GetAge();

            Assert.AreEqual(25, actualResult);
        }
    }
}
