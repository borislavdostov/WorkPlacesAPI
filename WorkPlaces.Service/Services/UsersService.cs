using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkPlaces.Data.Entities;
using WorkPlaces.Data.Interfaces;
using WorkPlaces.DataModel.Models;
using WorkPlaces.Extensions;
using WorkPlaces.Service.Interfaces;

namespace WorkPlaces.Service.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository usersRepository;

        public UsersService(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            var allUsers = usersRepository.GetAll();

            return allUsers.Select(user => new UserDTO
            {
                Id = user.Id,
                Name = $"{user.FirstName} { user.LastName}",
                Email = user.Email,
                Age = user.DateOfBirth.GetAge()
            }).ToList();
        }

        public UserDTO GetUser(int userId)
        {
            var userEntity = usersRepository.Get(userId);

            return new UserDTO
            {
                Id = userEntity.Id,
                Name = $"{userEntity.FirstName} {userEntity.LastName}",
                Email = userEntity.Email,
                Age = userEntity.DateOfBirth.GetAge()
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
                Name = $"{userEntity.FirstName} {userEntity.LastName}",
                Email = userEntity.Email,
                Age = userEntity.DateOfBirth.GetAge()
            };
        }

        public async Task UpdateUser(int userId, UserForManipulationDTO user)
        {
            var userEntity = usersRepository.Get(userId);

            userEntity.FirstName = user.FirstName;
            userEntity.LastName = user.LastName;
            userEntity.Email = user.Email;
            userEntity.DateOfBirth = user.DateOfBirth;

            usersRepository.Update(userEntity);
            await usersRepository.SaveChangesAsync();
        }

        public async Task DeleteUser(int userId)
        {
            var user = usersRepository.Get(userId);

            usersRepository.Delete(user);
            await usersRepository.SaveChangesAsync();
        }

        public bool UserExists(int userId)
        {
            return usersRepository.Exists(userId);
        }
    }
}
