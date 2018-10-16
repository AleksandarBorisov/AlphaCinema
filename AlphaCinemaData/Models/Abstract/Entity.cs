using AlphaCinemaData.Models.Contracts;

namespace AlphaCinemaData.Models.Abstract
{
    public abstract class Entity : IDeletable
    {
		virtual public int Id { get; set; }
		virtual public bool IsDeleted { get; set; }

    }
}
