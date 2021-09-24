using System.Collections.Generic;
using System.Threading.Tasks;
using WorkPlaces.DataModel.Models;

namespace WorkPlaces.Service.Interfaces
{
    public interface IUserWorkplacesService
    {
        IEnumerable<UserWorkplaceDTO> GetUserWorkPlaces();

        UserWorkplaceOptionsDTO GetUserWorkPlaceOptions();

        Task<UserWorkplaceForManipulationDTO> GetUserWorkPlaceAsync(int userWorkplaceId);

        Task<UserWorkplaceDTO> CreateUserWorkPlaceAsync(UserWorkplaceForManipulationDTO userWorkplace);

        Task UpdateUserWorkPlaceAsync(int userWorkplaceId, UserWorkplaceForManipulationDTO userWorkplace);

        Task DeleteUserWorkPlaceAsync(int userWorkplaceId);

        Task<bool> UserWorkPlaceExistsAsync(int userWorkplaceId);
    }
}
