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
            this.context = context;
            dbSet = context.Set<User>();
        }

        public IQueryable<User> GetAll()
        {
            return context.Users
                .Include(u => u.WorkPlaces)
                .Where(u => u.DeletedAt == null);
        }

        public User GetUser(int userId)
        {
            return context.Users
                .Include(u => u.WorkPlaces)
                .FirstOrDefault(u => u.Id == userId && u.DeletedAt == null);
        }

        public async Task AddUserAsync(User user)
        {
            user.CreatedAt = DateTime.Now;
            await context.Users.AddAsync(user);
        }

        public void UpdateUser(User user)
        {
            var entry = context.Entry(user);
            if (entry.State == EntityState.Detached)
            {
                dbSet.Attach(user);
            }

            user.UpdatedAt = DateTime.Now;
            entry.State = EntityState.Modified;
        }

        public void DeleteUser(User user)
        {
            user.DeletedAt = DateTime.Now;
            UpdateUser(user);
        }

        public bool UserExists(int userId)
        {
            return context.Users.Any(u => u.Id == userId && u.DeletedAt == null);
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
