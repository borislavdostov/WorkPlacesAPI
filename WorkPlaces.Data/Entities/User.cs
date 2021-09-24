using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WorkPlaces.Data.Common;
using WorkPlaces.Extensions;

namespace WorkPlaces.Data.Entities
{
    public class User : BaseModel
    {
        public User()
        {
            WorkPlaces = new HashSet<UserWorkplace>();
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

        public int Age => DateOfBirth.GetAge();

        [Required]
        public DateTime DateOfBirth { get; set; }

        public ICollection<UserWorkplace> WorkPlaces { get; set; }
    }
}
