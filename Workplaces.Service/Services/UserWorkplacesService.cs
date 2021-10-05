using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workplaces.Data.Entities;
using Workplaces.Data.Interfaces;
using Workplaces.Data.Repositories;
using Workplaces.DataModel.Models;
using Workplaces.Service.Interfaces;

namespace Workplaces.Service.Services
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
            this.userWorkplacesRepository = userWorkplacesRepository ?? throw new ArgumentNullException();
            this.usersRepository = usersRepository ?? throw new ArgumentNullException();
            this.workplacesRepository = workplacesRepository ?? throw new ArgumentNullException();
        }

        public IEnumerable<UserWorkplaceDTO> GetUserWorkplaces()
        {
            var userWorkplaces = userWorkplacesRepository.GetAll();

            return userWorkplaces.Select(uw => new UserWorkplaceDTO
            {
                Id = uw.Id,
                User = uw.User.FullName,
                Workplace = uw.Workplace.Name,
                FromDate = uw.FromDate,
                ToDate = uw.ToDate
            }).ToList();
        }

        public UserWorkplaceOptionsDTO GetUserWorkplaceOptions()
        {
            var users = usersRepository.GetAll().Select(u => new UserDropdownDTO
            {
                Id = u.Id,
                Name = u.FullName
            }).ToList();

            var workplaces = workplacesRepository.GetAll().Select(w => new WorkplaceDropdownDTO
            {
                Id = w.Id,
                Name = w.Name
            }).ToList();

            return new UserWorkplaceOptionsDTO
            {
                Users = users,
                Workplaces = workplaces
            };
        }

        public async Task<UserWorkplaceForManipulationDTO> GetUserWorkplaceAsync(int userWorkplaceId)
        {
            var userWorkplaceEntity = await userWorkplacesRepository.GetAsync(userWorkplaceId);

            return new UserWorkplaceForManipulationDTO
            {
                UserId = userWorkplaceEntity.UserId,
                WorkplaceId = userWorkplaceEntity.WorkplaceId,
                FromDate = userWorkplaceEntity.FromDate,
                ToDate = userWorkplaceEntity.ToDate
            };
        }

        public async Task<UserWorkplaceDTO> CreateUserWorkplaceAsync(UserWorkplaceForManipulationDTO userWorkplace)
        {
            var userWorkplaceEntity = new UserWorkplace
            {
                UserId = userWorkplace.UserId,
                WorkplaceId = userWorkplace.WorkplaceId,
                FromDate = userWorkplace.FromDate,
                ToDate = userWorkplace.ToDate
            };

            await userWorkplacesRepository.AddAsync(userWorkplaceEntity);
            await userWorkplacesRepository.SaveChangesAsync();

            var addedUserWorkplaceEntity = await userWorkplacesRepository.GetAsync(userWorkplaceEntity.Id);

            return new UserWorkplaceDTO
            {
                Id = userWorkplaceEntity.Id,
                User = addedUserWorkplaceEntity.User.FullName,
                Workplace = addedUserWorkplaceEntity.Workplace.Name,
                FromDate = userWorkplace.FromDate,
                ToDate = userWorkplace.ToDate
            };
        }

        public async Task UpdateUserWorkplaceAsync(int userWorkplaceId, UserWorkplaceForManipulationDTO userWorkplace)
        {
            var userWorkplaceEntity = await userWorkplacesRepository.GetAsync(userWorkplaceId);

            userWorkplaceEntity.UserId = userWorkplace.UserId;
            userWorkplaceEntity.WorkplaceId = userWorkplace.WorkplaceId;
            userWorkplaceEntity.FromDate = userWorkplace.FromDate;
            userWorkplaceEntity.ToDate = userWorkplace.ToDate;

            userWorkplacesRepository.Update(userWorkplaceEntity);
            await userWorkplacesRepository.SaveChangesAsync();
        }

        public async Task DeleteUserWorkplaceAsync(int userId)
        {
            var userWorkplaceEntity = await userWorkplacesRepository.GetAsync(userId);

            userWorkplacesRepository.Delete(userWorkplaceEntity);
            await userWorkplacesRepository.SaveChangesAsync();
        }

        public async Task<bool> UserWorkplaceExistsAsync(int userWorkplaceId)
        {
            return await userWorkplacesRepository.ExistsAsync(userWorkplaceId);
        }
    }
}
