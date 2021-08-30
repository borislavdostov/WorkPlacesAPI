using System.Collections.Generic;
using System.Threading.Tasks;
using WorkPlaces.DataModel.Models;

namespace WorkPlaces.Service.Interfaces
{
    public interface IUserWorkPlacesService
    {
        IEnumerable<UserWorkPlaceDTO> GetUserWorkPlaces();

        UserWorkPlaceDTO GetUserWorkPlace(int userWorkPlaceId);

        Task<UserWorkPlaceDTO> CreateUserWorkPlaceAsync(UserWorkPlaceForCreationDTO user);

        void UpdateUserWorkPlace(UserWorkPlaceForCreationDTO user);

        void DeleteUserWorkPlace(int userId);
    }
}
