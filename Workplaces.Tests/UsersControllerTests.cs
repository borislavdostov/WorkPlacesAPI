using NUnit.Framework;
using System;
using Workplaces.Controllers;

namespace Workplaces.Tests
{
    [TestFixture]
    public class UsersControllerTests
    {
        private UsersController usersController;

        [SetUp]
        public void Initialize()
        {
            //usersController = new UsersController(null);
        }

        [Test]
        public void Constructor_WithNullUsersService_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => usersController = new UsersController(null));
        }
    }
}
