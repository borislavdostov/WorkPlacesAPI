using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Workplaces.Data.Entities;
using Workplaces.Data.Interfaces;

namespace Workplaces.Data.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationDbContext context;
        private readonly DbSet<User> dbSet;

        public UsersRepository(ApplicationDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            dbSet = context.Set<User>();
        }

        public IQueryable<User> GetAll()
        {
            return dbSet
                .Include(u => u.Workplaces)
                .Where(u => u.DeletedAt == null);
        }

        public Task<User> GetAsync(int userId)
        {
            return dbSet
                .Include(u => u.Workplaces)
                .FirstOrDefaultAsync(u => u.Id == userId && u.DeletedAt == null);
        }

        public Task AddAsync(User user)
        {
            user.CreatedAt = DateTime.Now;
            return dbSet.AddAsync(user).AsTask();
        }

        public void Update(User user)
        {
            var entry = context.Entry(user);
            if (entry.State == EntityState.Detached)
            {
                dbSet.Attach(user);
            }

            user.UpdatedAt = DateTime.Now;
            entry.State = EntityState.Modified;
        }

        public void Delete(User user)
        {
            user.DeletedAt = DateTime.Now;
            Update(user);
        }

        public Task<bool> ExistsAsync(int userId)
        {
            return dbSet.AnyAsync(u => u.Id == userId && u.DeletedAt == null);
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
