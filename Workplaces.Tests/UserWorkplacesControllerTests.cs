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
    public class UserWorkplacesControllerTests
    {
        private UserWorkplacesController userWorkplacesController;

        private Mock<IUserWorkplacesService> mockUserWorkplacesService;
        private IUserWorkplacesService userWorkplacesService;
        private List<UserWorkplace> userWorkplacesFromService;

        private Mock<IUsersService> mockUsersService;
        private IUsersService usersService;
        private List<User> usersFromService;

        private Mock<IWorkplacesService> mockWorkplacesService;
        private IWorkplacesService workplacesService;
        private List<Workplace> workplacesFromService;

        [SetUp]
        public void Initialize()
        {
            userWorkplacesFromService = new List<UserWorkplace>();
            mockUserWorkplacesService = new Mock<IUserWorkplacesService>();
            userWorkplacesService = mockUserWorkplacesService.Object;
            mockUserWorkplacesService.Setup(s => s.GetUserWorkplaces())
                .Returns(userWorkplacesFromService.Select(u => new UserWorkplaceDTO()));
            mockUserWorkplacesService.Setup(s => s.GetUserWorkplaceOptions())
                .Returns(new UserWorkplaceOptionsDTO());
            mockUserWorkplacesService.Setup(s => s.GetUserWorkplaceAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => userWorkplacesFromService.Where(u => u.Id == id)
                .Select(u => new UserWorkplaceForManipulationDTO()).Single());
            mockUserWorkplacesService.Setup(s => s.CreateUserWorkplaceAsync(It.IsAny<UserWorkplaceForManipulationDTO>()))
                .Callback(() => userWorkplacesFromService.Add(new UserWorkplace()))
                .ReturnsAsync(new UserWorkplaceDTO());
            mockUserWorkplacesService.Setup(s => s.UserWorkplaceExistsAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => userWorkplacesFromService.Any(u => u.Id == id));

            usersFromService = new List<User>();
            mockUsersService = new Mock<IUsersService>();
            usersService = mockUsersService.Object;
            mockUsersService.Setup(s => s.UserExistsAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => usersFromService.Any(u => u.Id == id));

            workplacesFromService = new List<Workplace>();
            mockWorkplacesService = new Mock<IWorkplacesService>();
            workplacesService = mockWorkplacesService.Object;
            mockWorkplacesService.Setup(s => s.WorkplaceExistsAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => workplacesFromService.Any(u => u.Id == id));

            userWorkplacesController = new UserWorkplacesController(userWorkplacesService, usersService, workplacesService);
        }

        [Test]
        public void Constructor_WithNullUserWorkplacesService_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => userWorkplacesController =
            new UserWorkplacesController(null, usersService, workplacesService));
        }

        [Test]
        public void Constructor_WithNullUsersService_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => userWorkplacesController =
            new UserWorkplacesController(userWorkplacesService, null, workplacesService));
        }

        [Test]
        public void Constructor_WithNullWorkplacesService_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => userWorkplacesController =
            new UserWorkplacesController(userWorkplacesService, usersService, null));
        }

        [Test]
        public void GetUserWorkplaces_WithZeroUserWorkplaces_ShouldReturnOk()
        {
            var actualResult = userWorkplacesController.GetUserWorkplaces().Result;

            Assert.IsInstanceOf<OkObjectResult>(actualResult);
        }

        [Test]
        public void GetUserWorkplaces_WithTwoUserWorkplaces_ShouldReturnOk()
        {
            userWorkplacesFromService.Add(new UserWorkplace { Id = 1 });
            userWorkplacesFromService.Add(new UserWorkplace { Id = 2 });

            var actualResult = userWorkplacesController.GetUserWorkplaces().Result;

            Assert.IsInstanceOf<OkObjectResult>(actualResult);
        }

        [Test]
        public void GetUserWorkplaceOptions_WithUserWorkplaceOptions_ShouldReturnOk()
        {
            var actualResult = userWorkplacesController.GetUserWorkplaceOptions().Result;

            Assert.IsInstanceOf<OkObjectResult>(actualResult);
        }

        [Test]
        public void GetUserWorkplace_WithEmptyCollection_ShouldReturnNotFound()
        {
            var actualResult = userWorkplacesController.GetUserWorkplace(1).Result.Result;

            Assert.IsInstanceOf<NotFoundResult>(actualResult);
        }

        [Test]
        public void GetUserWorkplace_WithNonExistingUserWorkplace_ShouldReturnNotFound()
        {
            userWorkplacesFromService.Add(new UserWorkplace { Id = 1 });

            var actualResult = userWorkplacesController.GetUserWorkplace(2).Result.Result;

            Assert.IsInstanceOf<NotFoundResult>(actualResult);
        }

        [Test]
        public void GetUserWorkplace_WithExistingUserWorkplace_ShouldReturnOk()
        {
            userWorkplacesFromService.Add(new UserWorkplace { Id = 1 });

            var actualResult = userWorkplacesController.GetUserWorkplace(1).Result.Result;

            Assert.IsInstanceOf<OkObjectResult>(actualResult);
        }

        [Test]
        public void CreateUserWorkplace_WithoutUserAndWorkplace_ShouldReturnNotFound()
        {
            var actualResult = userWorkplacesController.CreateUserWorkplace(
                new UserWorkplaceForManipulationDTO()).Result.Result;

            Assert.IsInstanceOf<NotFoundResult>(actualResult);
        }

        [Test]
        public void CreateUserWorkplace_WithNonExistingUser_ShouldReturnNotFound()
        {
            usersFromService.Add(new User { Id = 1 });
            workplacesFromService.Add(new Workplace { Id = 1 });

            var actualResult = userWorkplacesController.CreateUserWorkplace(
                new UserWorkplaceForManipulationDTO { UserId = 2, WorkplaceId = 1 }).Result.Result;

            Assert.IsInstanceOf<NotFoundResult>(actualResult);
        }

        [Test]
        public void CreateUserWorkplace_WithNonExistingWorkplace_ShouldReturnNotFound()
        {
            usersFromService.Add(new User { Id = 1 });
            workplacesFromService.Add(new Workplace { Id = 1 });

            var actualResult = userWorkplacesController.CreateUserWorkplace(
                new UserWorkplaceForManipulationDTO { UserId = 1, WorkplaceId = 2 }).Result.Result;

            Assert.IsInstanceOf<NotFoundResult>(actualResult);
        }

        [Test]
        public void CreateUserWorkplace_AddUserWorkplace_ShouldReturnCreatedAtRoute()
        {
            usersFromService.Add(new User { Id = 1 });
            workplacesFromService.Add(new Workplace { Id = 1 });

            var actualResult = userWorkplacesController.CreateUserWorkplace(
                new UserWorkplaceForManipulationDTO { UserId = 1, WorkplaceId = 1 }).Result.Result;

            Assert.IsInstanceOf<CreatedAtRouteResult>(actualResult);
        }

        [Test]
        public void UpdateUserWorkplace_WithEmptyCollection_ShouldReturnNotFound()
        {
            var actualResult = userWorkplacesController.UpdateUserWorkplace(
                1, new UserWorkplaceForManipulationDTO()).Result;

            Assert.IsInstanceOf<NotFoundResult>(actualResult);
        }

        [Test]
        public void UpdateUserWorkplace_WithNonExistingUserWorkplace_ShouldReturnNotFound()
        {
            userWorkplacesFromService.Add(new UserWorkplace { Id = 1 });

            var actualResult = userWorkplacesController.UpdateUserWorkplace(
                2, new UserWorkplaceForManipulationDTO()).Result;

            Assert.IsInstanceOf<NotFoundResult>(actualResult);
        }

        //Update UserWorkplace

        //Delete UserWorkplace
    }
}
