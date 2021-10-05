using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Workplaces.Controllers;
using Workplaces.Data.Entities;
using Workplaces.Service.Interfaces;

namespace Workplaces.Tests
{
    public class UserWorkplacesControllerTests
    {
        private UserWorkplacesController userWorkplacesController;

        private Mock<IUserWorkplacesService> mockUserWorkplacesService;
        private List<UserWorkplace> userWorkplacesFromService;

        private Mock<IUsersService> mockUsersService;
        private List<User> usersFromService;

        private Mock<IWorkplacesService> mockWorkplacesService;
        private List<Workplace> workplacesFromService;

        [SetUp]
        public void Initialize()
        {
            userWorkplacesFromService = new List<UserWorkplace>();
            mockUserWorkplacesService = new Mock<IUserWorkplacesService>();
            //mockUserWorkplacesService.Setup(s => s.GetUsers())
            //    .Returns(usersFromService.Select(u => new UserDTO()));
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

            workplacesFromService = new List<Workplace>();
            mockWorkplacesService = new Mock<IWorkplacesService>();

            userWorkplacesController = new UserWorkplacesController(
                mockUserWorkplacesService.Object, mockUsersService.Object, mockWorkplacesService.Object);
        }

        [Test]
        public void Constructor_WithNullUserWorkplacesService_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => userWorkplacesController =
                new UserWorkplacesController(null, mockUsersService.Object, mockWorkplacesService.Object));
        }

        [Test]
        public void Constructor_WithNullUsersService_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => userWorkplacesController =
                new UserWorkplacesController(mockUserWorkplacesService.Object, null, mockWorkplacesService.Object));
        }

        [Test]
        public void Constructor_WithNullWorkplacesService_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => userWorkplacesController =
                new UserWorkplacesController(mockUserWorkplacesService.Object, mockUsersService.Object, null));
        }

        //Get UserWorkplaces

        //Get UserWorkplace

        //Create UserWorkplace

        //Update UserWorkplace

        //Delete UserWorkplace
    }
}
