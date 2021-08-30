using System;

namespace WorkPlaces.DataModel.Models
{
    public class UserWorkPlaceForCreationDTO
    {
        public int UserId { get; set; }

        public int WorkPlaceId { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }
    }
}
