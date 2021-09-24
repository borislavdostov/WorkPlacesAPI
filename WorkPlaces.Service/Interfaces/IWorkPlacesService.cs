using System.Threading.Tasks;

namespace WorkPlaces.Service.Interfaces
{
    public interface IWorkplacesService
    {
        Task<bool> WorkplaceExistsAsync(int workplaceId);
    }
}
