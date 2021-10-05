using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Workplaces.Data.Entities;

namespace Workplaces.Data.Repositories
{
    public class UserWorkplacesRepository : IUserWorkplacesRepository
    {
        private readonly ApplicationDbContext context;
        private readonly DbSet<UserWorkplace> dbSet;

        public UserWorkplacesRepository(ApplicationDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            dbSet = context.Set<UserWorkplace>();
        }

        public IQueryable<UserWorkplace> GetAll()
        {
            return dbSet
                .Include(u => u.User)
                .Include(u => u.Workplace)
                .Where(u => u.DeletedAt == null);
        }

        public Task<UserWorkplace> GetAsync(int userWorkplaceId)
        {
            return dbSet
                .Include(u => u.User)
                .Include(u => u.Workplace)
                .FirstOrDefaultAsync(u => u.Id == userWorkplaceId && u.DeletedAt == null);
        }

        public Task AddAsync(UserWorkplace userWorkplace)
        {
            userWorkplace.CreatedAt = DateTime.Now;
            return dbSet.AddAsync(userWorkplace).AsTask();
        }

        public void Update(UserWorkplace userWorkplace)
        {
            var entry = context.Entry(userWorkplace);
            if (entry.State == EntityState.Detached)
            {
                dbSet.Attach(userWorkplace);
            }

            userWorkplace.UpdatedAt = DateTime.Now;
            entry.State = EntityState.Modified;
        }

        public void Delete(UserWorkplace userWorkplace)
        {
            userWorkplace.DeletedAt = DateTime.Now;
            Update(userWorkplace);
        }

        public Task<bool> ExistsAsync(int userWorkplaceId)
        {
            return dbSet.AnyAsync(uw => uw.Id == userWorkplaceId && uw.DeletedAt == null);
        }

        public Task SaveChangesAsync()
        {
            return context.SaveChangesAsync();
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
