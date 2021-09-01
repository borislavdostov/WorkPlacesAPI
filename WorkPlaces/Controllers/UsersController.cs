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

        /// <summary>
        /// Gets all users
        /// </summary>
        /// <returns>All users(without deleted)</returns>
        /// <response code="200">Returns all users(without deleted) or empty collection</response>
        [HttpGet]
        public ActionResult<IEnumerable<UserDTO>> GetUsers()
        {
            var users = usersService.GetUsers();
            return Ok(users);
        }

        /// <summary>
        /// Gets specific user
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <returns>The user with the given Id</returns>
        /// <response code="200">Returns a user with the given Id</response>
        /// <response code="404">If a user with the given id does not exist</response>
        [HttpGet("{userId}", Name = nameof(GetUser))]
        public async Task<ActionResult<UserDTO>> GetUser(int userId)
        {
            if (!await usersService.UserExistsAsync(userId))
            {
                return NotFound();
            }

            var user = await usersService.GetUserAsync(userId);
            return Ok(user);
        }

        /// <summary>
        /// Creates new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns>A newly created user</returns>
        /// <response code="201">Returns the newly created user</response>
        [HttpPost]
        public async Task<ActionResult<UserDTO>> CreateUser(UserForManipulationDTO user)
        {
            var userToReturn = await usersService.CreateUserAsync(user);
            return CreatedAtRoute(nameof(GetUser), new { userId = userToReturn.Id }, userToReturn);
        }

        /// <summary>
        /// Updates user 
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <param name="user"></param>
        /// <returns>No content</returns>
        /// <response code="204">If the user is updated successfully</response>
        /// <response code="404">If a user with the given id does not exist</response>
        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, UserForManipulationDTO user)
        {
            if (!await usersService.UserExistsAsync(userId))
            {
                return NotFound();
            }

            await usersService.UpdateUserAsync(userId, user);
            return NoContent();
        }

        /// <summary>
        /// Deletes user
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <returns>No content</returns>
        /// <response code="204">If the user is deleted successfully</response>
        /// <response code="404">If a user with the given id does not exist</response>
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            if (!await usersService.UserExistsAsync(userId))
            {
                return NotFound();
            }

            await usersService.DeleteUserAsync(userId);
            return NoContent();
        }
    }
}
