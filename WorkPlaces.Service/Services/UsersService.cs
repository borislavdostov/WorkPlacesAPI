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
            var userEntity = usersRepository.GetUser(userId);

            if (userEntity == null)
            {
                return null;
            }

            return new UserDTO
            {
                Id = userEntity.Id,
                Name = $"{userEntity.FirstName} {userEntity.LastName}",
                Email = userEntity.Email,
                Age = userEntity.DateOfBirth.GetAge()
            };
        }

        public async Task<UserDTO> CreateUserAsync(UserForCreationDTO user)
        {
            var userEntity = new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth
            };

            await usersRepository.AddUserAsync(userEntity);

            return new UserDTO
            {
                Id = userEntity.Id,
                Name = $"{userEntity.FirstName} {userEntity.LastName}",
                Email = userEntity.Email,
                Age = userEntity.DateOfBirth.GetAge()
            };
        }

        public void UpdateUser(UserForCreationDTO user)
        {
            throw new NotImplementedException();
            //repository.Update(user);
            //repository.SaveChangesAsync();
        }

        public void DeleteUser(int userId)
        {
            var user = usersRepository.GetUser(userId);

            usersRepository.DeleteUser(user);
        }
    }
}
