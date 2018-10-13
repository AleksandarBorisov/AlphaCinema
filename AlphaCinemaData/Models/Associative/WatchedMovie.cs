using AlphaCinemaData.Models.Contracts;

namespace AlphaCinemaData.Models.Associative
{
    public class WatchedMovie : IDeletable
	{
        public int UserId { get; set; }

        public int ProjectionId { get; set; }

        public User User { get; set; }

        public Projection Projection { get; set; }

		public bool IsDeleted { get; set; }

	}
}
