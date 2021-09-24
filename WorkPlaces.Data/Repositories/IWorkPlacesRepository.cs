using System;
using System.Linq;
using System.Threading.Tasks;
using WorkPlaces.Data.Entities;

namespace WorkPlaces.Data.Repositories
{
    public interface IWorkPlacesRepository : IDisposable
    {
        IQueryable<Workplace> GetAll();

        Task<bool> ExistsAsync(int workPlaceId);
    }
}
