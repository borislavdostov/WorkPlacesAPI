using System;
using System.Threading.Tasks;

namespace WorkPlaces.Data.Repositories
{
    public interface IWorkPlacesRepository : IDisposable
    {
        Task<bool> WorkPlaceExists(int workPlaceId);
    }
}
