using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WorkPlaces.Data.Entities;

namespace WorkPlaces.Data.Repositories
{
    public class WorkplacesRepository : IWorkplacesRepository
    {
        private readonly ApplicationDbContext context;
        private readonly DbSet<Workplace> dbSet;

        public WorkplacesRepository(ApplicationDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            dbSet = context.Set<Workplace>();
        }

        public IQueryable<Workplace> GetAll()
        {
            return dbSet;
        }

        public Task<bool> ExistsAsync(int workplaceId)
        {
            return dbSet.AnyAsync(wp => wp.Id == workplaceId && wp.DeletedAt == null);
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
