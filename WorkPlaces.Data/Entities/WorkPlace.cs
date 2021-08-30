using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WorkPlaces.Data.Common;

namespace WorkPlaces.Data.Entities
{
    public class WorkPlace : BaseModel
    {
        public WorkPlace()
        {
            Users = new HashSet<UserWorkPlace>();
        }

        [Required]
        [MinLength(5), MaxLength(100)]
        public string Name { get; set; }

        public ICollection<UserWorkPlace> Users { get; set; }
    }
}
