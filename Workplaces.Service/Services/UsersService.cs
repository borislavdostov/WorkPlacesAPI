using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workplaces.Data.Entities;
using Workplaces.Data.Interfaces;
using Workplaces.DataModel.Models;
using Workplaces.Service.Interfaces;

namespace Workplaces.Service.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository usersRepository;

        public UsersService(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository ?? throw new ArgumentNullException();
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            var allUsers = usersRepository.GetAll();

            return allUsers.Select(u => new UserDTO
            {
                Id = u.Id,
                Name = u.FullName,
                Email = u.Email,
                Age = u.Age
            }).ToList();
        }

        public async Task<UserForManipulationDTO> GetUserAsync(int userId)
        {
            var userEntity = await usersRepository.GetAsync(userId);

            return new UserForManipulationDTO
            {
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                Email = userEntity.Email,
                DateOfBirth = userEntity.DateOfBirth
            };
        }

        public async Task<UserDTO> CreateUserAsync(UserForManipulationDTO user)
        {
            var userEntity = new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth
            };

            await usersRepository.AddAsync(userEntity);
            await usersRepository.SaveChangesAsync();

            return new UserDTO
            {
                Id = userEntity.Id,
                Name = userEntity.FullName,
                Email = userEntity.Email,
                Age = userEntity.Age
            };
        }

        public async Task UpdateUserAsync(int userId, UserForManipulationDTO user)
        {
            var userEntity = await usersRepository.GetAsync(userId);

            userEntity.FirstName = user.FirstName;
            userEntity.LastName = user.LastName;
            userEntity.Email = user.Email;
            userEntity.DateOfBirth = user.DateOfBirth;

            usersRepository.Update(userEntity);
            await usersRepository.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int userId)
        {
            var user = await usersRepository.GetAsync(userId);

            usersRepository.Delete(user);
            await usersRepository.SaveChangesAsync();
        }

        public async Task<bool> UserExistsAsync(int userId)
        {
            return await usersRepository.ExistsAsync(userId);
        }
    }
}
