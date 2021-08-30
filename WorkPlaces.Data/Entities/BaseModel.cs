using System;

namespace WorkPlaces.Data.Common
{
    public abstract class BaseModel : IBaseModel
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public bool IsDeleted => DeletedAt != null;
    }
}
