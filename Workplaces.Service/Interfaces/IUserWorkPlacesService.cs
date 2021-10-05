using System.Collections.Generic;
using System.Threading.Tasks;
using Workplaces.DataModel.Models;

namespace Workplaces.Service.Interfaces
{
    public interface IUserWorkplacesService
    {
        IEnumerable<UserWorkplaceDTO> GetUserWorkplaces();

        UserWorkplaceOptionsDTO GetUserWorkplaceOptions();

        Task<UserWorkplaceForManipulationDTO> GetUserWorkplaceAsync(int userWorkplaceId);

        Task<UserWorkplaceDTO> CreateUserWorkplaceAsync(UserWorkplaceForManipulationDTO userWorkplace);

        Task UpdateUserWorkplaceAsync(int userWorkplaceId, UserWorkplaceForManipulationDTO userWorkplace);

        Task DeleteUserWorkplaceAsync(int userWorkplaceId);

        Task<bool> UserWorkplaceExistsAsync(int userWorkplaceId);
    }
}
