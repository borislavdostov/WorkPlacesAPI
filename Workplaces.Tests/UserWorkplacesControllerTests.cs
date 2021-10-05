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
            //mockUserWorkplacesService.Setup(s => s.GetUserAsync(It.IsAny<int>()))
            //    .ReturnsAsync((int id) => usersFromService.Where(u => u.Id == id)
            //    .Select(u => new UserForManipulationDTO()).Single());
            //mockUserWorkplacesService.Setup(s => s.CreateUserAsync(It.IsAny<UserForManipulationDTO>()))
            //    .Callback((UserForManipulationDTO user) => usersFromService.Add(new User()))
            //    .ReturnsAsync(new UserDTO());
            //mockUserWorkplacesService.Setup(s => s.UserExistsAsync(It.IsAny<int>()))
            //    .ReturnsAsync((int id) => usersFromService.Any(u => u.Id == id));

            usersFromService = new List<User>();
            mockUsersService = new Mock<IUsersService>();
            usersService = mockUsersService.Object;

            workplacesFromService = new List<Workplace>();
            mockWorkplacesService = new Mock<IWorkplacesService>();
            workplacesService = mockWorkplacesService.Object;

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
        public void GetUserWorkplaces_WithZeroUsers_ShouldReturnOk()
        {
            var actualResult = userWorkplacesController.GetUserWorkplaces().Result;

            Assert.IsInstanceOf<OkObjectResult>(actualResult);
        }

        [Test]
        public void GetUsers_WithTwoUsers_ShouldReturnOk()
        {
            userWorkplacesFromService.Add(new UserWorkplace { Id = 1 });
            userWorkplacesFromService.Add(new UserWorkplace { Id = 2 });

            var actualResult = userWorkplacesController.GetUserWorkplaces().Result;

            Assert.IsInstanceOf<OkObjectResult>(actualResult);
        }

        //Get UserWorkplaces

        //Get UserWorkplace

        //Create UserWorkplace

        //Update UserWorkplace

        //Delete UserWorkplace
    }
}
