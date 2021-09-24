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
        [HttpGet(Name = nameof(GetUserWorkPlaces))]
        public ActionResult<IEnumerable<UserWorkplaceDTO>> GetUserWorkPlaces()
        {
            var userWorkPlaces = userWorkplacesService.GetUserWorkplaces();
            return Ok(userWorkPlaces);
        }

        /// <summary>
        /// Gets all options for user work place
        /// </summary>
        /// <returns>All options for user work place(without deleted)</returns>
        /// <response code="200">All options for user work place(without deleted) or empty collection</response>
        [HttpGet("options")]
        public ActionResult<UserWorkplaceOptionsDTO> GetUserWorkPlaceOptions()
        {
            var userWorkPlaceOptions = userWorkplacesService.GetUserWorkplaceOptions();
            return Ok(userWorkPlaceOptions);
        }

        /// <summary>
        /// Gets specific user workplace
        /// </summary>
        /// <param name="userWorkPlaceId">Id of the user workplace</param>
        /// <returns>The user workplace with the given Id</returns>
        /// <response code="200">Returns a user workplace with the given Id</response>
        /// <response code="404">If a user workplace with the given id does not exist</response>
        [HttpGet("{userWorkPlaceId}")]
        public async Task<ActionResult<UserWorkplaceForManipulationDTO>> GetUserWorkPlace(int userWorkPlaceId)
        {
            if (!await userWorkplacesService.UserWorkplaceExistsAsync(userWorkPlaceId))
            {
                return NotFound();
            }

            var userWorkPlace = await userWorkplacesService.GetUserWorkplaceAsync(userWorkPlaceId);
            return Ok(userWorkPlace);
        }

        /// <summary>
        /// Creates new user workplace
        /// </summary>
        /// <param name="userWorkPlace"></param>
        /// <returns>A newly created user workplace</returns>
        /// <response code="201">Returns the newly created user workplace</response>
        [HttpPost]
        public async Task<ActionResult<UserWorkplaceDTO>> CreateUserWorkPlace(UserWorkplaceForManipulationDTO userWorkPlace)
        {
            if (!await usersService.UserExistsAsync(userWorkPlace.UserId) ||
                !await workplacesService.WorkplaceExistsAsync(userWorkPlace.WorkplaceId))
            {
                return NotFound();
            }

            var userWorkPlaceToReturn = await userWorkplacesService.CreateUserWorkplaceAsync(userWorkPlace);
            return CreatedAtRoute(nameof(GetUserWorkPlaces),
                new { userWorkPlaceId = userWorkPlaceToReturn.Id }, userWorkPlaceToReturn);
        }

        /// <summary>
        /// Updates user workplace
        /// </summary>
        /// <param name="userWorkPlaceId">Id of the user workplace</param>
        /// <param name="userWorkPlace"></param>
        /// <returns>No content</returns>
        /// <response code="204">If the user workplace is updated successfully</response>
        /// <response code="404">If a user workplace, user or workplace with the given id does not exist</response>
        [HttpPut("{userWorkPlaceId}")]
        public async Task<IActionResult> UpdateUserWorkPlace(int userWorkPlaceId, UserWorkplaceForManipulationDTO userWorkPlace)
        {
            if (!await userWorkplacesService.UserWorkplaceExistsAsync(userWorkPlaceId) ||
                !await usersService.UserExistsAsync(userWorkPlace.UserId) ||
                !await workplacesService.WorkplaceExistsAsync(userWorkPlace.WorkplaceId))
            {
                return NotFound();
            }

            await userWorkplacesService.UpdateUserWorkplaceAsync(userWorkPlaceId, userWorkPlace);
            return NoContent();
        }

        /// <summary>
        /// Deletes user workplace
        /// </summary>
        /// <param name="userWorkPlaceId">Id of the user workplace</param>
        /// <returns>No content</returns>
        /// <response code="204">If the user workplace is deleted successfully</response>
        /// <response code="404">If a user workplace with the given id does not exist</response>
        [HttpDelete("{userWorkPlaceId}")]
        public async Task<IActionResult> DeleteUserWorkPlace(int userWorkPlaceId)
        {
            if (!await userWorkplacesService.UserWorkplaceExistsAsync(userWorkPlaceId))
            {
                return NotFound();
            }

            await userWorkplacesService.DeleteUserWorkplaceAsync(userWorkPlaceId);
            return NoContent();
        }
    }
}
