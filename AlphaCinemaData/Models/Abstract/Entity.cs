using AlphaCinemaData.Models.Contracts;
using System;

namespace AlphaCinemaData.Models.Abstract
{
    public abstract class Entity : IAuditable, IDeletable
    {
        public Guid Id { get; set; } 

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
