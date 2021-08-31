using System;
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
        private readonly IUserWorkPlacesRepository userWorkPlacesRepository;
        private readonly IUsersRepository usersRepository;
        private readonly IWorkPlacesRepository workPlacesRepository;

        public UserWorkPlacesService(
            IUserWorkPlacesRepository userWorkPlacesRepository,
            IUsersRepository usersRepository,
            IWorkPlacesRepository workPlacesRepository)
        {
            this.userWorkPlacesRepository = userWorkPlacesRepository;
            this.usersRepository = usersRepository;
            this.workPlacesRepository = workPlacesRepository;
        }

        public IEnumerable<UserWorkPlaceDTO> GetUserWorkPlaces()
        {
            var userWorkPlaces = userWorkPlacesRepository.GetAll();
            return userWorkPlaces.Select(uwp => new UserWorkPlaceDTO
            {
                Id = uwp.Id,
                User = $"{uwp.User.FirstName} {uwp.User.LastName}",
                WorkPlace = uwp.WorkPlace.Name,
                FromDate = uwp.FromDate,
                ToDate = uwp.ToDate
            }).ToList();
        }

        public UserWorkPlaceDTO GetUserWorkPlace(int userWorkPlaceId)
        {
            var userWorkPlaceEntity = userWorkPlacesRepository.GetUserWorkPlace(userWorkPlaceId);

            return new UserWorkPlaceDTO
            {
                Id = userWorkPlaceEntity.Id,
                User = $"{userWorkPlaceEntity.User.FirstName} {userWorkPlaceEntity.User.LastName}",
                WorkPlace = userWorkPlaceEntity.WorkPlace.Name,
                FromDate = userWorkPlaceEntity.FromDate,
                ToDate = userWorkPlaceEntity.ToDate
            };
        }

        public async Task<UserWorkPlaceDTO> CreateUserWorkPlaceAsync(UserWorkPlaceForManipulationDTO userWorkPlace)
        {
            var userWorkPlaceEntity = new UserWorkPlace
            {
                UserId = userWorkPlace.UserId,
                WorkPlaceId = userWorkPlace.WorkPlaceId,
                FromDate = userWorkPlace.FromDate,
                ToDate = userWorkPlace.ToDate
            };

            await userWorkPlacesRepository.AddUserWorkPlaceAsync(userWorkPlaceEntity);
            await userWorkPlacesRepository.SaveChangesAsync();

            var addedUserWorkPlaceEntity = userWorkPlacesRepository.GetUserWorkPlace(userWorkPlaceEntity.Id);

            return new UserWorkPlaceDTO
            {
                Id = userWorkPlaceEntity.Id,
                User = $"{addedUserWorkPlaceEntity.User.FirstName} {addedUserWorkPlaceEntity.User.LastName}",
                WorkPlace = addedUserWorkPlaceEntity.WorkPlace.Name,
                FromDate = userWorkPlace.FromDate,
                ToDate = userWorkPlace.ToDate
            };
        }

        public async Task UpdateUserWorkPlace(int userWorkPlaceId, UserWorkPlaceForManipulationDTO userWorkPlace)
        {
            var userWorkPlaceEntity = userWorkPlacesRepository.GetUserWorkPlace(userWorkPlaceId);

            userWorkPlaceEntity.UserId = userWorkPlace.UserId;
            userWorkPlaceEntity.WorkPlaceId = userWorkPlace.WorkPlaceId;
            userWorkPlaceEntity.FromDate = userWorkPlace.FromDate;
            userWorkPlaceEntity.ToDate = userWorkPlace.ToDate;

            userWorkPlacesRepository.UpdateUserWorkPlace(userWorkPlaceEntity);
            await userWorkPlacesRepository.SaveChangesAsync();
        }

        public async Task DeleteUserWorkPlace(int userId)
        {
            var userWorkPlaceEntity = userWorkPlacesRepository.GetUserWorkPlace(userId);
            userWorkPlacesRepository.DeleteUserWorkPlace(userWorkPlaceEntity);
            await userWorkPlacesRepository.SaveChangesAsync();
        }

        public bool UserWorkPlaceExists(int userWorkPlaceId)
        {
            return userWorkPlacesRepository.UserWorkPlaceExists(userWorkPlaceId);
        }
    }
}
