using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Workplaces.Data.Entities;
using Workplaces.Data.Repositories;
using Workplaces.Service.Interfaces;
using Workplaces.Service.Services;

namespace Workplaces.Service.Tests
{
    [TestFixture]
    public class WorkplacesServiceTests
    {
        IWorkplacesService workplacesService;
        Mock<IWorkplacesRepository> mockWorkplacesRepository;
        List<Workplace> workplacesFromRepository;

        [SetUp]
        public void Initialize()
        {
            workplacesFromRepository = new List<Workplace>();
            mockWorkplacesRepository = new Mock<IWorkplacesRepository>();

            mockWorkplacesRepository.Setup(r => r.ExistsAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => workplacesFromRepository.Any(w => w.Id == id));

            workplacesService = new WorkplacesService(mockWorkplacesRepository.Object);
        }

        [Test]
        public void Constructor_WithNullWorkplacesRepository_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => workplacesService = new WorkplacesService(null));
        }

        [Test]
        public void WorkplaceExistsAsync_WithNonExistingWorkplace_ShouldReturnFalse()
        {
            var actualResult = workplacesService.WorkplaceExistsAsync(1).Result;

            Assert.IsFalse(actualResult);
        }

        [Test]
        public void WorkplaceExistsAsync_WithExistingWorkplace_ShouldReturnTrue()
        {
            workplacesFromRepository.Add(new Workplace { Id = 1 });

            var actualResult = workplacesService.WorkplaceExistsAsync(1).Result;

            Assert.IsTrue(actualResult);
        }
    }
}
