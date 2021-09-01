using System.Collections.Generic;
using System.Threading.Tasks;
using WorkPlaces.DataModel.Models;

namespace WorkPlaces.Service.Interfaces
{
    public interface IUsersService
    {
        IEnumerable<UserDTO> GetUsers();

        Task<UserDTO> GetUser(int userId);

        Task<UserDTO> CreateUserAsync(UserForManipulationDTO user);

        Task UpdateUser(int userId, UserForManipulationDTO user);

        Task DeleteUser(int userId);

        Task<bool> UserExists(int userId);
    }
}
