using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace WorkPlaces.Data.Repositories
{
    public class WorkPlacesRepository : IWorkPlacesRepository
    {
        private readonly ApplicationDbContext context;

        public WorkPlacesRepository(ApplicationDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<bool> WorkPlaceExists(int workPlaceId)
        {
            return context.WorkPlaces.AnyAsync(wp => wp.Id == workPlaceId && wp.DeletedAt == null);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                context?.Dispose();
            }
        }
    }
}
