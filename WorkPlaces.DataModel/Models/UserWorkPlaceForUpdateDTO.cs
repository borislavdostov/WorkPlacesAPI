using System;
using System.ComponentModel.DataAnnotations;

namespace WorkPlaces.DataModel.Models
{
    public class UserWorkPlaceForUpdateDTO
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int WorkPlaceId { get; set; }

        [Required]
        public DateTime FromDate { get; set; }

        [Required]
        public DateTime ToDate { get; set; }
    }
}
