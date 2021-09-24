using System.Threading.Tasks;
using WorkPlaces.Data.Repositories;
using WorkPlaces.Service.Interfaces;

namespace WorkPlaces.Service.Services
{
    public class WorkplacesService : IWorkplacesService
    {
        private readonly IWorkplacesRepository workplacesRepository;

        public WorkplacesService(IWorkplacesRepository workplacesRepository)
        {
            this.workplacesRepository = workplacesRepository;
        }

        public async Task<bool> WorkplaceExistsAsync(int workplaceId)
        {
            return await workplacesRepository.ExistsAsync(workplaceId);
        }
    }
}
