using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using WorkPlaces.Data.Entities;
using WorkPlaces.Data.Interfaces;
using WorkPlaces.DataModel.Models;
using WorkPlaces.Service.Interfaces;
using WorkPlaces.Service.Services;

namespace WorkPlaces.Service.Tests
{
    [TestFixture]
    public class UsersServiceTests
    {
        private IUsersService usersService;
        private Mock<IUsersRepository> mockUsersRepository;
        private List<User> usersFromRepository;

        [SetUp]
        public void Initialize()
        {
            usersFromRepository = new List<User>();
            mockUsersRepository = new Mock<IUsersRepository>();
            mockUsersRepository.Setup(r => r.GetAll())
                .Returns(usersFromRepository.AsQueryable());
            mockUsersRepository.Setup(r => r.GetAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => usersFromRepository.FirstOrDefault(u => u.Id == id));
            mockUsersRepository.Setup(r => r.AddAsync(It.IsAny<User>()))
                .Callback<User>(u => usersFromRepository.Add(u));
            mockUsersRepository.Setup(r => r.Delete(It.IsAny<User>()))
                .Callback<User>(u => usersFromRepository.Remove(u));
            usersService = new UsersService(mockUsersRepository.Object);
        }

        [Test]
        public void GetUsersMethod_EmptyCollection_ShouldReturnCountZero()
        {
            var actualResult = usersService.GetUsers().Count();

            Assert.AreEqual(0, actualResult);
        }

        [Test]
        public void GetUsersMethod_WithOneUser_ShouldReturnCountOne()
        {
            usersFromRepository.Add(new User());

            var actualResult = usersService.GetUsers().Count();

            Assert.AreEqual(1, actualResult);
        }

        [Test]
        public void GetUsersMethod_WithOneUser_ShouldReturnUserIdOne()
        {
            usersFromRepository.Add(new User { Id = 1 });

            var users = usersService.GetUsers();
            var actualResult = users.First().Id;

            Assert.AreEqual(1, actualResult);
        }

        [Test]
        public void GetUserAsyncMethod_UserFound_ShouldReturnNonNullUser()
        {
            usersFromRepository.Add(new User { Id = 5 });

            var actualResult = usersService.GetUserAsync(5).Result;

            Assert.IsNotNull(actualResult);
        }

        [Test]
        public void CreateUserAsyncMethod_UserAdded_ShouldIncrementUserCount()
        {
            usersService.CreateUserAsync(new UserForManipulationDTO());
            var actualresult = usersFromRepository.Count();

            Assert.AreEqual(1, actualresult);
        }

        [Test]
        public void DeleteUserAsyncMethod_ExistingUser_ShouldDecrementUsersCount()
        {
            usersFromRepository.Add(new User { Id = 3 });

            usersService.DeleteUserAsync(3);
            var actualresult = usersFromRepository.Count();

            Assert.AreEqual(0, actualresult);
        }
    }
}
