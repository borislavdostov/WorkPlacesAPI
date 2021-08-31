using System.Collections.Generic;
using System.Threading.Tasks;
using WorkPlaces.DataModel.Models;

namespace WorkPlaces.Service.Interfaces
{
    public interface IUserWorkPlacesService
    {
        IEnumerable<UserWorkPlaceDTO> GetUserWorkPlaces();

        UserWorkPlaceDTO GetUserWorkPlace(int userWorkPlaceId);

        Task<UserWorkPlaceDTO> CreateUserWorkPlaceAsync(UserWorkPlaceForManipulationDTO userWorkPlace);

        Task UpdateUserWorkPlace(int userWorkPlaceId, UserWorkPlaceForManipulationDTO userWorkPlace);

        Task DeleteUserWorkPlace(int userWorkPlaceId);

        bool UserWorkPlaceExists(int userWorkPlaceId);
    }
}
