using System.Threading.Tasks;
using WorkPlaces.Data.Repositories;
using WorkPlaces.Service.Interfaces;

namespace WorkPlaces.Service.Services
{
    public class WorkPlacesService : IWorkplacesService
    {
        private readonly IWorkplacesRepository workPlacesRepository;

        public WorkPlacesService(IWorkplacesRepository workPlacesRepository)
        {
            this.workPlacesRepository = workPlacesRepository;
        }

        public async Task<bool> WorkplaceExistsAsync(int workPlaceId)
        {
            return await workPlacesRepository.ExistsAsync(workPlaceId);
        }
    }
}
