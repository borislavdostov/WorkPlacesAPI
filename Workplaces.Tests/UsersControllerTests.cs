using Moq;
using NUnit.Framework;
using System;
using Workplaces.Controllers;
using Workplaces.Service.Interfaces;

namespace Workplaces.Tests
{
    [TestFixture]
    public class UsersControllerTests
    {
        private UsersController usersController;

        private Mock<IUsersService> mockUsersService;

        [SetUp]
        public void Initialize()
        {
            mockUsersService = new Mock<IUsersService>();
            usersController = new UsersController(mockUsersService.Object);
        }

        [Test]
        public void Constructor_WithNullUsersService_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => usersController = new UsersController(null));
        }
    }
}
