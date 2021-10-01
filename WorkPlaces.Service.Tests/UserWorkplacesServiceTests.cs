﻿using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Workplaces.Data.Entities;
using Workplaces.Data.Interfaces;
using Workplaces.Data.Repositories;
using Workplaces.DataModel.Models;
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
            mockUserWorkplacesRepository.Setup(r => r.GetAll())
                .Returns(userWorkplacesFromRepository.AsQueryable());
            mockUserWorkplacesRepository.Setup(r => r.GetAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => userWorkplacesFromRepository.FirstOrDefault(uw => uw.Id == id));
            mockUserWorkplacesRepository.Setup(r => r.AddAsync(It.IsAny<UserWorkplace>()))
                .Callback<UserWorkplace>(uw => userWorkplacesFromRepository.Add(uw));

            mockUsersRepository = new Mock<IUsersRepository>();
            usersRepository = mockUsersRepository.Object;
            usersFromRepository = new List<User>();
            mockUsersRepository.Setup(r => r.GetAll())
                .Returns(usersFromRepository.AsQueryable());

            mockWorkplacesRepository = new Mock<IWorkplacesRepository>();
            workplacesRepository = mockWorkplacesRepository.Object;
            workplacesFromRepository = new List<Workplace>();
            mockWorkplacesRepository.Setup(r => r.GetAll())
                .Returns(workplacesFromRepository.AsQueryable());

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

        [Test]
        public void GetUserWorkplacesMethod_WithOneUserWorkplace_ShouldReturnCountOne()
        {
            userWorkplacesFromRepository.Add(new UserWorkplace { User = new User(), Workplace = new Workplace() });

            var actualResult = userWorkplacesService.GetUserWorkplaces().Count();

            Assert.AreEqual(1, actualResult);
        }

        [Test]
        public void GetUserWorkplacesMethod_WithOneUserWorkplace_ShouldReturnUserWorkplacesCorrectly()
        {
            userWorkplacesFromRepository.Add(new UserWorkplace { Id = 1, User = new User(), Workplace = new Workplace() });

            var userWorkplaces = userWorkplacesService.GetUserWorkplaces();
            var actualResult = userWorkplaces.First().Id;

            Assert.AreEqual(1, actualResult);
        }

        [Test]
        public void GetUserWorkplaceOptionsAsyncMethod_WithEmptyCollection_ShouldReturnNonNullOption()
        {
            var actualResult = userWorkplacesService.GetUserWorkplaceOptions();

            Assert.IsNotNull(actualResult);
        }

        [Test]
        public void GetUserWorkplaceOptionsAsyncMethod_WithOneUserWorkPlaceOption_ShouldReturnUserOptionsCountOne()
        {
            usersFromRepository.Add(new User());

            var actualResult = userWorkplacesService.GetUserWorkplaceOptions().Users.Count();

            Assert.AreEqual(1, actualResult);
        }

        [Test]
        public void GetUserWorkplaceOptionsAsyncMethod_WithOneUserWorkPlaceOption_ShouldReturnWorkplaceOptionsCountOne()
        {
            workplacesFromRepository.Add(new Workplace());

            var actualResult = userWorkplacesService.GetUserWorkplaceOptions().Workplaces.Count();

            Assert.AreEqual(1, actualResult);
        }

        [Test]
        public void GetUserWorkplaceOptionsAsyncMethod_WithOneUserWorkPlaceOption_ShouldReturnUserOptionsCorrectly()
        {
            usersFromRepository.Add(new User { Id = 1 });

            var actualResult = userWorkplacesService.GetUserWorkplaceOptions().Users.First().Id;

            Assert.AreEqual(1, actualResult);
        }

        [Test]
        public void GetUserWorkplaceOptionsAsyncMethod_WithOneUserWorkPlaceOption_ShouldReturnWorkplaceOptionsCorrectly()
        {
            workplacesFromRepository.Add(new Workplace { Id = 1 });

            var actualResult = userWorkplacesService.GetUserWorkplaceOptions().Workplaces.First().Id;

            Assert.AreEqual(1, actualResult);
        }

        [Test]
        public void GetUserWorkplaceAsyncMethod_WithExistingUserWorkplace_ShouldReturnNonNullUserWorkplace()
        {
            userWorkplacesFromRepository.Add(new UserWorkplace { Id = 1 });

            var actualResult = userWorkplacesService.GetUserWorkplaceAsync(1).Result;

            Assert.IsNotNull(actualResult);
        }

        [Test]
        public void GetUserWorkplaceAsyncMethod_WithExistingUserWorkplace_ShouldReturnUserIdCorrectly()
        {
            userWorkplacesFromRepository.Add(new UserWorkplace { Id = 1, UserId = 2 });

            var actualResult = userWorkplacesService.GetUserWorkplaceAsync(1).Result.UserId;

            Assert.AreEqual(2, actualResult);
        }

        [Test]
        public void GetUserWorkplaceAsyncMethod_WithExistingUserWorkplace_ShouldReturnWorkplaceIdCorrectly()
        {
            userWorkplacesFromRepository.Add(new UserWorkplace { Id = 1, WorkplaceId = 2 });

            var actualResult = userWorkplacesService.GetUserWorkplaceAsync(1).Result.WorkplaceId;

            Assert.AreEqual(2, actualResult);
        }

        [Test]
        public void CreateUserWorkplaceAsyncMethod_UserWorkPlaceAdded_ShouldIncrementUserWorkplacesCount()
        {
            userWorkplacesService.CreateUserWorkplaceAsync(new UserWorkplaceForManipulationDTO());
            var actualResult = userWorkplacesFromRepository.Count;

            Assert.AreEqual(1, actualResult);
        }

        [Test]
        public void CreateUserWorkplaceAsyncMethod_UserAdded_ShouldAddUserIdCorrectly()
        {
            userWorkplacesService.CreateUserWorkplaceAsync(
                new UserWorkplaceForManipulationDTO { UserId = 1 });
            var actualResult = userWorkplacesFromRepository.FirstOrDefault().UserId;

            Assert.AreEqual(1, actualResult);
        }

        [Test]
        public void CreateUserWorkplaceAsyncMethod_UserAdded_ShouldAddWorkplaceIdCorrectly()
        {
            userWorkplacesService.CreateUserWorkplaceAsync(
                new UserWorkplaceForManipulationDTO { WorkplaceId = 1 });
            var actualResult = userWorkplacesFromRepository.FirstOrDefault().WorkplaceId;

            Assert.AreEqual(1, actualResult);
        }
    }
}