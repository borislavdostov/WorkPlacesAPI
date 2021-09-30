using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using WorkPlaces.Data.Entities;
using WorkPlaces.Data.Interfaces;
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
            mockUsersRepository.Setup(r => r.Delete(It.IsAny<User>()))
                .Callback<User>(u => usersFromRepository.Remove(u));
            usersService = new UsersService(mockUsersRepository.Object);
        }

        [Test]
        public void GetUsers_EmptyCollection_ShouldReturnCountZero()
        {
            var actualResult = usersService.GetUsers().Count();

            Assert.AreEqual(0, actualResult);
        }

        [Test]
        public void GetUsers_WithOneUser_ShouldReturnCountOne()
        {
            usersFromRepository.Add(new User());

            var actualResult = usersService.GetUsers().Count();

            Assert.AreEqual(1, actualResult);
        }

        [Test]
        public void GetUserAsync_UserFound_ShouldReturnCorrectUser()
        {
            usersFromRepository.Add(new User { Id = 5 });

            var actualResult = usersService.GetUserAsync(5).Result;

            Assert.IsNotNull(actualResult);
        }

        [Test]
        public void GetUsers_WithOneUser_ShouldReturnCorrectUsers()
        {
            usersFromRepository.Add(new User { Id = 1 });

            var users = usersService.GetUsers();
            var actualResult = users.First().Id;

            Assert.AreEqual(1, actualResult);
        }
    }
}
