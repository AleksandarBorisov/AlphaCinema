using AlphaCinemaData.Models.Contracts;

namespace AlphaCinemaData.Models.Associative
{
    public class WatchedMovie : IDeletable
	{
		virtual public int UserId { get; set; }

		virtual public int ProjectionId { get; set; }

		virtual public User User { get; set; }

		virtual public Projection Projection { get; set; }

		virtual public bool IsDeleted { get; set; }

	}
}
