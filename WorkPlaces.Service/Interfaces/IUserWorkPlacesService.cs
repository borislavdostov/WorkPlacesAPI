using System.Collections.Generic;
using System.Threading.Tasks;
using WorkPlaces.DataModel.Models;

namespace WorkPlaces.Service.Interfaces
{
    public interface IUserWorkPlacesService
    {
        IEnumerable<UserWorkPlaceDTO> GetUserWorkPlaces();

        Task<UserWorkPlaceDTO> GetUserWorkPlaceAsync(int userWorkPlaceId);

        Task<UserWorkPlaceDTO> CreateUserWorkPlaceAsync(UserWorkPlaceForManipulationDTO userWorkPlace);

        Task UpdateUserWorkPlaceAsync(int userWorkPlaceId, UserWorkPlaceForManipulationDTO userWorkPlace);

        Task DeleteUserWorkPlaceAsync(int userWorkPlaceId);

        Task<bool> UserWorkPlaceExistsAsync(int userWorkPlaceId);
    }
}
