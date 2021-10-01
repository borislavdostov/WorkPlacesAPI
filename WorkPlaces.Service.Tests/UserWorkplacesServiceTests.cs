using Moq;
using NUnit.Framework;
using System.Collections.Generic;
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
        List<UserWorkplace> userWorkplacesFromRepository;
        Mock<IUsersRepository> mockUsersRepository;
        List<User> usersFromRepository;
        Mock<IWorkplacesRepository> mockWorkplacesRepository;
        List<Workplace> workplacesFromRepository;

        [SetUp]
        public void Initialize()
        {
            mockUserWorkplacesRepository = new Mock<IUserWorkplacesRepository>();
            userWorkplacesFromRepository = new List<UserWorkplace>();
            mockUsersRepository = new Mock<IUsersRepository>();
            usersFromRepository = new List<User>();
            mockWorkplacesRepository = new Mock<IWorkplacesRepository>();
            workplacesFromRepository = new List<Workplace>();

            userWorkplacesService = new UserWorkplacesService(
                mockUserWorkplacesRepository.Object, mockUsersRepository.Object, mockWorkplacesRepository.Object);
        }
    }
}
