using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Workplaces.Controllers;
using Workplaces.Data.Entities;
using Workplaces.DataModel.Models;
using Workplaces.Extensions;
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
                .Returns(usersFromService.Select(u => new UserDTO
                {
                    Id = u.Id,
                    Name = $"{u.FirstName} {u.LastName}",
                    Email = u.Email,
                    Age = u.DateOfBirth.GetAge()
                }));
            mockUsersService.Setup(s => s.GetUserAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => usersFromService.Where(u => u.Id == id)
                .Select(u => new UserForManipulationDTO
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    DateOfBirth = u.DateOfBirth,
                }).Single());
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
        public void GetUsersMethod_WithZeroUsers_ShouldReturnOk()
        {
            var actualResult = usersController.GetUsers().Result;

            Assert.IsInstanceOf<OkObjectResult>(actualResult);
        }

        [Test]
        public void GetUsersMethod_WithTwoUsers_ShouldReturnOk()
        {
            usersFromService.Add(new User { Id = 1 });
            usersFromService.Add(new User { Id = 2 });

            var actualResult = usersController.GetUsers().Result;

            Assert.IsInstanceOf<OkObjectResult>(actualResult);
        }

        [Test]
        public void GetUserMethod_WithExistingUser_ShouldReturnOk()
        {
            usersFromService.Add(new User { Id = 1 });

            var actualResult = usersController.GetUser(1).Result.Result;

            Assert.IsInstanceOf<OkObjectResult>(actualResult);
        }

        [Test]
        public void GetUserMethod_WithEmptyCollection_ShouldReturnNotFound()
        {
            var actualResult = usersController.GetUser(1).Result.Result;

            Assert.IsInstanceOf<NotFoundResult>(actualResult);
        }

        [Test]
        public void GetUserMethod_WithNonExistingUser_ShouldReturnNotFound()
        {
            usersFromService.Add(new User { Id = 1 });

            var actualResult = usersController.GetUser(2).Result.Result;

            Assert.IsInstanceOf<NotFoundResult>(actualResult);
        }

        //Create User

        //Update User

        //Delete User
    }
}
