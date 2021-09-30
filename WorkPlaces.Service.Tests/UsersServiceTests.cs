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
            mockUsersRepository.Setup(r => r.GetAll()).Returns(usersFromRepository.AsQueryable());
            usersService = new UsersService(mockUsersRepository.Object);
        }

        [Test]
        public void GetUsers_EmptyCollection_ShouldReturnCountZero()
        {
            var expectedResult = new List<UserDTO>();

            var actualUsers = usersService.GetUsers();

            Assert.AreEqual(expectedResult.Count, actualUsers.Count());
        }

        [Test]
        public void GetUsers_WithOneUser_ShouldReturnCountOne()
        {
            usersFromRepository.Add(new User());
            var expectedResult = new List<UserDTO> { new UserDTO() };

            var actualUsers = usersService.GetUsers();

            Assert.AreEqual(expectedResult.Count, actualUsers.Count());
        }
    }
}
