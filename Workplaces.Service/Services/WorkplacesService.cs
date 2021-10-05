using System;
using System.Threading.Tasks;
using Workplaces.Data.Repositories;
using Workplaces.Service.Interfaces;

namespace Workplaces.Service.Services
{
    public class WorkplacesService : IWorkplacesService
    {
        private readonly IWorkplacesRepository workplacesRepository;

        public WorkplacesService(IWorkplacesRepository workplacesRepository)
        {
            this.workplacesRepository = workplacesRepository ?? throw new ArgumentNullException();
        }

        public async Task<bool> WorkplaceExistsAsync(int workplaceId)
        {
            return await workplacesRepository.ExistsAsync(workplaceId);
        }
    }
}
