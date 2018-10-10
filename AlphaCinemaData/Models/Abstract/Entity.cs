using AlphaCinemaData.Models.Contracts;
using System;

namespace AlphaCinemaData.Models.Abstract
{
    public abstract class Entity : IDeletable
    {
        public Guid Id { get; set; } 

        public bool IsDeleted { get; set; }

    }
}
