using System;
using System.ComponentModel.DataAnnotations;
using Workplaces.Data.Common;

namespace Workplaces.Data.Entities
{
    public class UserWorkplace : BaseModel
    {
        [Required]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        [Required]
        public int WorkplaceId { get; set; }

        public virtual Workplace Workplace { get; set; }

        [Required]
        public DateTime FromDate { get; set; }

        [Required]
        public DateTime ToDate { get; set; }
    }
}
