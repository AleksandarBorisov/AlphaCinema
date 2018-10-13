using AlphaCinemaData.Models.Contracts;

namespace AlphaCinemaData.Models.Abstract
{
    public abstract class Entity : IDeletable
    {
        public int Id { get; set; }
		public bool IsDeleted { get; set; }

    }
}
