using System.Linq;
using System.Threading.Tasks;
using WorkPlaces.Data.Entities;

namespace WorkPlaces.Data.Interfaces
{
    public interface IUsersRepository
    {
        User GetUser(int userId);

        IQueryable<User> GetAll();

        Task AddUserAsync(User user);

        void UpdateUser(User user);

        void DeleteUser(User user);

        bool Exists(int userId);
    }
}
