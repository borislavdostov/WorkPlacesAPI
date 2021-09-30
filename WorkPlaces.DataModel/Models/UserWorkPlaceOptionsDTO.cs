using System.Collections.Generic;

namespace Workplaces.DataModel.Models
{
    public class UserWorkplaceOptionsDTO
    {
        public IEnumerable<UserDropdownDTO> Users { get; set; }
        public IEnumerable<WorkplaceDropdownDTO> Workplaces { get; set; }
    }
}
