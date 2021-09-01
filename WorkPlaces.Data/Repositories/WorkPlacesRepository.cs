using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using WorkPlaces.Data.Entities;

namespace WorkPlaces.Data.Repositories
{
    public class WorkPlacesRepository : IWorkPlacesRepository
    {
        private readonly ApplicationDbContext context;
        private readonly DbSet<WorkPlace> dbSet;

        public WorkPlacesRepository(ApplicationDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            dbSet = context.Set<WorkPlace>();
        }

        public Task<bool> ExistsAsync(int workPlaceId)
        {
            return dbSet.AnyAsync(wp => wp.Id == workPlaceId && wp.DeletedAt == null);
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
