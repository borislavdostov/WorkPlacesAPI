using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WorkPlaces.Data.Entities;

namespace WorkPlaces.Data.Repositories
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

        public Task<UserWorkplace> GetAsync(int userWorkPlaceId)
        {
            return dbSet
                .Include(u => u.User)
                .Include(u => u.Workplace)
                .FirstOrDefaultAsync(u => u.Id == userWorkPlaceId && u.DeletedAt == null);
        }

        public Task AddAsync(UserWorkplace userWorkPlace)
        {
            userWorkPlace.CreatedAt = DateTime.Now;
            return dbSet.AddAsync(userWorkPlace).AsTask();
        }

        public void Update(UserWorkplace userWorkPlace)
        {
            var entry = context.Entry(userWorkPlace);
            if (entry.State == EntityState.Detached)
            {
                dbSet.Attach(userWorkPlace);
            }

            userWorkPlace.UpdatedAt = DateTime.Now;
            entry.State = EntityState.Modified;
        }

        public void Delete(UserWorkplace userWorkPlace)
        {
            userWorkPlace.DeletedAt = DateTime.Now;
            Update(userWorkPlace);
        }

        public Task<bool> ExistsAsync(int userWorkPlaceId)
        {
            return dbSet.AnyAsync(uwp => uwp.Id == userWorkPlaceId && uwp.DeletedAt == null);
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
