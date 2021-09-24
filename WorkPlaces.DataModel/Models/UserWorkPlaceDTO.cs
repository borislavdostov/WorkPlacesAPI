using System;

namespace WorkPlaces.DataModel.Models
{
    public class UserWorkplaceDTO
    {
        public int Id { get; set; }

        public string User { get; set; }

        public string Workplace { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }
    }
}
