using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Workplaces.Data.Entities;
using Workplaces.Data.Interfaces;
using Workplaces.DataModel.Models;
using Workplaces.Service.Interfaces;
using Workplaces.Service.Services;

namespace Workplaces.Service.Tests
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
            mockUsersRepository.Setup(r => r.ExistsAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => usersFromRepository.Any(u => u.Id == id));

            usersService = new UsersService(mockUsersRepository.Object);
        }

        [Test]
        public void Constructor_WithNullUsersRepository_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => usersService = new UsersService(null));
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
        public void GetUsers_WithOneUser_ShouldReturnUsersCorrectly()
        {
            usersFromRepository.Add(new User { Id = 1 });

            var users = usersService.GetUsers();
            var actualResult = users.First().Id;

            Assert.AreEqual(1, actualResult);
        }

        [Test]
        public void GetUserAsync_WithExistingUser_ShouldReturnNonNullUser()
        {
            usersFromRepository.Add(new User { Id = 1 });

            var actualResult = usersService.GetUserAsync(1).Result;

            Assert.IsNotNull(actualResult);
        }

        [Test]
        public void GetUserAsync_WithExistingUser_ShouldReturnUserCorrectly()
        {
            usersFromRepository.Add(new User { Id = 1, FirstName = "Ivan", LastName = "Ivanov" });
            usersFromRepository.Add(new User { Id = 2, FirstName = "John", LastName = "Doe" });
            usersFromRepository.Add(new User { Id = 3, FirstName = "Dimitar", LastName = "Dimitrov" });

            var user = usersService.GetUserAsync(2).Result;

            Assert.AreEqual("John", user.FirstName);
            Assert.AreEqual("Doe", user.LastName);
        }

        [Test]
        public void CreateUserAsync_UserAdded_ShouldIncrementUsersCount()
        {
            usersService.CreateUserAsync(new UserForManipulationDTO());
            var actualResult = usersFromRepository.Count();

            Assert.AreEqual(1, actualResult);
        }

        [Test]
        public void UpdateUserAsync_WithExistingUser_ShouldChangeUsersName()
        {
            var user = new User { Id = 1, FirstName = "Nikolay", LastName = "Nikolov" };
            usersFromRepository.Add(user);

            usersService.UpdateUserAsync(1, new UserForManipulationDTO { FirstName = "John", LastName = "Doe" });
            var updatedUser = usersFromRepository.FirstOrDefault();
            var actualResult = $"{updatedUser.FirstName} {updatedUser.LastName}";

            Assert.AreEqual("John Doe", actualResult);
        }

        [Test]
        public void DeleteUserAsync_WithNonExistingUser_ShouldNotReflectOnUsersCount()
        {
            usersFromRepository.Add(new User { Id = 1 });

            usersService.DeleteUserAsync(2);
            var actualResult = usersFromRepository.Count();

            Assert.AreEqual(1, actualResult);
        }

        [Test]
        public void DeleteUserAsync_WithExistingUser_ShouldDecrementUsersCount()
        {
            usersFromRepository.Add(new User { Id = 1 });

            usersService.DeleteUserAsync(1);
            var actualResult = usersFromRepository.Count();

            Assert.AreEqual(0, actualResult);
        }

        [Test]
        public void DeleteUserAsync_WithExistingUser_ShouldDeleteCorrectUser()
        {
            usersFromRepository.Add(new User { Id = 1 });

            usersService.DeleteUserAsync(1);
            var actualResult = usersFromRepository.FirstOrDefault(u => u.Id == 1);

            Assert.IsNull(actualResult);
        }

        [Test]
        public void UserExistsAsync_WithNonExistingUser_ShouldReturnFalse()
        {
            var actualResult = usersService.UserExistsAsync(1).Result;

            Assert.IsFalse(actualResult);
        }

        [Test]
        public void UserExistsAsync_WithExistingUser_ShouldReturnTrue()
        {
            usersFromRepository.Add(new User { Id = 1 });

            var actualResult = usersService.UserExistsAsync(1).Result;

            Assert.IsTrue(actualResult);
        }
    }
}
