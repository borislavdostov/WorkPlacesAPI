using System;
using System.ComponentModel.DataAnnotations;

namespace Workplaces.Data.Common
{
    public interface IBaseModel
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}
