using System;
using System.Linq;
using System.Threading.Tasks;
using Workplaces.Data.Entities;

namespace Workplaces.Data.Repositories
{
    public interface IWorkplacesRepository : IDisposable
    {
        IQueryable<Workplace> GetAll();

        Task<bool> ExistsAsync(int workplaceId);
    }
}
