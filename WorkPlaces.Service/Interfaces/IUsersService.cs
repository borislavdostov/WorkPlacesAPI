using System.Collections.Generic;
using System.Threading.Tasks;
using WorkPlaces.DataModel.Models;

namespace WorkPlaces.Service.Interfaces
{
    public interface IUsersService
    {
        IEnumerable<UserDTO> GetUsers();

        UserDTO GetUser(int userId);

        Task<UserDTO> CreateUserAsync(UserForCreationDTO user);

        Task UpdateUser(int userId, UserForUpdateDTO user);

        Task DeleteUser(int userId);

        bool UserExists(int userId);
    }
}
