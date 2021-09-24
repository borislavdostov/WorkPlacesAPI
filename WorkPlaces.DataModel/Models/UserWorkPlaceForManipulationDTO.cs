using System;
using System.ComponentModel.DataAnnotations;

namespace WorkPlaces.DataModel.Models
{
    public class UserWorkplaceForManipulationDTO
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int WorkplaceId { get; set; }

        [Required]
        public DateTime FromDate { get; set; }

        [Required]
        public DateTime ToDate { get; set; }
    }
}
