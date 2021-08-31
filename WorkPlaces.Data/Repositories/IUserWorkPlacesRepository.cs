using System.Linq;
using System.Threading.Tasks;
using WorkPlaces.Data.Entities;

namespace WorkPlaces.Data.Repositories
{
    public interface IUserWorkPlacesRepository
    {
        IQueryable<UserWorkPlace> GetAll();

        UserWorkPlace Get(int userWorkPlaceId);

        Task AddAsync(UserWorkPlace userWorkPlace);

        void Update(UserWorkPlace userWorkPlace);

        void Delete(UserWorkPlace userWorkPlace);

        bool Exists(int userWorkPlaceId);

        Task SaveChangesAsync();
    }
}
