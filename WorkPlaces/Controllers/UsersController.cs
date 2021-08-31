using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkPlaces.DataModel.Models;
using WorkPlaces.Service.Interfaces;

namespace WorkPlaces.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserDTO>> GetUsers()
        {
            var users = usersService.GetUsers();
            return Ok(users);
        }

        [HttpGet("{userId}", Name = nameof(GetUser))]
        public ActionResult<UserDTO> GetUser(int userId)
        {
            if (!usersService.UserExists(userId))
            {
                return NotFound();
            }

            var user = usersService.GetUser(userId);
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> CreateUser(UserForManipulationDTO user)
        {
            var userToReturn = await usersService.CreateUserAsync(user);
            return CreatedAtRoute(nameof(GetUser), new { userId = userToReturn.Id }, userToReturn);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, UserForManipulationDTO user)
        {
            if (!usersService.UserExists(userId))
            {
                return NotFound();
            }

            await usersService.UpdateUser(userId, user);
            return NoContent();
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            if (!usersService.UserExists(userId))
            {
                return NotFound();
            }

            await usersService.DeleteUser(userId);
            return NoContent();
        }
    }
}
