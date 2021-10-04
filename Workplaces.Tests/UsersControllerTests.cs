using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Workplaces.Controllers;
using Workplaces.DataModel.Models;
using Workplaces.Service.Interfaces;

namespace Workplaces.Tests
{
    [TestFixture]
    public class UsersControllerTests
    {
        private UsersController usersController;

        private Mock<IUsersService> mockUsersService;
        private List<UserDTO> usersFromService;

        [SetUp]
        public void Initialize()
        {
            usersFromService = new List<UserDTO>();
            mockUsersService = new Mock<IUsersService>();
            mockUsersService.Setup(s => s.GetUsers())
                .Returns(usersFromService);

            usersController = new UsersController(mockUsersService.Object);
        }

        [Test]
        public void Constructor_WithNullUsersService_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => usersController = new UsersController(null));
        }

        [Test]
        public void GetUsersMethod_WithTwoUsers_ShouldReturnOK()
        {
            usersFromService.Add(new UserDTO { Id = 1 });
            usersFromService.Add(new UserDTO { Id = 2 });

            var actualResult = usersController.GetUsers();

            Assert.IsInstanceOf<OkObjectResult>(actualResult.Result);
        }


    }
}
