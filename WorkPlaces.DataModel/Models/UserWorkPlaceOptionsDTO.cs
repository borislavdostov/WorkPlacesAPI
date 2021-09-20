using System.Collections.Generic;

namespace WorkPlaces.DataModel.Models
{
    public class UserWorkPlaceOptionsDTO
    {
        public IEnumerable<UserDropdownDTO> Users { get; set; }
        public IEnumerable<WorkPlaceDropdownDTO> WorkPlaces { get; set; }
    }
}
