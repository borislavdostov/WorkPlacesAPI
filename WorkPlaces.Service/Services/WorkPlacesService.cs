using System.Threading.Tasks;
using WorkPlaces.Data.Repositories;
using WorkPlaces.Service.Interfaces;

namespace WorkPlaces.Service.Services
{
    public class WorkPlacesService : IWorkPlacesService
    {
        private readonly IWorkPlacesRepository workPlacesRepository;

        public WorkPlacesService(IWorkPlacesRepository workPlacesRepository)
        {
            this.workPlacesRepository = workPlacesRepository;
        }

        public async Task<bool> WorkPlaceExistsAsync(int workPlaceId)
        {
            return await workPlacesRepository.ExistsAsync(workPlaceId);
        }
    }
}
