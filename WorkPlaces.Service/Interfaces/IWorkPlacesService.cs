using System.Threading.Tasks;

namespace WorkPlaces.Service.Interfaces
{
    public interface IWorkPlacesService
    {
        Task<bool> WorkPlaceExistsAsync(int workPlaceId);
    }
}
