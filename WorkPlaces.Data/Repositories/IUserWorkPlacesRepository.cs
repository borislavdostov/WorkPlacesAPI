using System;
using System.Linq;
using System.Threading.Tasks;
using WorkPlaces.Data.Entities;

namespace WorkPlaces.Data.Repositories
{
    public interface IUserWorkPlacesRepository : IDisposable
    {
        IQueryable<UserWorkplace> GetAll();

        Task<UserWorkplace> GetAsync(int userWorkPlaceId);

        Task AddAsync(UserWorkplace userWorkPlace);

        void Update(UserWorkplace userWorkPlace);

        void Delete(UserWorkplace userWorkPlace);

        Task<bool> ExistsAsync(int userWorkPlaceId);

        Task SaveChangesAsync();
    }
}
