using System.Linq;
using System.Threading.Tasks;
using WorkPlaces.Data.Entities;

namespace WorkPlaces.Data.Repositories
{
    public interface IUserWorkPlacesRepository
    {
        IQueryable<UserWorkPlace> GetAll();

        UserWorkPlace GetUserWorkPlace(int userWorkPlaceId);

        Task AddUserWorkPlaceAsync(UserWorkPlace userWorkPlace);

        void UpdateUserWorkPlace(UserWorkPlace userWorkPlace);

        void DeleteUserWorkPlace(UserWorkPlace userWorkPlace);

        bool UserWorkPlaceExists(int userWorkPlaceId);

        Task SaveChangesAsync();
    }
}
