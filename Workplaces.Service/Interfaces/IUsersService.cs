using System.Collections.Generic;
using System.Threading.Tasks;
using Workplaces.DataModel.Models;

namespace Workplaces.Service.Interfaces
{
    public interface IUsersService
    {
        IEnumerable<UserDTO> GetUsers();

        Task<UserForManipulationDTO> GetUserAsync(int userId);

        Task<UserDTO> CreateUserAsync(UserForManipulationDTO user);

        Task UpdateUserAsync(int userId, UserForManipulationDTO user);

        Task DeleteUserAsync(int userId);

        Task<bool> UserExistsAsync(int userId);
    }
}
