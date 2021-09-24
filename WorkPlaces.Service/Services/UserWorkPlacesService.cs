using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkPlaces.Data.Entities;
using WorkPlaces.Data.Interfaces;
using WorkPlaces.Data.Repositories;
using WorkPlaces.DataModel.Models;
using WorkPlaces.Service.Interfaces;

namespace WorkPlaces.Service.Services
{
    public class UserWorkPlacesService : IUserWorkPlacesService
    {
        private readonly IUserWorkplacesRepository userWorkPlacesRepository;
        private readonly IUsersRepository usersRepository;
        private readonly IWorkplacesRepository workPlacesRepository;

        public UserWorkPlacesService(
            IUserWorkplacesRepository userWorkPlacesRepository,
            IUsersRepository usersRepository,
            IWorkplacesRepository workPlacesRepository)
        {
            this.userWorkPlacesRepository = userWorkPlacesRepository;
            this.usersRepository = usersRepository;
            this.workPlacesRepository = workPlacesRepository;
        }

        public IEnumerable<UserWorkplaceDTO> GetUserWorkPlaces()
        {
            var userWorkPlaces = userWorkPlacesRepository.GetAll();

            return userWorkPlaces.Select(uwp => new UserWorkplaceDTO
            {
                Id = uwp.Id,
                User = uwp.User.FullName,
                Workplace = uwp.Workplace.Name,
                FromDate = uwp.FromDate,
                ToDate = uwp.ToDate
            }).ToList();
        }

        public UserWorkPlaceOptionsDTO GetUserWorkPlaceOptions()
        {
            var users = usersRepository.GetAll().Select(u => new UserDropdownDTO
            {
                Id = u.Id,
                Name = u.FullName
            }).ToList();

            var workPlaces = workPlacesRepository.GetAll().Select(wp => new WorkPlaceDropdownDTO
            {
                Id = wp.Id,
                Name = wp.Name
            }).ToList();


            return new UserWorkPlaceOptionsDTO
            {
                Users = users,
                WorkPlaces = workPlaces
            };
        }

        public async Task<UserWorkplaceForManipulationDTO> GetUserWorkPlaceAsync(int userWorkPlaceId)
        {
            var userWorkPlaceEntity = await userWorkPlacesRepository.GetAsync(userWorkPlaceId);

            return new UserWorkplaceForManipulationDTO
            {
                UserId = userWorkPlaceEntity.UserId,
                WorkplaceId = userWorkPlaceEntity.WorkplaceId,
                FromDate = userWorkPlaceEntity.FromDate,
                ToDate = userWorkPlaceEntity.ToDate
            };
        }

        public async Task<UserWorkplaceDTO> CreateUserWorkPlaceAsync(UserWorkplaceForManipulationDTO userWorkPlace)
        {
            var userWorkPlaceEntity = new UserWorkplace
            {
                UserId = userWorkPlace.UserId,
                WorkplaceId = userWorkPlace.WorkplaceId,
                FromDate = userWorkPlace.FromDate,
                ToDate = userWorkPlace.ToDate
            };

            await userWorkPlacesRepository.AddAsync(userWorkPlaceEntity);
            await userWorkPlacesRepository.SaveChangesAsync();

            //TODO: Load relations instead of loading the added entity
            var addedUserWorkPlaceEntity = await userWorkPlacesRepository.GetAsync(userWorkPlaceEntity.Id);

            return new UserWorkplaceDTO
            {
                Id = userWorkPlaceEntity.Id,
                User = addedUserWorkPlaceEntity.User.FullName,
                Workplace = addedUserWorkPlaceEntity.Workplace.Name,
                FromDate = userWorkPlace.FromDate,
                ToDate = userWorkPlace.ToDate
            };
        }

        public async Task UpdateUserWorkPlaceAsync(int userWorkPlaceId, UserWorkplaceForManipulationDTO userWorkPlace)
        {
            var userWorkPlaceEntity = await userWorkPlacesRepository.GetAsync(userWorkPlaceId);

            userWorkPlaceEntity.UserId = userWorkPlace.UserId;
            userWorkPlaceEntity.WorkplaceId = userWorkPlace.WorkplaceId;
            userWorkPlaceEntity.FromDate = userWorkPlace.FromDate;
            userWorkPlaceEntity.ToDate = userWorkPlace.ToDate;

            userWorkPlacesRepository.Update(userWorkPlaceEntity);
            await userWorkPlacesRepository.SaveChangesAsync();
        }

        public async Task DeleteUserWorkPlaceAsync(int userId)
        {
            var userWorkPlaceEntity = await userWorkPlacesRepository.GetAsync(userId);

            userWorkPlacesRepository.Delete(userWorkPlaceEntity);
            await userWorkPlacesRepository.SaveChangesAsync();
        }

        public async Task<bool> UserWorkPlaceExistsAsync(int userWorkPlaceId)
        {
            return await userWorkPlacesRepository.ExistsAsync(userWorkPlaceId);
        }
    }
}
