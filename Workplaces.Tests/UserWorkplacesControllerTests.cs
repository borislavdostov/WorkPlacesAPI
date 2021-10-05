using Moq;
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

        //SetUp method

        //Constructor

        //Get UserWorkplaces

        //Get UserWorkplace

        //Create UserWorkplace

        //Update UserWorkplace

        //Delete UserWorkplace
    }
}
