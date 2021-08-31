using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkPlaces.DataModel.Models;
using WorkPlaces.Service.Interfaces;

namespace WorkPlaces.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserWorkPlacesController : ControllerBase
    {
        private readonly IUserWorkPlacesService userWorkPlacesService;
        private readonly IUsersService usersService;
        private readonly IWorkPlacesService workPlacesService;

        public UserWorkPlacesController(
            IUserWorkPlacesService userWorkPlacesService,
            IUsersService usersService,
            IWorkPlacesService workPlacesService)
        {
            this.userWorkPlacesService = userWorkPlacesService;
            this.usersService = usersService;
            this.workPlacesService = workPlacesService;
        }

        [HttpGet(Name = nameof(GetUserWorkPlaces))]
        public ActionResult<IEnumerable<UserWorkPlaceDTO>> GetUserWorkPlaces()
        {
            var userWorkPlaces = userWorkPlacesService.GetUserWorkPlaces();
            return Ok(userWorkPlaces);
        }

        [HttpGet("{userWorkPlaceId}")]
        public ActionResult<UserWorkPlaceDTO> GetUserWorkPlace(int userWorkPlaceId)
        {
            if (!userWorkPlacesService.UserWorkPlaceExists(userWorkPlaceId))
            {
                return NotFound();
            }

            var userWorkPlace = userWorkPlacesService.GetUserWorkPlace(userWorkPlaceId);
            return Ok(userWorkPlace);
        }

        [HttpPost]
        public async Task<ActionResult<UserWorkPlaceDTO>> CreateUserWorkPlace(UserWorkPlaceForCreationDTO userWorkPlace)
        {
            if (!usersService.UserExists(userWorkPlace.UserId))
            {
                return NotFound();
            }

            if (!workPlacesService.WorkPlaceExists(userWorkPlace.WorkPlaceId))
            {
                return NotFound();
            }

            var userWorkPlaceToReturn = await userWorkPlacesService.CreateUserWorkPlaceAsync(userWorkPlace);
            return CreatedAtRoute(nameof(GetUserWorkPlaces), new { userWorkPlaceId = userWorkPlaceToReturn.Id }, userWorkPlaceToReturn);
        }

        [HttpPut("{userWorkPlaceId}")]
        public async Task<ActionResult> UpdateUserWorkPlace(int userWorkPlaceId, UserWorkPlaceForUpdateDTO userWorkPlace)
        {
            if (!userWorkPlacesService.UserWorkPlaceExists(userWorkPlaceId))
            {
                return NotFound();
            }

            if (!usersService.UserExists(userWorkPlace.UserId))
            {
                return NotFound();
            }

            if (!workPlacesService.WorkPlaceExists(userWorkPlace.WorkPlaceId))
            {
                return NotFound();
            }

            await userWorkPlacesService.UpdateUserWorkPlace(userWorkPlaceId, userWorkPlace);
            return NoContent();
        }
    }
}
