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

        [HttpGet(Name = nameof(GetUsers))]
        public ActionResult<IEnumerable<UserDTO>> GetUsers()
        {
            var users = usersService.GetUsers();
            return Ok(users);
        }

        [HttpGet("{userId}")]
        public ActionResult<UserDTO> GetUser(int userId)
        {
            var user = usersService.GetUser(userId);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> CreateUser(UserForCreationDTO user)
        {
            var userToReturn = await usersService.CreateUserAsync(user);
            return CreatedAtRoute(nameof(GetUsers), new { authorId = userToReturn.Id }, userToReturn);
        }
    }
}
