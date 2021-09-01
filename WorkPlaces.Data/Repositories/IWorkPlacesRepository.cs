using System;
using System.Threading.Tasks;

namespace WorkPlaces.Data.Repositories
{
    public interface IWorkPlacesRepository : IDisposable
    {
        Task<bool> ExistsAsync(int workPlaceId);
    }
}
