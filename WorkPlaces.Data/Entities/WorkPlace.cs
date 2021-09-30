using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Workplaces.Data.Common;

namespace Workplaces.Data.Entities
{
    public class Workplace : BaseModel
    {
        public Workplace()
        {
            Users = new HashSet<UserWorkplace>();
        }

        [Required]
        [MinLength(5), MaxLength(100)]
        public string Name { get; set; }

        public ICollection<UserWorkplace> Users { get; set; }
    }
}
