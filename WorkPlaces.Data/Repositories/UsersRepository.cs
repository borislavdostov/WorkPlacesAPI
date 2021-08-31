using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WorkPlaces.Data.Entities;
using WorkPlaces.Data.Interfaces;

namespace WorkPlaces.Data.Repositories
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
            return context.Users
                .Include(u => u.WorkPlaces)
                .Where(u => u.DeletedAt == null);
        }

        public User Get(int userId)
        {
            return context.Users
                .Include(u => u.WorkPlaces)
                .FirstOrDefault(u => u.Id == userId && u.DeletedAt == null);
        }

        public async Task AddAsync(User user)
        {
            user.CreatedAt = DateTime.Now;
            await context.Users.AddAsync(user);
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

        public bool Exists(int userId)
        {
            return context.Users.Any(u => u.Id == userId && u.DeletedAt == null);
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
