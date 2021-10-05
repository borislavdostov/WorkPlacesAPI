using NUnit.Framework;
using System;
using Workplaces.Extensions;

namespace Workplaces.Common.Tests
{
    [TestFixture]
    public class DateTimeExtensionsTests
    {
        DateTime date;

        [SetUp]
        public void Initialize()
        {
            date = DateTime.Now;
        }

        [Test]
        public void GetAge_WithFutureDate_ShouldReturnNegativeAge()
        {
            var actualResult = date.AddYears(1).GetAge();

            Assert.AreEqual(-1, actualResult);
        }

        [Test]
        public void GetAge_WithDateTimeNow_ShouldReturnZeroAge()
        {
            var actualResult = date.GetAge();

            Assert.AreEqual(0, actualResult);
        }

        [Test]
        public void GetAge_WithDateBeforeTwentyFiveYears_ShouldReturnAgeOfTwentyFive()
        {
            var actualResult = date.AddYears(-25).GetAge();

            Assert.AreEqual(25, actualResult);
        }
    }
}
