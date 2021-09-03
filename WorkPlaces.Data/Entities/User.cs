using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WorkPlaces.Data.Common;

namespace WorkPlaces.Data.Entities
{
    public class User : BaseModel
    {
        public User()
        {
            WorkPlaces = new HashSet<UserWorkPlace>();
        }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(50)]
        public string Email { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        [Required]
        public DateTime DateOfBirth { get; set; }

        public ICollection<UserWorkPlace> WorkPlaces { get; set; }
    }
}
