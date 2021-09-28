using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkPlaces.DataModel.Models;
using WorkPlaces.Service.Interfaces;

namespace WorkPlaces.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserWorkplacesController : ControllerBase
    {
        private readonly IUserWorkplacesService userWorkplacesService;
        private readonly IUsersService usersService;
        private readonly IWorkplacesService workplacesService;

        public UserWorkplacesController(
            IUserWorkplacesService userWorkplacesService,
            IUsersService usersService,
            IWorkplacesService workplacesService)
        {
            this.userWorkplacesService = userWorkplacesService;
            this.usersService = usersService;
            this.workplacesService = workplacesService;
        }

        /// <summary>
        /// Gets all user workplaces
        /// </summary>
        /// <returns>All user work places(without deleted)</returns>
        /// <response code="200">Returns all user work places(without deleted) or empty collection</response>
        [HttpGet(Name = nameof(GetUserWorkplaces))]
        public ActionResult<IEnumerable<UserWorkplaceDTO>> GetUserWorkplaces()
        {
            var userWorkplaces = userWorkplacesService.GetUserWorkplaces();
            return Ok(userWorkplaces);
        }

        /// <summary>
        /// Gets all options for user work place
        /// </summary>
        /// <returns>All options for user work place(without deleted)</returns>
        /// <response code="200">All options for user work place(without deleted) or empty collection</response>
        [HttpGet("options")]
        public ActionResult<UserWorkplaceOptionsDTO> GetUserWorkplaceOptions()
        {
            var userWorkplaceOptions = userWorkplacesService.GetUserWorkplaceOptions();
            return Ok(userWorkplaceOptions);
        }

        /// <summary>
        /// Gets specific user workplace
        /// </summary>
        /// <param name="userWorkplaceId">Id of the user workplace</param>
        /// <returns>The user workplace with the given Id</returns>
        /// <response code="200">Returns a user workplace with the given Id</response>
        /// <response code="404">If a user workplace with the given id does not exist</response>
        [HttpGet("{userWorkplaceId}")]
        public async Task<ActionResult<UserWorkplaceForManipulationDTO>> GetUserWorkplace(int userWorkplaceId)
        {
            if (!await userWorkplacesService.UserWorkplaceExistsAsync(userWorkplaceId))
            {
                return NotFound();
            }

            var userWorkplace = await userWorkplacesService.GetUserWorkplaceAsync(userWorkplaceId);
            return Ok(userWorkplace);
        }

        /// <summary>
        /// Creates new user workplace
        /// </summary>
        /// <param name="userWorkplace"></param>
        /// <returns>A newly created user workplace</returns>
        /// <response code="201">Returns the newly created user workplace</response>
        [HttpPost]
        public async Task<ActionResult<UserWorkplaceDTO>> CreateUserWorkplace(UserWorkplaceForManipulationDTO userWorkplace)
        {
            if (!await usersService.UserExistsAsync(userWorkplace.UserId) ||
                !await workplacesService.WorkplaceExistsAsync(userWorkplace.WorkplaceId))
            {
                return NotFound();
            }

            var userWorkplaceToReturn = await userWorkplacesService.CreateUserWorkplaceAsync(userWorkplace);
            return CreatedAtRoute(nameof(GetUserWorkplaces),
                new { userWorkplaceId = userWorkplaceToReturn.Id }, userWorkplaceToReturn);
        }

        /// <summary>
        /// Updates user workplace
        /// </summary>
        /// <param name="userWorkplaceId">Id of the user workplace</param>
        /// <param name="userWorkplace"></param>
        /// <returns>No content</returns>
        /// <response code="204">If the user workplace is updated successfully</response>
        /// <response code="404">If a user workplace, user or workplace with the given id does not exist</response>
        [HttpPut("{userWorkplaceId}")]
        public async Task<IActionResult> UpdateUserWorkplace(int userWorkplaceId, UserWorkplaceForManipulationDTO userWorkplace)
        {
            if (!await userWorkplacesService.UserWorkplaceExistsAsync(userWorkplaceId) ||
                !await usersService.UserExistsAsync(userWorkplace.UserId) ||
                !await workplacesService.WorkplaceExistsAsync(userWorkplace.WorkplaceId))
            {
                return NotFound();
            }

            await userWorkplacesService.UpdateUserWorkplaceAsync(userWorkplaceId, userWorkplace);
            return NoContent();
        }

        /// <summary>
        /// Deletes user workplace
        /// </summary>
        /// <param name="userWorkplaceId">Id of the user workplace</param>
        /// <returns>No content</returns>
        /// <response code="204">If the user workplace is deleted successfully</response>
        /// <response code="404">If a user workplace with the given id does not exist</response>
        [HttpDelete("{userWorkplaceId}")]
        public async Task<IActionResult> DeleteUserWorkplace(int userWorkplaceId)
        {
            if (!await userWorkplacesService.UserWorkplaceExistsAsync(userWorkplaceId))
            {
                return NotFound();
            }

            await userWorkplacesService.DeleteUserWorkplaceAsync(userWorkplaceId);
            return NoContent();
        }
    }
}
