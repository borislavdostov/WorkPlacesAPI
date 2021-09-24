using System.Collections.Generic;
using System.Threading.Tasks;
using WorkPlaces.DataModel.Models;

namespace WorkPlaces.Service.Interfaces
{
    public interface IUserWorkPlacesService
    {
        IEnumerable<UserWorkPlaceDTO> GetUserWorkPlaces();

        UserWorkPlaceOptionsDTO GetUserWorkPlaceOptions();

        Task<UserWorkplaceForManipulationDTO> GetUserWorkPlaceAsync(int userWorkPlaceId);

        Task<UserWorkPlaceDTO> CreateUserWorkPlaceAsync(UserWorkplaceForManipulationDTO userWorkPlace);

        Task UpdateUserWorkPlaceAsync(int userWorkPlaceId, UserWorkplaceForManipulationDTO userWorkPlace);

        Task DeleteUserWorkPlaceAsync(int userWorkPlaceId);

        Task<bool> UserWorkPlaceExistsAsync(int userWorkPlaceId);
    }
}
