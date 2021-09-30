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
        private List<User> usersFromRepository;
        private Mock<IUsersRepository> mockUsersRepository;
        private IUsersRepository usersRepository;

        [SetUp]
        public void Initialize()
        {
            usersFromRepository = new List<User>();
            mockUsersRepository = new Mock<IUsersRepository>();
            mockUsersRepository.Setup(r => r.GetAll()).Returns(usersFromRepository.AsQueryable());
            usersRepository = mockUsersRepository.Object;
            usersService = new UsersService(usersRepository);
        }

        [Test]
        public void GetUsers_WithOneUser_ShouldReturnUsersCorrectly()
        {
            usersFromRepository.Add(new User());
            var expectedResult = new List<UserDTO> { new UserDTO() };

            var users = usersService.GetUsers();

            Assert.AreEqual(expectedResult.Count, users.Count());
        }
    }
}
