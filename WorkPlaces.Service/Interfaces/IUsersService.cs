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

        void UpdateUser(UserForCreationDTO user);

        void DeleteUser(int userId);
    }
}
