using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Workplaces.Data.Entities;
using Workplaces.Data.Interfaces;
using Workplaces.Data.Repositories;
using Workplaces.Service.Interfaces;
using Workplaces.Service.Services;

namespace Workplaces.Service.Tests
{
    [TestFixture]
    public class UserWorkplacesServiceTests
    {
        IUserWorkplacesService userWorkplacesService;

        Mock<IUserWorkplacesRepository> mockUserWorkplacesRepository;
        IUserWorkplacesRepository userWorkplacesRepository;
        List<UserWorkplace> userWorkplacesFromRepository;

        Mock<IUsersRepository> mockUsersRepository;
        IUsersRepository usersRepository;
        List<User> usersFromRepository;

        Mock<IWorkplacesRepository> mockWorkplacesRepository;
        IWorkplacesRepository workplacesRepository;
        List<Workplace> workplacesFromRepository;


        [SetUp]
        public void Initialize()
        {
            mockUserWorkplacesRepository = new Mock<IUserWorkplacesRepository>();
            userWorkplacesRepository = mockUserWorkplacesRepository.Object;
            userWorkplacesFromRepository = new List<UserWorkplace>();
            mockUserWorkplacesRepository.Setup(r => r.GetAll()).Returns(userWorkplacesFromRepository.AsQueryable());

            mockUsersRepository = new Mock<IUsersRepository>();
            usersRepository = mockUsersRepository.Object;
            usersFromRepository = new List<User>();

            mockWorkplacesRepository = new Mock<IWorkplacesRepository>();
            workplacesRepository = mockWorkplacesRepository.Object;
            workplacesFromRepository = new List<Workplace>();


            userWorkplacesService = new UserWorkplacesService(
                mockUserWorkplacesRepository.Object, mockUsersRepository.Object, mockWorkplacesRepository.Object);
        }

        [Test]
        public void Constructor_WithNullUserWorkplacesRepository_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(
                () => userWorkplacesService = new UserWorkplacesService(null, usersRepository, workplacesRepository));
        }

        [Test]
        public void Constructor_WithNullUsersRepository_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(
                () => userWorkplacesService = new UserWorkplacesService(userWorkplacesRepository, null, workplacesRepository));
        }

        [Test]
        public void Constructor_WithNullWorkplacesRepository_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(
                () => userWorkplacesService = new UserWorkplacesService(userWorkplacesRepository, usersRepository, null));
        }

        [Test]
        public void GetUserWorkplacesMethod_EmptyCollection_ShouldReturnCountZero()
        {
            var actualResult = userWorkplacesService.GetUserWorkplaces().Count();

            Assert.AreEqual(0, actualResult);
        }
    }
}
