using System;
using System.Linq;
using System.Threading.Tasks;
using Workplaces.Data.Entities;

namespace Workplaces.Data.Interfaces
{
    public interface IUsersRepository : IDisposable
    {
        IQueryable<User> GetAll();

        Task<User> GetAsync(int userId);

        Task AddAsync(User user);

        void Update(User user);

        void Delete(User user);

        Task<bool> ExistsAsync(int userId);

        Task SaveChangesAsync();
    }
}
