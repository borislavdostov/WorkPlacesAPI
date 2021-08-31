using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WorkPlaces.Data.Entities;

namespace WorkPlaces.Data.Repositories
{
    public class UserWorkPlacesRepository : IUserWorkPlacesRepository
    {
        private readonly ApplicationDbContext context;
        private readonly DbSet<UserWorkPlace> dbSet;

        public UserWorkPlacesRepository(ApplicationDbContext context)
        {
            this.context = context;
            dbSet = context.Set<UserWorkPlace>();
        }

        public IQueryable<UserWorkPlace> GetAll()
        {
            return context.UserWorkPlaces
                .Include(u => u.User)
                .Include(u => u.WorkPlace)
                .Where(u => u.DeletedAt == null);
        }

        public UserWorkPlace GetUserWorkPlace(int userWorkPlaceId)
        {
            return context.UserWorkPlaces
                .Include(u => u.User)
                .Include(u => u.WorkPlace)
                .FirstOrDefault(u => u.Id == userWorkPlaceId && u.DeletedAt == null);
        }

        public async Task AddUserWorkPlaceAsync(UserWorkPlace userWorkPlace)
        {
            userWorkPlace.CreatedAt = DateTime.Now;
            await context.UserWorkPlaces.AddAsync(userWorkPlace);
        }

        public void UpdateUserWorkPlace(UserWorkPlace userWorkPlace)
        {
            var entry = context.Entry(userWorkPlace);
            if (entry.State == EntityState.Detached)
            {
                dbSet.Attach(userWorkPlace);
            }

            userWorkPlace.UpdatedAt = DateTime.Now;
            entry.State = EntityState.Modified;
        }

        public void DeleteUserWorkPlace(UserWorkPlace userWorkPlace)
        {
            userWorkPlace.DeletedAt = DateTime.Now;
            UpdateUserWorkPlace(userWorkPlace);
        }

        public bool UserWorkPlaceExists(int userWorkPlaceId)
        {
            return context.UserWorkPlaces.Any(uwp => uwp.Id == userWorkPlaceId && uwp.DeletedAt == null);
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
