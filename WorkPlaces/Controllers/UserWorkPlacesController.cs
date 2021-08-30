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

        public UserWorkPlacesController(IUserWorkPlacesService userWorkPlacesService)
        {
            this.userWorkPlacesService = userWorkPlacesService;
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
            var userWorkPlace = userWorkPlacesService.GetUserWorkPlace(userWorkPlaceId);

            if (userWorkPlace == null)
            {
                return NotFound();
            }

            return Ok(userWorkPlace);
        }

        [HttpPost]
        public async Task<ActionResult<UserWorkPlaceDTO>> CreateUserWorkPlace(UserWorkPlaceForCreationDTO userWorkPlace)
        {
            var userWorkPlaceToReturn = await userWorkPlacesService.CreateUserWorkPlaceAsync(userWorkPlace);

            if (userWorkPlaceToReturn == null)
            {
                return NotFound();
            }

            return CreatedAtRoute(nameof(GetUserWorkPlaces), new { userWorkPlaceId = userWorkPlaceToReturn.Id }, userWorkPlaceToReturn);
        }
    }
}
