using System.Collections.Generic;
using System.Threading.Tasks;
using WorkPlaces.DataModel.Models;

namespace WorkPlaces.Service.Interfaces
{
    public interface IUserWorkPlacesService
    {
        IEnumerable<UserWorkplaceDTO> GetUserWorkPlaces();

        UserWorkplaceOptionsDTO GetUserWorkPlaceOptions();

        Task<UserWorkplaceForManipulationDTO> GetUserWorkPlaceAsync(int userWorkPlaceId);

        Task<UserWorkplaceDTO> CreateUserWorkPlaceAsync(UserWorkplaceForManipulationDTO userWorkPlace);

        Task UpdateUserWorkPlaceAsync(int userWorkPlaceId, UserWorkplaceForManipulationDTO userWorkPlace);

        Task DeleteUserWorkPlaceAsync(int userWorkPlaceId);

        Task<bool> UserWorkPlaceExistsAsync(int userWorkPlaceId);
    }
}
