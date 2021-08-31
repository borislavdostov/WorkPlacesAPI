using System;
using System.Linq;
using System.Threading.Tasks;
using WorkPlaces.Data.Entities;

namespace WorkPlaces.Data.Interfaces
{
    public interface IUsersRepository : IDisposable
    {
        IQueryable<User> GetAll();

        User Get(int userId);

        Task AddAsync(User user);

        void Update(User user);

        void Delete(User user);

        bool Exists(int userId);

        Task SaveChangesAsync();
    }
}
