using System.Threading.Tasks;

namespace Workplaces.Service.Interfaces
{
    public interface IWorkplacesService
    {
        Task<bool> WorkplaceExistsAsync(int workplaceId);
    }
}
