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
    public class UserWorkplacesService : IUserWorkplacesService
    {
        private readonly IUserWorkplacesRepository userWorkplacesRepository;
        private readonly IUsersRepository usersRepository;
        private readonly IWorkplacesRepository workplacesRepository;

        public UserWorkplacesService(
            IUserWorkplacesRepository userWorkplacesRepository,
            IUsersRepository usersRepository,
            IWorkplacesRepository workplacesRepository)
        {
            this.userWorkplacesRepository = userWorkplacesRepository;
            this.usersRepository = usersRepository;
            this.workplacesRepository = workplacesRepository;
        }

        public IEnumerable<UserWorkplaceDTO> GetUserWorkplaces()
        {
            var userWorkPlaces = userWorkplacesRepository.GetAll();

            return userWorkPlaces.Select(uwp => new UserWorkplaceDTO
            {
                Id = uwp.Id,
                User = uwp.User.FullName,
                Workplace = uwp.Workplace.Name,
                FromDate = uwp.FromDate,
                ToDate = uwp.ToDate
            }).ToList();
        }

        public UserWorkplaceOptionsDTO GetUserWorkplaceOptions()
        {
            var users = usersRepository.GetAll().Select(u => new UserDropdownDTO
            {
                Id = u.Id,
                Name = u.FullName
            }).ToList();

            var workPlaces = workplacesRepository.GetAll().Select(wp => new WorkplaceDropdownDTO
            {
                Id = wp.Id,
                Name = wp.Name
            }).ToList();


            return new UserWorkplaceOptionsDTO
            {
                Users = users,
                Workplaces = workPlaces
            };
        }

        public async Task<UserWorkplaceForManipulationDTO> GetUserWorkplaceAsync(int userWorkPlaceId)
        {
            var userWorkPlaceEntity = await userWorkplacesRepository.GetAsync(userWorkPlaceId);

            return new UserWorkplaceForManipulationDTO
            {
                UserId = userWorkPlaceEntity.UserId,
                WorkplaceId = userWorkPlaceEntity.WorkplaceId,
                FromDate = userWorkPlaceEntity.FromDate,
                ToDate = userWorkPlaceEntity.ToDate
            };
        }

        public async Task<UserWorkplaceDTO> CreateUserWorkplaceAsync(UserWorkplaceForManipulationDTO userWorkPlace)
        {
            var userWorkPlaceEntity = new UserWorkplace
            {
                UserId = userWorkPlace.UserId,
                WorkplaceId = userWorkPlace.WorkplaceId,
                FromDate = userWorkPlace.FromDate,
                ToDate = userWorkPlace.ToDate
            };

            await userWorkplacesRepository.AddAsync(userWorkPlaceEntity);
            await userWorkplacesRepository.SaveChangesAsync();

            //TODO: Load relations instead of loading the added entity
            var addedUserWorkPlaceEntity = await userWorkplacesRepository.GetAsync(userWorkPlaceEntity.Id);

            return new UserWorkplaceDTO
            {
                Id = userWorkPlaceEntity.Id,
                User = addedUserWorkPlaceEntity.User.FullName,
                Workplace = addedUserWorkPlaceEntity.Workplace.Name,
                FromDate = userWorkPlace.FromDate,
                ToDate = userWorkPlace.ToDate
            };
        }

        public async Task UpdateUserWorkplaceAsync(int userWorkPlaceId, UserWorkplaceForManipulationDTO userWorkPlace)
        {
            var userWorkPlaceEntity = await userWorkplacesRepository.GetAsync(userWorkPlaceId);

            userWorkPlaceEntity.UserId = userWorkPlace.UserId;
            userWorkPlaceEntity.WorkplaceId = userWorkPlace.WorkplaceId;
            userWorkPlaceEntity.FromDate = userWorkPlace.FromDate;
            userWorkPlaceEntity.ToDate = userWorkPlace.ToDate;

            userWorkplacesRepository.Update(userWorkPlaceEntity);
            await userWorkplacesRepository.SaveChangesAsync();
        }

        public async Task DeleteUserWorkplaceAsync(int userId)
        {
            var userWorkPlaceEntity = await userWorkplacesRepository.GetAsync(userId);

            userWorkplacesRepository.Delete(userWorkPlaceEntity);
            await userWorkplacesRepository.SaveChangesAsync();
        }

        public async Task<bool> UserWorkplaceExistsAsync(int userWorkPlaceId)
        {
            return await userWorkplacesRepository.ExistsAsync(userWorkPlaceId);
        }
    }
}
