using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Workplaces.Controllers;
using Workplaces.Data.Entities;
using Workplaces.DataModel.Models;
using Workplaces.Service.Interfaces;

namespace Workplaces.Tests
{
    [TestFixture]
    public class UsersControllerTests
    {
        private UsersController usersController;

        private Mock<IUsersService> mockUsersService;
        private List<User> usersFromService;

        [SetUp]
        public void Initialize()
        {
            usersFromService = new List<User>();
            mockUsersService = new Mock<IUsersService>();
            mockUsersService.Setup(s => s.GetUsers())
                .Returns(usersFromService.Select(u => new UserDTO()));
            mockUsersService.Setup(s => s.GetUserAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => usersFromService.Where(u => u.Id == id)
                .Select(u => new UserForManipulationDTO()).Single());
            mockUsersService.Setup(s => s.CreateUserAsync(It.IsAny<UserForManipulationDTO>()))
                .Callback(() => usersFromService.Add(new User()))
                .ReturnsAsync(new UserDTO());
            mockUsersService.Setup(s => s.UserExistsAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => usersFromService.Any(u => u.Id == id));

            usersController = new UsersController(mockUsersService.Object);
        }

        [Test]
        public void Constructor_WithNullUsersService_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => usersController = new UsersController(null));
        }

        [Test]
        public void GetUsers_WithZeroUsers_ShouldReturnOk()
        {
            var actualResult = usersController.GetUsers().Result;

            Assert.IsInstanceOf<OkObjectResult>(actualResult);
        }

        [Test]
        public void GetUsers_WithTwoUsers_ShouldReturnOk()
        {
            usersFromService.Add(new User { Id = 1 });
            usersFromService.Add(new User { Id = 2 });

            var actualResult = usersController.GetUsers().Result;

            Assert.IsInstanceOf<OkObjectResult>(actualResult);
        }

        [Test]
        public void GetUser_WithExistingUser_ShouldReturnOk()
        {
            usersFromService.Add(new User { Id = 1 });

            var actualResult = usersController.GetUser(1).Result.Result;

            Assert.IsInstanceOf<OkObjectResult>(actualResult);
        }

        [Test]
        public void GetUser_WithEmptyCollection_ShouldReturnNotFound()
        {
            var actualResult = usersController.GetUser(1).Result.Result;

            Assert.IsInstanceOf<NotFoundResult>(actualResult);
        }

        [Test]
        public void GetUser_WithNonExistingUser_ShouldReturnNotFound()
        {
            usersFromService.Add(new User { Id = 1 });

            var actualResult = usersController.GetUser(2).Result.Result;

            Assert.IsInstanceOf<NotFoundResult>(actualResult);
        }

        [Test]
        public void CreateUser_AddUser_ShouldReturnCreatedAtRoute()
        {
            var actualResult = usersController.CreateUser(new UserForManipulationDTO()).Result.Result;

            Assert.IsInstanceOf<CreatedAtRouteResult>(actualResult);
        }

        [Test]
        public void UpdateUser_WithExistingUser_ShouldReturnNoContent()
        {
            usersFromService.Add(new User { Id = 1 });

            var actualResult = usersController.UpdateUser(1, new UserForManipulationDTO()).Result;

            Assert.IsInstanceOf<NoContentResult>(actualResult);
        }

        [Test]
        public void UpdateUser_EmptyCollection_ShouldReturnNotFound()
        {
            var actualResult = usersController.UpdateUser(1, new UserForManipulationDTO()).Result;

            Assert.IsInstanceOf<NotFoundResult>(actualResult);
        }

        [Test]
        public void UpdateUser_WithNonExistingUser_ShouldReturnNotFound()
        {
            usersFromService.Add(new User { Id = 1 });

            var actualResult = usersController.UpdateUser(2, new UserForManipulationDTO()).Result;

            Assert.IsInstanceOf<NotFoundResult>(actualResult);
        }

        [Test]
        public void DeleteUser_WithExistingUser_ShouldReturnNoContent()
        {
            usersFromService.Add(new User { Id = 1 });

            var actualResult = usersController.DeleteUser(1).Result;

            Assert.IsInstanceOf<NoContentResult>(actualResult);
        }

        [Test]
        public void DeleteUser_EmptyCollection_ShouldReturnNotFound()
        {
            var actualResult = usersController.DeleteUser(1).Result;

            Assert.IsInstanceOf<NotFoundResult>(actualResult);
        }

        [Test]
        public void DeleteUser_WithNonExistingUser_ShouldReturnNotFound()
        {
            usersFromService.Add(new User { Id = 1 });

            var actualResult = usersController.DeleteUser(2).Result;

            Assert.IsInstanceOf<NotFoundResult>(actualResult);
        }
    }
}
